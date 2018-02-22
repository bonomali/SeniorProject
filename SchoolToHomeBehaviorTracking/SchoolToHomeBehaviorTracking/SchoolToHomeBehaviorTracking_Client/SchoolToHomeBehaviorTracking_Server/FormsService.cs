using SchoolToHomeBehaviorTracking_Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolToHomeBehaviorTracking_Server
{
    public partial class SchoolToHomeService : IMetaInterface
    {
        private static string BEHAVIORFORMS = "Behavior";
        private static string HOMEFORMS = "Home";
        private static string HOMETRACKINGFORM = "Home Tracking Form";
        private static string PROGRESSREPORTFORM = "Progress Report Form";
        private static string INCIDENTFORM = "Incident Form";
        private static string OTHERFORMS = "Other";
        private static string CUSTOMFORM = "Custom Behavior Tracking";
        private static string TEMPLATE = "Template";
        private static string SEPERATOR = ":?:";
        private static string DATEFORMAT = "MM/dd/yyyy";

        //return list of teacher behavior traking forms by category (behavior or other)
        public List<string> GetAllTeacherForms(string category)
        {
            List<string> formList = new List<string>();
            try
            {

                using (test_dbEntities db = new test_dbEntities())
                {
                    var forms = db.forms.ToList();

                    if (category == BEHAVIORFORMS)
                    {
                        foreach (var f in forms)
                        {
                            if (f.Category == BEHAVIORFORMS)
                                formList.Add(f.FormName);
                        }
                    }
                    else if (category == OTHERFORMS)
                    {
                        foreach (var f in forms)
                        {
                            if (f.Category != BEHAVIORFORMS && f.Category != HOMEFORMS)
                                formList.Add(f.FormName);
                        }
                    }
                    else  //return all forms
                    {
                        foreach (var f in forms)
                            formList.Add(f.FormName);
                    }
                }
                formList.Sort();
            }
            catch
            {
            }
            return formList;
        }

        //return list of child's daily home tracking forms
        public List<string> GetChildDailyForms(string fname, string lname)
        {
            List<string> forms = new List<string>();
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var student = db.students.First((s) => s.FirstName == fname && s.LastName == lname);
                    var studForms = db.studentforms.Where((f) => f.StudentID == student.StudentID && f.FormData == TEMPLATE);

                    using (test_dbEntities db2 = new test_dbEntities())
                    {
                        var completeForms = db2.studentforms.Where((c) => c.StudentID == student.StudentID && c.FormData != TEMPLATE);

                        using (test_dbEntities db3 = new test_dbEntities())
                        {
                            foreach (var x in studForms)
                            {
                                var formTemplate = db3.forms.First((y) => y.FormID == x.FormID);
                                if (formTemplate.Category == HOMEFORMS)
                                    forms.Add(x.FormName);
                            }
                            foreach (var c in completeForms)
                            {
                                if (c.FormData != TEMPLATE && c.FormDate == DateTime.Now.ToString(DATEFORMAT))
                                    forms.Remove(c.FormName);
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            return forms;
        }

        //add a tracking form to student   
        //return true on success, false on failure                                                 
        public bool AddFormToStudent(string form, string description, string fname, string lname)
        {
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var forms = db.forms.FirstOrDefault((f) => f.FormName == form && f.FormName != CUSTOMFORM);
                    var student = db.students.First((s) => s.FirstName == fname && s.LastName == lname);

                    studentform stof = new studentform();
                    //custom form
                    if (forms == null)
                    {
                        var f = db.forms.FirstOrDefault((x) => x.FormName == CUSTOMFORM);
                        stof.FormID = f.FormID;
                        stof.Description = description;

                        //check that custom form with same name doesn't already exist
                        var result = db.studentforms.FirstOrDefault((y) => y.FormName == form);
                        if (result != null)
                            return false;
                    }
                    else
                    {
                        //check if student already has form template
                        var result = db.studentforms.Where(x => x.FormID == forms.FormID &&
                                                            x.StudentID == student.StudentID
                                                      && x.FormData == TEMPLATE).FirstOrDefault();
                        if (result != null)
                            return false;

                        stof.FormID = forms.FormID;
                    }
                    stof.StudentID = student.StudentID;
                    stof.FormName = form;
                    stof.FormDate = DateTime.Now.ToString(DATEFORMAT);
                    stof.FormData = TEMPLATE;

                    db.studentforms.Add(stof);
                    db.SaveChanges();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        //return list of tracking forms for student by category (behavior, other)
        public List<string> GetStudentFormsByCategory(string fname, string lname, string category)
        {
            List<string> formList = new List<string>();
            try
            {

                using (test_dbEntities db = new test_dbEntities())
                {
                    var student = db.students.First((s) => s.FirstName == fname && s.LastName == lname);
                    var forms = db.studentforms.Where((f) => f.StudentID == student.StudentID && f.FormData == TEMPLATE);

                    using (test_dbEntities database = new test_dbEntities())
                    {
                        if (category == BEHAVIORFORMS)
                        {
                            foreach (var f in forms)
                            {
                                var form = database.forms.FirstOrDefault((x) => x.FormID == f.FormID && x.Category == BEHAVIORFORMS);
                                if (form != null)
                                    formList.Add(f.FormName);
                            }
                        }
                        else if (category == OTHERFORMS)
                        {
                            foreach (var f in forms)
                            {
                                var form = database.forms.FirstOrDefault((x) => x.FormID == f.FormID && x.Category != BEHAVIORFORMS
                                                                            && x.Category != HOMEFORMS);
                                if (form != null)
                                    formList.Add(f.FormName);
                            }
                        }
                        else  //return all forms for student
                        {
                            foreach (var f in forms)
                                formList.Add(f.FormName);
                        }
                    }
                }
                formList.Sort();
            }
            catch
            {
            }
            return formList;
        }

        //remove a tracking form for student
        //return true on success, false on failure
        public bool RemoveForm(string form, string fname, string lname)
        {
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var student = db.students.First((s) => s.FirstName == fname && s.LastName == lname);

                    studentform stof = new studentform();
                    stof = (from s in db.studentforms
                            where s.StudentID == student.StudentID && s.FormName == form
                            select s).FirstOrDefault();

                    db.studentforms.Remove(stof);
                    db.SaveChanges();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        //save student form data
        //return true on success, false on failure
        public bool SaveStudentForm(StudentFormData form)
        {
            try
            {
                SecureData encrypt = new SecureData();
                using (test_dbEntities db = new test_dbEntities())
                {
                    var student = db.students.First((s) => s.FirstName == form.StudentFName && s.LastName == form.StudentLName);
                    var forms = db.forms.FirstOrDefault((f) => f.FormName == form.FormName);

                    studentform studForm = new studentform();
                    studForm.StudentID = student.StudentID;

                    if (forms == null) //custom form
                    {
                        var f = db.forms.FirstOrDefault((x) => x.FormName == CUSTOMFORM);
                        studForm.FormID = f.FormID;
                    }
                    else
                        studForm.FormID = forms.FormID;

                    studForm.FormName = form.FormName;
                    studForm.FormDate = DateTime.Now.ToString(DATEFORMAT);
                    studForm.EndDate = form.EndDate;
                    studForm.FormData = encrypt.Encrypt(form.Data);
                    studForm.Shared = form.Shared;
                    studForm.BehaviorRating = form.BehaviorRating;

                    db.studentforms.Add(studForm);
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        //return list of daily behavior tracking forms for student
        //only return list of forms that haven't been completed for the day
        public List<string> GetStudentDailyForms(string fname, string lname)
        {
            List<string> forms = new List<string>();
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var student = db.students.First((s) => s.FirstName == fname && s.LastName == lname);
                    var studForms = db.studentforms.Where((f) => f.StudentID == student.StudentID && f.FormData == TEMPLATE);

                    using (test_dbEntities db2 = new test_dbEntities())
                    {
                        var completeForms = db2.studentforms.Where((c) => c.StudentID == student.StudentID && c.FormData != TEMPLATE);

                        using (test_dbEntities db3 = new test_dbEntities())
                        {
                            foreach (var x in studForms)
                            {
                                var formTemplate = db3.forms.First((y) => y.FormID == x.FormID);
                                if (formTemplate.Category == BEHAVIORFORMS)
                                    forms.Add(x.FormName);
                            }
                            foreach (var c in completeForms)
                            {
                                if (c.FormData != TEMPLATE && c.FormDate == DateTime.Now.ToString(DATEFORMAT))
                                    forms.Remove(c.FormName);
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            return forms;
        }

        //get description of student form
        public string GetStudentFormDescription(string fname, string lname, string formName)
        {
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var student = db.students.First((s) => s.FirstName == fname && s.LastName == lname);
                    var studForms = db.studentforms.First((f) => f.StudentID == student.StudentID && f.FormName == formName);

                    return studForms.Description;
                }
            }
            catch
            {
                return null;
            }
        }

         //return a completed form for student
        //returns form completed that day or null if no form exists
        public StudentFormData GetCompletedDailyForm(string formName, string fname, string lname)
        {
            StudentFormData targetForm = new StudentFormData();
            try
            {
                string date = DateTime.Now.ToString(DATEFORMAT);
                using (test_dbEntities db = new test_dbEntities())
                {
                    var student = db.students.First((s) => s.FirstName == fname && s.LastName == lname);
                    var form = db.studentforms.FirstOrDefault((f) => f.StudentID == student.StudentID &&
                                               f.FormName == formName && f.Shared == true && f.FormData != TEMPLATE
                                               && f.FormDate == date);
                    //no form exists
                    if (form == null)
                        targetForm = null;
                    else
                    {
                        SecureData decrypt = new SecureData();

                        targetForm.FormName = form.FormName;
                        targetForm.Data = decrypt.Decrypt(form.FormData);
                        targetForm.StudentFName = student.FirstName;
                        targetForm.StudentLName = student.LastName;
                        targetForm.FormDescription = GetStudentFormDescription(student.FirstName, student.LastName, form.FormName);
                        targetForm.Shared = form.Shared;
                    }
                }
            }
            catch
            {
                return null;
            }
            return targetForm;
        }

        //get list of forms by type for a student for teacher viewing
        public List<StudentFormData> GetStudentFormsListByType(string form, string fname, string lname)
        {
            List<StudentFormData> trackingForms = new List<StudentFormData>();
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var student = db.students.First((s) => s.FirstName == fname && s.LastName == lname);
                    var forms = db.studentforms.Where((f) => f.StudentID == student.StudentID && f.FormName == form
                                                            && f.FormData != TEMPLATE);

                    foreach (var x in forms)
                    {
                        StudentFormData f = new StudentFormData();
                        f.FormName = x.FormName;
                        f.FormDate = x.FormDate;
                        f.EndDate = x.EndDate;
                        f.FormID = x.StudentFormID.ToString();
                        f.Shared = x.Shared;
                        f.FormDescription = GetStudentFormDescription(student.FirstName, student.LastName, x.FormName);   
                        if (x.FormName == HOMETRACKINGFORM)
                        {
                            if (x.Shared)
                                trackingForms.Add(f);
                        }
                        else
                            trackingForms.Add(f);
                    }
                    trackingForms.OrderBy(x => x.FormDate);
                }
            }
            catch
            {
            }
            return trackingForms;
        }

        //get list of forms by type for a child for parent viewing
        public List<StudentFormData> GetChildFormsListByType(string form, string fname, string lname)
        {
            List<StudentFormData> trackingForms = new List<StudentFormData>();
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var student = db.students.First((s) => s.FirstName == fname && s.LastName == lname);
                    var forms = db.studentforms.Where((f) => f.StudentID == student.StudentID && f.FormName == form
                                                            && f.FormData != TEMPLATE);

                    foreach (var x in forms)
                    {
                        if (x.FormName == HOMETRACKINGFORM || (x.FormName == PROGRESSREPORTFORM && x.Shared))
                        {
                            StudentFormData f = new StudentFormData();
                            f.FormName = x.FormName;
                            f.FormDate = x.FormDate;
                            f.FormID = x.StudentFormID.ToString();
                            f.Shared = x.Shared;

                            trackingForms.Add(f);
                        }
                    }
                    trackingForms.OrderBy(x => x.FormDate);
                }
            }
            catch
            {
            }
            return trackingForms;
        }

        //return a form by id for teacher viewing
        public StudentFormData GetTeacherViewableFormByID(string formID)
        {
            StudentFormData targetForm = new StudentFormData();
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    int id = Convert.ToInt32(formID);
                    var form = db.studentforms.First((f) => f.StudentFormID == id);

                    SecureData decrypt = new SecureData();
                    targetForm.FormName = form.FormName;
                    targetForm.Shared = form.Shared;
                    targetForm.Data = decrypt.Decrypt(form.FormData);
                    
                    if (targetForm.FormName == HOMETRACKINGFORM && !form.Shared)
                        targetForm = null;
                }
            }
            catch
            {
                return null;
            }
            return targetForm;
        }

        //return a form by id for parent viewing
        public StudentFormData GetParentViewableFormByID(string formID)
        {
            StudentFormData targetForm = new StudentFormData();
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    int id = Convert.ToInt32(formID);
                    var form = db.studentforms.First((f) => f.StudentFormID == id);

                    SecureData decrypt = new SecureData();

                    targetForm.FormName = form.FormName;
                    targetForm.Shared = form.Shared;
                    targetForm.Data = decrypt.Decrypt(form.FormData);

                    if (targetForm.FormName == PROGRESSREPORTFORM && !form.Shared)
                        targetForm = null;
                }
            }
            catch
            {
                return null;
            }
            return targetForm;
        }

        //Return graphing data for behavior tracking forms for a specified time period
        public List<List<string>> GetBehaviorGraphingData(string formName, string fname, string lname, string startDate, string endDate)
        {
            List<string> dates = new List<string>();
            List<string> rating = new List<string>();
            List<string> timeSlept = new List<string>();
            List<string> formID = new List<string>();
            List<List<string>> data = new List<List<string>>();

            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var student = db.students.First((s) => s.FirstName == fname && s.LastName == lname);
                    var forms = db.studentforms.Where((f) => f.StudentID == student.StudentID && f.FormName == formName);

                    foreach(var f in forms)
                    {
                        DateTime date = Convert.ToDateTime(f.FormDate);
                        DateTime sDate = Convert.ToDateTime(startDate);
                        DateTime eDate = Convert.ToDateTime(endDate);

                        if (date.CompareTo(sDate) >= 0 && date.CompareTo(eDate) <= 0 && f.FormData != TEMPLATE)
                        {
                            //do not add home tracking form unless shared
                            if (f.FormName == HOMETRACKINGFORM && !f.Shared) { }
                            else
                            {
                                dates.Add(f.FormDate);
                                rating.Add(f.BehaviorRating.ToString());

                                //calculate sleep time
                                SecureData decrypt = new SecureData();
                                string[] delimiter = new string[] { SEPERATOR };
                                string[] formData = (decrypt.Decrypt(f.FormData)).Split(delimiter, StringSplitOptions.None);
                                try
                                {
                                    TimeSpan diff = DateTime.Parse(formData[6]).Subtract(DateTime.Parse(formData[5]));
                                    double elapsedTime = Convert.ToDouble(diff.TotalMinutes);
                                    double minsInDay = 24 * 60;
                                    double sleepTime = (minsInDay - elapsedTime) / 60;
                                    if (sleepTime > 24)
                                        sleepTime = sleepTime - 24;
                                    timeSlept.Add(sleepTime.ToString());
                                }
                                catch
                                {
                                    timeSlept.Add("0");
                                }

                                formID.Add(f.StudentFormID.ToString());
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            data.Add(dates);
            data.Add(rating);
            data.Add(timeSlept);
            data.Add(formID);
            return data;
        }

        //Return incident form graphing data
        public List<List<string>> GetIncidentGraphingData(string fname, string lname, string startDate, string endDate)
        {
            List<string> dates = new List<string>();
            List<string> numIncidents = new List<string>();
            List<List<string>> data = new List<List<string>>();

            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    using (test_dbEntities db2 = new test_dbEntities())
                    {
                        var student = db.students.First((s) => s.FirstName == fname && s.LastName == lname);
                        var forms = db.studentforms.Where((f) => f.StudentID == student.StudentID && f.FormName == INCIDENTFORM);
                        var forms2 = db2.studentforms.Where((f) => f.StudentID == student.StudentID && f.FormName == INCIDENTFORM);

                        foreach (var f in forms)
                        {
                            DateTime date = Convert.ToDateTime(f.FormDate);
                            DateTime sDate = Convert.ToDateTime(startDate);
                            DateTime eDate = Convert.ToDateTime(endDate);
                            int counter = 0;

                            if (date.CompareTo(sDate) >= 0 && date.CompareTo(eDate) <= 0 && f.FormData != TEMPLATE)
                            {
                                foreach (var x in forms2)
                                {
                                    if (f.FormDate == x.FormDate)
                                        counter++;
                                }
                                if (!dates.Contains(f.FormDate))
                                {
                                    dates.Add(f.FormDate);
                                    numIncidents.Add(counter.ToString());
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            data.Add(dates);
            data.Add(numIncidents);
            return data;
        }

        //Get graphing data for home tracking forms for a specified time period
        public List<List<string>> GetHomeTrackingGraphingData(string fname, string lname, string startDate, string endDate)
        {
            List<string> dates = new List<string>();
            List<string> rating = new List<string>();
            List<string> timeSlept = new List<string>();
            List<string> formID = new List<string>();
            List<List<string>> data = new List<List<string>>();

            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var student = db.students.First((s) => s.FirstName == fname && s.LastName == lname);
                    var forms = db.studentforms.Where((f) => f.StudentID == student.StudentID && f.FormName == HOMETRACKINGFORM);

                    foreach (var f in forms)
                    {
                        DateTime date = Convert.ToDateTime(f.FormDate);
                        DateTime sDate = Convert.ToDateTime(startDate);
                        DateTime eDate = Convert.ToDateTime(endDate);

                        if (date.CompareTo(sDate) >= 0 && date.CompareTo(eDate) <= 0 && f.FormData != TEMPLATE)
                        {
                            dates.Add(f.FormDate);
                            rating.Add(f.BehaviorRating.ToString());

                            //calculate sleep time
                            SecureData decrypt = new SecureData();
                            string[] delimiter = new string[] { SEPERATOR };
                            string[] formData = (decrypt.Decrypt(f.FormData)).Split(delimiter, StringSplitOptions.None);
                            try
                            {
                                TimeSpan diff = DateTime.Parse(formData[6]).Subtract(DateTime.Parse(formData[5]));
                                double elapsedTime = Convert.ToDouble(diff.TotalMinutes);
                                double minsInDay = 24 * 60;
                                double sleepTime = (minsInDay - elapsedTime) / 60;
                                if (sleepTime > 24)
                                    sleepTime = sleepTime - 24;
                                timeSlept.Add(sleepTime.ToString());
                            }
                            catch
                            {
                                timeSlept.Add("0");
                            }
                                                        
                            formID.Add(f.StudentFormID.ToString());
                        }
                    }
                }
            }
            catch
            {
            }
            data.Add(dates);
            data.Add(rating);
            data.Add(timeSlept);
            data.Add(formID);
            return data;
        }
    }
}
