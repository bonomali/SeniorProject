using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SchoolToHomeBehaviorTracking_Interface
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWCFService1" in both code and config file together.
    [ServiceContract]
    public interface IWCFService
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

        //return a list of students
        [OperationContract]
        List<string> ListStudents();

        //return information for single student
        [OperationContract]
        StudentData GetStudent(string lastName);

        [OperationContract]
        string GetTeacherUserName(string email);

        [OperationContract]
        string GetParentUserName(string email);

        [OperationContract]
        string GetAdminAccessDate(string email);

        [OperationContract]
        string GetTeacherAccessDate(string email);

        [OperationContract]
        string GetParentAccessDate(string email);

        //update admin user's last access
        [OperationContract]
        void UpdateAdminLastAccess(string email);

        //update teacher user's last access
        [OperationContract]
        void UpdateTeacherLastAccess(string email);

        //update parent user's last access
        [OperationContract]
        void UpdateParentLastAccess(string email);

        //Add teacher to database through admin account, generate access code
        //return teacher access code
        [OperationContract]
        int AddTeacher(string fname, string lname);

        //Remove link between teacher and teacher account
        //Keep teacher and student data in database for archieve reasons
        //Return true on success, false on failure
        [OperationContract]
        bool RemoveTeacher(string fname, string lname);

        //Update user's email
        //retur true on success, false on failure
        [OperationContract]
        bool UpdateEmail(string oldEmail, string newEmail);

        //Lookup access code by teacher name
        //Return access code on success, -1 on failure
        [OperationContract]
        int AccessCodeLookup(string name);
    }
}
