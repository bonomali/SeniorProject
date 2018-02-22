using System.Collections.Generic;
using System.ServiceModel;

namespace SchoolToHomeBehaviorTracking_Interface
{
    [ServiceContract]
    public interface ITeacherDashService
    {
        //return teacher's username
        [OperationContract]
        string GetTeacherUserName(string email);

        //return teacher user's last access date
        [OperationContract]
        string GetTeacherAccessDate(string email);

        //update teacher user's last access
        [OperationContract]
        void UpdateTeacherLastAccess(string email);

        //Update teacher username
        //return true on success, false on failure
        [OperationContract]
        bool UpdateTeacherUserName(string email, string newUserName);

        //Add student to teacher account
        //Return teacher code on success, -1 on failure
        [OperationContract]
        int AddStudent(string email, StudentData stud);

        //Delete a student
        //Return true on success, false on failure
        [OperationContract]
        bool DeleteStudent(string email, string fname, string lname);

        //Update student
        //Also deletes parent account if parent has no other students
        //Return true on success, false on failure
        [OperationContract]
        bool UpdateStudent(string email, string fname, string lname, StudentData stud);

        //return a list of students for teacher
        //return list of students
        [OperationContract]
        List<string> ListStudents(string email);

        //return information for single student
        [OperationContract]
        StudentData GetStudent(string fname, string lname);
    }
}
