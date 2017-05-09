#!/bin/bash
#
#
# Sets up this Halsign VPX on first-boot
#
# On Redhat systems this file would go in the /etc/rc.d/init.d directory (make
# sure it's marked executable after copying it there), and then the command:
#
#       'chkconfig vpx_startup_setup on'
#
# can be used to tell the system to run it on startup.
#
# chkconfig: - 80 20
# description: vpx_startup_setup is the script for setting up the VPX on first-boot.

wlbpath="/opt/halsign/wlb/"
wlbconfpath=$wlbpath"wlb.conf"
wlblogdir="/var/log/wlb/"
logfile=$wlblogdir"wlb_install_log.log"
touchfile="/etc/init.d/vpx_startup_setup_done"
stunnel_jail="/usr/local/var/run/stunnel/"
pg_conf="/var/lib/pgsql/9.0/data/postgresql.conf"
dbusername=postgres
dbpassword=postgres
wlbusername=admin
wlbpassword=admin
hn=localhost
hd=localdomain
wcfPort=8012

function show_eula()
{
	clear
	echo -e "\033[44;37;1mHalsign Workload Balancing Virtual Appliance - End User License Agreement        \033[0m"
	
	echo ""	
	echo "--------------------------------------------------------------------"
	echo ""
	cat $wlbpath"eula.txt"
	echo ""
	echo "--------------------------------------------------------------------"	
	local resp=""
	while true; do
		read -p "Accept the terms in License Agreement? (yes/no): " resp
		resp=`echo $resp | tr "[:upper:]" "[:lower:]"`
		if [ "$resp" == "y" ] || [ "$resp" == "yes" ]; then #accepted, continue with the configuration
			break;		
		elif [ "$resp" == "n" ] || [ "$resp" == "no" ]; then #did not accept, ask if vpx should be shut down
			shutdown=""
			while true; do
				read -p "Shutdown virtual appliance? (yes/no): " shutdown
				shutdown=`echo $shutdown | tr "[:upper:]" "[:lower:]"`
				if [ "$shutdown" == "y" ] || [ "$shutdown" == "yes" ]; then
					echo "Shutting down..."
					shutdown -h now
				elif [ "$shutdown" == "n" ] || [ "$shutdown" == "no" ]; then
					break
				else
					echo "Type 'yes' or 'no'."
				fi
			done
		else
			echo "Type 'yes' or 'no'."
		fi
	done
}

# if /etc/init.d/vpx_startup_setup_done exists do nothing
# deleting this file manually will wreak havoc, so please don't.
if [ -f $touchfile ]
then
	#run stunnel
	if [ -f /usr/sbin/stunnel ] && [ -f /etc/stunnel/stunnel.conf ]; then
		/usr/sbin/stunnel
	fi
	sleep 2
	echo ""
	exit 0;
	
fi

#show eula
#show_eula;

mkdir -p $wlblogdir


#delete database folder if it exists
#This assumes that configuration was interrupted before completion previously
rm -rf /var/lib/pgsql/9.0/data


# configure root password
#while ! passwd ; do : ; done

# configure network
# write network details to /etc/sysconfig/network-scripts/ifcfg-eth0
echo "DEVICE=eth0" > /etc/sysconfig/network-scripts/ifcfg-eth0

echo "BOOTPROTO=dhcp" >> /etc/sysconfig/network-scripts/ifcfg-eth0
echo "ONBOOT=yes" >> /etc/sysconfig/network-scripts/ifcfg-eth0

# set hostname in /etc/sysconfig/network so it comes back after a restart
echo "NETWORKING=yes" > /etc/sysconfig/network
echo "NETWORKING_IPV6=no" >> /etc/sysconfig/network
echo "HOSTNAME=$hn" >> /etc/sysconfig/network

# set the hostname for this session.
/bin/hostname "$hn"

#delete eth1
rm -f /etc/sysconfig/network-scripts/ifcfg-eth1
# refresh networking
service network restart

#add swapfile
echo ""

echo -n "Creating swap partition...  "
mkswap /swapfile >> $logfile 2>&1
swapon /swapfile >> $logfile 2>&1
echo "/swapfile                swap                   swap    defaults        0 0" >> /etc/fstab
echo "[  OK  ]"

# get ipaddress
ip=`ifconfig  | grep -m1 'inet addr:'| cut -d: -f2 | awk '{ print $1}'`

# add the hostname to hosts

echo "127.0.0.1	localhost.localdomain localhost" > /etc/hosts
echo "$ip $hn.$dn $hn" >> /etc/hosts
echo "::1 localhost6.localdomain6 localhost6" >> /etc/hosts

###################################
#scripts to start wlb services will go here
###################################

# postgres bits

echo -n "Initializing database...  "
service postgresql-9.0 initdb > $logfile
echo "[  OK  ]"

# change to trust mode to do db operations
sed -i 's/ident/trust/g' /var/lib/pgsql/9.0/data/pg_hba.conf
#sed -i '/127/s/ident/password/g' /var/lib/pgsql/9.0/data/pg_hba.conf
#set the proper search path
sed -i -e '/search/s/public/"WorkloadBalancing"/g' $pg_conf -e '/^#search/s/#//'
#get total memory of WLB vpx
mem=`free -t -m | grep "Mem" | awk '{ print $2}'`
let shared_buffers=$(echo "$mem/4")
let wal_buffers=$(echo "$shared_buffers/32")
let effective_cache=$(echo "$mem/2")
#set postgresql server profermance turning property: 
#shared_buffers, wal_buffers, effective_cache_size, checkpoint_segments, and log_line_prefix
sed -i -e "s/shared_buffers = 32MB/shared_buffers = ${shared_buffers}MB/; s/wal_buffers = 64kB/wal_buffers = ${wal_buffers}MB/; /^#wal_buffers/s/#//; s/effective_cache_size = 128MB/effective_cache_size = ${effective_cache}MB/; /^#effective_cache_size/s/#//; s/checkpoint_segments = 3/checkpoint_segments = 10/; /^#checkpoint_segments/s/#//; s/checkpoint_completion_target = 0.5/checkpoint_completion_target = 0.9/; /^#checkpoint_completion_target/s/#//; s/log_line_prefix = ''/log_line_prefix = '%t:%r:%u@%d:[%p]:'/; /^#log_line_prefix/s/#//;" $pg_conf

#change default logging configurations. log rotation will be handled by logrotate
sed -i -e "s/\(log_filename\).*/\1 = 'postgresql.log'/" $pg_conf
sed -i -e "s/\(log_truncate_on_rotation.*\)/#\1/" $pg_conf
sed -i -e "s/\(log_rotation_age\).*/\1 = 0/" $pg_conf
sed -i -e "s/\(log_rotation_size\).*/\1 = 0/" $pg_conf

#if wlb.conf has wlb_unittest replace it
sed -i 's/WLB_UnitTest/WorkloadBalancing/g' $wlbconfpath

# Next time VPX starts, make sure the database starts automatically
chkconfig postgresql-9.0 on
echo -n "Starting PostgreSQL database...  "
service postgresql-9.0 start > $logfile
echo "[  OK  ]"

# Create postgres username and password based on input (or defaults)

#change the db password
#disable the history expansion, so that it'll handle ! correctly
set +o histexpand

#change the trust mode to password mode
sed -i 's/trust/password/g' /var/lib/pgsql/9.0/data/pg_hba.conf		

echo ""
echo -n "Loading objects into database...  "

#set the encoding of db properly
sed -i -e '/CREATE DATABASE/s/;/ ENCODING=\x27UTF8\x27;/g' $wlbpath"wlb_db.out"
#change the ownerships to new user
sed -i '/connect/! s/postgres/'$dbusername'/g' $wlbpath"wlb_db.out"
#then create the database
psql -Upostgres -f $wlbpath"wlb_db.out" >> $logfile 2>&1
        
psql -Upostgres -c "ALTER USER $dbusername WITH PASSWORD '$dbpassword'" >> $logfile 2>&1

echo "[  OK  ]"		

#restart db
echo -n "Restarting PostgreSQL server...  "
service postgresql-9.0 restart > $logfile
echo "[  OK  ]"
#re-enable history expansion
set -o histexpand

echo -n "Saving Workload Balancing Server settings...  "
#run logrotate every 10 mins
echo "*/10 * * * * root /usr/sbin/logrotate /etc/logrotate.conf" >> /etc/crontab
cd $wlbpath
#call the tool to handle passwords and stuff
mono WlbConfig.exe  DBUsername=$dbusername DBPassword=$dbpassword WlbUsername=$wlbusername WlbPassword=$wlbpassword >> $logfile 2>&1

#update the config file, to update port number, uri and hostnames
#write port to stunnel.conf
let connPort=$wcfPort+1
let restPort=$connPort+1

cat <<EOT >> /etc/stunnel/stunnel.conf
[rest]
accept = 443
connect = $restPort

[soap]
accept = $wcfPort
connect = $connPort

EOT

# Write WCF port and RESTful port to wlb.conf file
sed -i 's#\(WcfServicePort\s*=\s*\)[0-9]*#\1'$connPort'#g' $wlbconfpath
sed -i 's#\(RestHttpPort\s*=\s*\)[0-9]*#\1'$restPort'#g' $wlbconfpath
#set use ssl flag to false, since secure layer will be handled by stunnel
sed -i 's#\(WcfServiceUseSSL\s*=\s*\).*#\1'false'#g' $wlbconfpath
echo "[  OK  ]"
echo ""
#modify iptables conf file to open access the port
sed -i 's/8012/'$wcfPort'/g' /etc/sysconfig/iptables
#and restart the service
service iptables restart > /dev/null

#this part is for self-signed certificate creation

echo -n "Creating self-signed certificate...  "
mkdir -p /etc/ssl/certs
#make sure hostname isn't left empty
if [ "$hn" == "" ]; then
	hn=$ip
fi

# Write out key(.key) ertificate (.pem)
openssl req -x509 -days 3650 -nodes -subj "/CN=$hn" -newkey rsa:1024 -keyout /etc/ssl/certs/server.key -out /etc/ssl/certs/server.pem >> $logfile 2>&1

# Set permissions for root to RW for the cert files (pem & key) 
chmod 600 /etc/ssl/certs/server.*

#run stunnel
mkdir -p $stunnel_jail

# Set permissions RWE for everyone to /usr/local/var/run/stunnel/
chmod 777 $stunnel_jail
/usr/sbin/stunnel
echo "[  OK  ]"
echo ""

echo -n "Importing reports into database...  "
cd $wlbpath
mono ReportImport.exe -v --version tampa -p ./Reports/tampa >> $logfile 2>&1
mono ReportImport.exe -v --version creedence -p ./Reports/creedence >> $logfile 2>&1
echo "[  OK  ]"

echo ""

rm -f $wlbpath"wlb_db.out"
# touch /etc/init.d/vpx_startup_setup_done
# so that this script doesn't run again
touch $touchfile
#deleting this file will prompt vpx to delete the entire database during next reboot
#so this file should not be removed
chmod 600 $touchfile

#temporary directory for locks
mkdir -p /tmp/wlb/
chkconfig workloadbalancing on
#Start the workloadbalancing services
service workloadbalancing start
sleep 3

clear
echo -e "\033[44;37;1mHalsign Workload Balancing Virtual Appliance                                     \033[0m"
echo ""
echo "Halsign Workload Balancing Virtual Appliance configuration is complete."
echo ""
echo "Use the following information to connect to Workload Balancing Server:"
echo "    Address: $ip"
echo "    Port: $wcfPort"
echo "    WLB user name: $wlbusername"
echo ""
