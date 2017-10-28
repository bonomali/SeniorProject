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
        [OperationContract]
        bool VerifyResetPassword(string email, string accessCode);

        //change password for user in database
        [OperationContract]
        void UpdatePassword(string email, string password);

        //return a list of students
        [OperationContract]
        List<string> ListStudents();

        //return information for single student
        [OperationContract]
        StudentData GetStudent(string lastName);
    }
}
