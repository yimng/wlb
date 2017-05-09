#!/bin/bash

wlbpath="/opt/halsign/wlb/"
wlbconfpath=$wlbpath"wlb.conf"
wlblogdir="/var/log/wlb/"
logfile=$wlblogdir"wlb_install_log.log"
pg_conf="/var/lib/pgsql/9.0/data/postgresql.conf"

#change default logging configurations. log rotation will be handled by logrotate
sed -i -e "s/\(log_filename\).*/\1 = 'postgresql.log'/" $pg_conf
sed -i -e "s/\(log_truncate_on_rotation.*\)/#\1/" $pg_conf
sed -i -e "s/\(log_rotation_age\).*/\1 = 0/" $pg_conf
sed -i -e "s/\(log_rotation_size\).*/\1 = 0/" $pg_conf

#run logrotate every 10 mins
if grep -q 'logrotate' /etc/crontab; then
	sed -i 's#.*logrotate.*#'"*/10 * * * * root /usr/sbin/logrotate /etc/logrotate.conf"'#g' /etc/crontab
else
	echo "*/10 * * * * root /usr/sbin/logrotate /etc/logrotate.conf" >> /etc/crontab
fi

cd $wlbpath
mono ReportImport.exe -v --version tampa -p ./Reports/tampa >> $logfile 2>&1
mono ReportImport.exe -v --version creedence -p ./Reports/creedence >> $logfile 2>&1
