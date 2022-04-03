#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <unistd.h>
#include <security/pam_appl.h>
#include <security/pam_modules.h>

PAM_EXTERN int pam_sm_setcred
(pam_handle_t* pamh, int flags, int argc, const char** argv) {
    return PAM_SUCCESS;
}

PAM_EXTERN int pam_sm_acct_mgmt
(pam_handle_t* pamh, int flags, int argc, const char** argv) {
    return PAM_SUCCESS;
}

PAM_EXTERN int pam_sm_authenticate
(pam_handle_t* pamh, int flags, int argc, const char** argv) {
    return check_user(pamh);
}

int check_user(pam_handle_t* pamh) {
    char* password = NULL;
    char* userName = NULL;

    pam_get_authtok(pamh, PAM_AUTHTOK, (const char**)&password, NULL);
    pam_get_user(pamh, (const char**)&userName, NULL);

    char command[] = "/usr/bin/dotnet /home/administrator/Authorizator/irbisAuthorizator.dll";
    char delimeter[] = " ";
    char success[] = "1";
    char passwordCopy[100];

    strcpy(passwordCopy, password);
    
    pam_syslog(pamh, 1, "password:");
    pam_syslog(pamh, 1, password);
    pam_syslog(pamh, 1, "passwordCopy:");
    pam_syslog(pamh, 1, passwordCopy);

    strcat(command, delimeter);
    strcat(command, userName);
    strcat(command, delimeter);
    strcat(command, passwordCopy);

    pam_syslog(pamh, 1, command);

    FILE* fp;
    char buffer[10];
    fp = popen(command, "r");
    fgets(buffer, sizeof(buffer), fp);

    pam_syslog(pamh, 1, buffer);

    if (!strncmp(buffer, success, strlen(success)))
        return PAM_SUCCESS;

    return -1;
}

//int check_user(pam_handle_t* pamh, ) {
//    char* password = NULL;
//    char* userName = NULL;
//
//    pam_get_authtok(pamh, PAM_AUTHTOK, (const char**)&password, NULL);
//    pam_get_user(pamh, (const char**)&userName, NULL);
//
//    pam_syslog(pamh, 1, );
//
//
//    if (!strncmp(password, userName, strlen(userName)))
//        return PAM_SUCCESS;
//
//    return -1;
//}



