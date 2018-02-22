using System.Collections.Generic;
using System.ServiceModel;

namespace SchoolToHomeBehaviorTracking_Interface
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWCFService" in both code and config file together.
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

        //return a list of students for teacher
        //return list of students
        [OperationContract]
        List<string> ListStudents(string email);

        //return information for single student
        [OperationContract]
        StudentData GetStudent(string fname, string lname);

        //return teacher's username
        [OperationContract]
        string GetTeacherUserName(string email);

        //return parent's username
        [OperationContract]
        string GetParentUserName(string email);

        //return admin user's last access date
        [OperationContract]
        string GetAdminAccessDate(string email);

        //return teacher user's last access date
        [OperationContract]
        string GetTeacherAccessDate(string email);

        //return parent user's last access date
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
        //return true on success, false on failure
        [OperationContract]
        bool UpdateEmail(string oldEmail, string newEmail);

        //Lookup access code by teacher name
        //Return access code on success, -1 on failure
        [OperationContract]
        int AccessCodeLookup(string name);

        //Add student to teacher account
        //Return teacher code on success, -1 on failure
        [OperationContract]
        int AddStudent(string email, StudentData stud);

        //Update teacher username
        //return true on success, false on failure
        [OperationContract]
        bool UpdateTeacherUserName(string email, string newUserName);

        //Update teacher username
        //return true on success, false on failure
        [OperationContract]
        bool UpdateParentUserName(string email, string newUserName);

        //Delete a student
        //Return true on success, false on failure
        [OperationContract]
        bool DeleteStudent(string email, string fname, string lname);

        //Update student
        //Also deletes parent account if parent has no other students
        //Return true on success, false on failure
        [OperationContract]
        bool UpdateStudent(string email, string fname, string lname, StudentData stud);

        //get list of teacher use form names by category (behavior or other)
        [OperationContract]
        List<string> GetTeacherForms(string category);

        //get list of child daily home tracking forms
        [OperationContract]
        List<string> GetChildDailyForms(string fname, string lname);

        //add a form to a student
        //return true on success, false on failure
        [OperationContract]
        bool AddFormToStudent(string form, string description, string fname, string lname);

        //get list of forms for a student by category (behavior, other)
        [OperationContract]
        List<string> GetStudentForms(string fname, string lname, string category);

        //remove a tracking form for student
        //return true on success, false on failure
        [OperationContract]
        bool RemoveForm(string form, string fname, string lname);

        //save student tracking form
        //return true on success, false on failure
        [OperationContract]
        bool SaveStudentForm(StudentFormData form);

        //get list of daily behavior tracking forms
        [OperationContract]
        List<string> GetStudentDailyForms(string fname, string lname);

        //get form description for student form
        [OperationContract]
        string GetStudentFormDescription(string fname, string lname, string formName);

        //return a list of children for parent
        //return list of children
        [OperationContract]
        List<string> ListChildren(string email);

        //return a completed for for student
        //returns form completed that day or null if no form exists
        [OperationContract]
        StudentFormData GetDailyForm(string formName, string fname, string lname);

        //Get list of forms by type for a student for teacher viewing
        [OperationContract]
        List<StudentFormData> GetStudentFormsListByType(string form, string fname, string lname);

        //Get list of forms by type for a child for parent viewing
        [OperationContract]
        List<StudentFormData> GetChildFormsListByType(string form, string fname, string lname);

        //Get form by id for teacher viewing
        [OperationContract]
        StudentFormData GetTeacherViewableFormByID(string formID);

        //Get form by id for parent viewing
        [OperationContract]
        StudentFormData GetParentViewableFormByID(string formID);

        //Get graphing data for behavior forms for a specified time period
        [OperationContract]
        List<List<string>> GetBehaviorGraphingData(string formName, string fname, string lname, string startDate, string endDate);

        //Get graphing data for incident forms for a specified time period
        [OperationContract]
        List<List<string>> GetIncidentGraphingData(string fname, string lname, string startDate, string endDate);

        //Get graphing data for home tracking forms for a specified time period
        [OperationContract]
        List<List<string>> GetHomeTrackingGraphingData(string fname, string lname, string startDate, string endDate);
    }
}
