using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace SchoolToHomeBehaviorTracking_Interface
{
    [ServiceContract]
    public interface IFormsService
    {
        //add a form to a student
        //return true on success, false on failure
        [OperationContract]
        bool AddFormToStudent(string form, string description, string fname, string lname);

        //remove a tracking form for student
        //return true on success, false on failure
        [OperationContract]
        bool RemoveForm(string form, string fname, string lname);

        //save student tracking form
        //return true on success, false on failure
        [OperationContract]
        bool SaveStudentForm(StudentFormData form);

        //get list of forms for a student by category (behavior, other)
        [OperationContract]
        List<string> GetStudentFormsByCategory(string fname, string lname, string category);

        //get list of teacher use form names by category (behavior or other)
        [OperationContract]
        List<string> GetAllTeacherForms(string category);

        //get list of child daily home tracking forms
        [OperationContract]
        List<string> GetChildDailyForms(string fname, string lname);

        //get list of daily behavior tracking forms
        [OperationContract]
        List<string> GetStudentDailyForms(string fname, string lname);

        //get form description for student form
        [OperationContract]
        string GetStudentFormDescription(string fname, string lname, string formName);

        //return a completed for for student
        //returns form completed that day or null if no form exists
        [OperationContract]
        StudentFormData GetCompletedDailyForm(string formName, string fname, string lname);

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
