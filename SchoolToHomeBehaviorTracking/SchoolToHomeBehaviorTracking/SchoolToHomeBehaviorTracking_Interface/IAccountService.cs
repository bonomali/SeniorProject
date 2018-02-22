using System.ServiceModel;

namespace SchoolToHomeBehaviorTracking_Interface
{
    //Interface for logging into application and managing accounts
    [ServiceContract]
    public interface IAccountService
    {
        //validate login credentials
        //return true on success, false on failure
        [OperationContract]
        bool Login(string email, string password);

        //create a new user account
        //return true on success, false on failure
        [OperationContract]
        bool CreateUser(string email, string password);

        //validate user's email to create an account
        //return true on success, false on failure
        [OperationContract]
        bool ValidateEmail(string email);

        //Update user's email
        //return true on success, false on failure
        [OperationContract]
        bool UpdateEmail(string oldEmail, string newEmail);

        //validate user's password to create an account
        //return true on success, false on failure
        [OperationContract]
        bool ValidatePassword(string password);

        //validate that password fields match for account creation
        //return true on success, false on failure
        [OperationContract]
        bool ValidateMatchingPasswords(string p1, string p2);

        //send email
        //return true on success, false on failure
        [OperationContract]
        bool ResetPassword(string email);

        //verify correct access code was entered to reset password
        //return true on success, false on failure
        [OperationContract]
        bool VerifyResetPassword(string email, string accessCode);

        //change password for user in database
        [OperationContract]
        void UpdatePassword(string email, string password);

        //verify that valid access code entered for a teacher account
        //return true on success, false on failure
        [OperationContract]
        bool ValidateTeacherAccessCode(int code);

        //verify that valid teacher code entered for a teacher account
        //return true on success, false on failure
        [OperationContract]
        bool ValidateParentTeacherCode(int code);

        //Add teacher account
        //return true on success, false on failure
        [OperationContract]
        bool AddTeacherAccount(string email, int code, string username);

        //add parent account
        //return true on success, false on failure
        [OperationContract]
        bool AddParentAccount(string email, string username);

        //add student to existing parent account
        //return true on success, false on failure
        [OperationContract]
        bool AddStudentToParentAccount(string email, int teachercode);

        //checks if user has an admin account
        //return true if user has admin account, false if no admin account
        [OperationContract]
        bool ExistingAdminAccount(string email);

        //checks if user has a teacher account
        //return true if user has teacher account, false if no teacher account
        [OperationContract]
        bool ExistingTeacherAccount(string email);

        //checks if user has a parent account
        //return true if user has parent account, false if no parent account
        [OperationContract]
        bool ExistingParentAccount(string email);
    }
}
