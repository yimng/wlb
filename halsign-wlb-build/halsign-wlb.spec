Summary: Halsign vGateServer Workload Balancing
Name: halsign-wlb
Version: 6.5.0
Release: 153
License: Proprietary
Group: vGate 
URL: http://www.halsign.com
Vendor: Halsign, Inc.
BuildRoot: %{_tmppath}/%{name}-%{version}-%{release}-buildroot
Requires: postgresql90 postgresql90-server vixie-cron
Requires: mono >= 2.10.6

%description
%prep
%build
#%patch

%install
echo Starting install section

rm -rf %{buildroot}
mkdir -p %{buildroot}/opt/halsign/wlb
mkdir -p %{buildroot}/etc/init.d
mkdir %{buildroot}/etc/rc2.d
mkdir %{buildroot}/etc/rc3.d
mkdir %{buildroot}/etc/rc4.d
mkdir %{buildroot}/etc/rc5.d
mkdir -p %{buildroot}/var/spool/cron
mkdir -p %{buildroot}/root/
mkdir -p %{buildroot}/etc/sysconfig
mkdir -p %{buildroot}/etc/stunnel
mkdir -p %{buildroot}/etc/logrotate.d
#touch %{buildroot}/etc/sysconfig/network

#echo Copying %{SOURCE0} to %{buildroot}/etc/init.d
#cp %{SOURCE0} %{buildroot}/etc/init.d
cp /repos/vpx-wlb.hg/vpx_startup_setup.sh %{buildroot}/etc/init.d
cp /repos/vpx-wlb.hg/vpx_setup_patch.sh %{buildroot}/etc/init.d

chmod +x %{buildroot}/etc/init.d/vpx_startup_setup.sh
chmod +x %{buildroot}/etc/init.d/vpx_setup_patch.sh

ln -sf /etc/init.d/vpx_startup_setup.sh %{buildroot}/etc/rc2.d/S99vpx_startup_setup.sh
ln -sf /etc/init.d/vpx_startup_setup.sh %{buildroot}/etc/rc3.d/S99vpx_startup_setup.sh
ln -sf /etc/init.d/vpx_startup_setup.sh %{buildroot}/etc/rc4.d/S99vpx_startup_setup.sh
ln -sf /etc/init.d/vpx_startup_setup.sh %{buildroot}/etc/rc5.d/S99vpx_startup_setup.sh

#cp -R /repos/vpx-wlb.hg/autogen/* %{buildroot}/opt/halsign/wlb
cp /repos/vpx-wlb.hg/iptables %{buildroot}/etc/sysconfig
cp /repos/vpx-wlb.hg/stunnel.conf %{buildroot}/etc/stunnel

cd %{buildroot}/opt/halsign/wlb
build_loc="/output/wlb/wlb.zip"
cp -f $build_loc .
unzip wlb.zip
#delete the zip file
rm -rf wlb.zip
#create link to startup script in home directory
mv %{buildroot}/opt/halsign/wlb/workloadbalancing %{buildroot}/etc/init.d
mv %{buildroot}/opt/halsign/wlb/root.cron %{buildroot}/var/spool/cron/root
mv %{buildroot}/opt/halsign/wlb/postgres.logrotate %{buildroot}/etc/logrotate.d/postgres


chmod +x %{buildroot}/etc/init.d/workloadbalancing
chmod +x %{buildroot}/opt/halsign/wlb/wlbconfig
chmod +x %{buildroot}/opt/halsign/wlb/wlbmaintenance.sh
chmod 644 %{buildroot}/var/spool/cron/root
chmod +x %{buildroot}/opt/halsign/wlb/wlb-watchdog.sh
chmod +x %{buildroot}/opt/halsign/wlb/wlb

%pre
if [ "$1" = "1" ]; then
	# Perform tasks to prepare for the initial installation
	echo "This is the fresh installation."
elif [ "$1" = "2" ]; then
	# Perform whatever maintenance must occur before the upgrade begins
	service workloadbalancing stop
	if [ -f /opt/halsign/wlb/wlb.conf ]; then
		cp /opt/halsign/wlb/wlb.conf /opt/halsign/wlb/wlb.conf.old
	fi
	if [ ! -f /opt/halsign/wlb/DwmRestWebSvc.exe ] || grep -q 'REST_ENABLED=0' /etc/init.d/workloadbalancing ; then
		touch  /tmp/wlb/rest_disabled
	fi
fi

%post
rm -f /etc/localtime
echo Halsign Workload Balancing Virtual Appliance	v%{version} > /etc/issue
echo "export PATH=$PATH:/opt/halsign/wlb" > /root/.bash_profile
#turn off memory overcommit
if grep -q 'vm.overcommit_memory' /etc/sysctl.conf
then
	sed -i 's/.*\(vm.overcommit_memory\).*/\1 = 2/g' /etc/sysctl.conf
else
	echo  "vm.overcommit_memory = 2" >> /etc/sysctl.conf
fi
if [ "$1" = "1" ]; then
	# Perform tasks to prepare for the initial installation
	#iptables start at boot
	chkconfig iptables on
	# DB_SCHEMA_VERSION=$(seq 100 | xargs -n1 -i find /opt/halsign/wlb/update/ -name "schemaUpdate_{}.sql" | tail -1 | awk -F_ '{print $2}' | awk -F. '{print $1}')
	# if [ -n "${DB_SCHEMA_VERSION}" ]; then
	# 	sed -i "s/DBSchemaVersion[ ]*=[ ]*.*/DBSchemaVersion = ${DB_SCHEMA_VERSION}/g" /opt/halsign/wlb/wlb.conf
	# fi
    rm -rf /opt/halsign/wlb/update
    echo "Finished installing WLB."
elif [ "$1" = "2" ]; then
	if [ -f /tmp/wlb/rest_disabled ]; then
		#disable rest
		sed -i 's/\(REST_ENABLED=\)[01]/\10/g' /etc/init.d/workloadbalancing
		sed -i 's/\(REST_ENABLED=\)[01]/\10/g' /opt/halsign/wlb/wlb-watchdog.sh		
	fi
	#Migrate old configurations
	if [ -f /opt/halsign/wlb/wlb.conf.old ]; then
		cd /opt/halsign/wlb
		mono UpdateUtils.exe -c wlb.conf.old wlb.conf
	fi
    #Migrate database, database update scripts are in update subdirectory
    cd /opt/halsign/wlb
    mono UpdateUtils.exe -d $PWD"/update"
	#delete temp file
	rm -f /tmp/wlb/rest_disabled
	rm -f /opt/halsign/wlb/wlb.conf.old
	rm -f /opt/halsign/wlb/wlb_db.out	
	#apply the setup script patch
	/etc/init.d/vpx_setup_patch.sh
	#restart the services after upgrade has finished
	service postgresql-9.0 restart	
	service workloadbalancing start
    echo "Finished updating WLB."
fi

%clean
echo Cleaning buildroot:%{buildroot}
rm -rf %{buildroot}

%files
%defattr(-,root,root,-)
/etc/init.d/vpx_startup_setup.sh
/etc/init.d/vpx_setup_patch.sh
/etc/init.d/workloadbalancing
/etc/rc2.d/S99vpx_startup_setup.sh
/etc/rc3.d/S99vpx_startup_setup.sh
/etc/rc4.d/S99vpx_startup_setup.sh
/etc/rc5.d/S99vpx_startup_setup.sh
/var/spool/cron/root
%dir /opt/halsign/wlb
%config /etc/sysconfig/iptables
%config /etc/stunnel/stunnel.conf
/etc/logrotate.d/postgres
# this will handle all subdirs
/opt/halsign/wlb/*



%changelog
* Thu Apr 3 2015 Kun Lu
- init

