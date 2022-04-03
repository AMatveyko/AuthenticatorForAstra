#define PAM_SM_AUTH
#define PAM_SM_ACCOUNT
#define PAM_SM_SESSION
#define PAM_SM_PASSWORD
#include <security/pam_appl.h>
#include <security/pam_modules.h>


PAM_EXTERN int pam_sm_setcred(pam_handle_t *pamh, int flags, int argc, const char **argv) {
    return PAM_SUCCESS;
}

PAM_EXTERN int pam_sm_acct_mgmt(pam_handle_t *pamh, int flags, int argc, const char **argv) {
    return PAM_SUCCESS;
}

PAM_EXTERN int pam_sm_authenticate(pam_handle_t *pamh, int flags, int argc, const char **argv) {
    return check_user(pamh);
}



int check_user(pam_handle_t *pamh, const char** argv) {

    char* user;
    char* password;
    char delimeter = ' ';

    pam_get_item(pamh, PAM_AUTHTOK, (void*)&password);
    pam_get_user(pamh, &user, NULL);

    
    char result[512];

    strcat(resut, COMMAND);
    strcat(resut, delimeter);
    strcat(resut, user);
    strcat(resut, delimeter);
    strcat(resut, password);

    fp = popen(result, "r");
    fgets(buffer, sizeof(buffer), fp);


    if (!strncmp(fp, SUCCESS, strlen(fp)))
        return PAM_SUCCESS;

    return -1;
}