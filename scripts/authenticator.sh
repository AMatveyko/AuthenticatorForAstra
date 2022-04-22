#!/bin/bash

read password

result=$(</usr/bin/dotnet /etc/Authorizator/PamAuthenticator.dll authentication $PAM_TYPE $PAM_USER $password debug)

if [[ $result == "success" ]]; then
 exit 0
else
 /bin/echo $result >> /var/log/authen.log
 exit 1
fi

