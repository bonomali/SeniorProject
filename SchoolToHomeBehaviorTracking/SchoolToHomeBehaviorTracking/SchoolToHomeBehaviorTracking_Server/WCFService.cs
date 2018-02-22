using SchoolToHomeBehaviorTracking_Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SchoolToHomeBehaviorTracking_Server
{
    public class WCFService : IWCFService
    {
        private static string BEHAVIORFORMS = "Behavior";
        private static string HOMEFORMS = "Home";
        private static string HOMETRACKINGFORM = "Home Tracking Form";
        private static string PROGRESSREPORTFORM = "Progress Report Form";
        private static string INCIDENTFORM = "Incident Form";
        private static string OTHERFORMS = "Other";
        private static string CUSTOMFORM = "Custom Behavior Tracking";
        private static string TEMPLATE = "Template";
        private static string DATEFORMAT = "MM/dd/yyyy";

        //validate user account by email and password
        //return true on success, false on failure
        public bool Login(string email, string password)
        {
            bool found = false;
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    email = email.ToLower();
                    var user = db.users.First((e) => e.Email == email);
                    if (user.Password == EncryptPassword(password))
                        found = true;
                }
            }
            catch
            {
                return false;
            }
            return found;
        }

        //validate user's email for account creation
        //return true on success, false on failure
        public bool ValidateEmail(string email)
        {
            ValidateAccountCreation validate = new ValidateAccountCreation();
            return validate.IsValidEmail(email);
        }

        //validate user's password for account creation
        //return true on success, false on failure
        public bool ValidatePassword(string password)
        {
            ValidateAccountCreation validate = new ValidateAccountCreation();
            return validate.ValidatePassword(password);
        }

        //validate that passwords match
        //return true on success, false on failure
        public bool ValidateMatchingPasswords(string p1, string p2)
        {
            ValidateAccountCreation validate = new ValidateAccountCreation();
            return validate.MatchingPassword(p1, p2);
        }

        //create a new user account
        //return true on success, false on failure
        public bool CreateUser(string email, string password)
        {
            bool success = false;

            try
            {
                email = email.ToLower();
                using (test_dbEntities db = new test_dbEntities())
                {
                    user newUser = new user();
                    newUser.Email = email;
                    newUser.Password = EncryptPassword(password);

                    db.users.Add(newUser);
                    db.SaveChanges();

                    success = true;
                }
            }
            catch
            {
                return false;
            }
            return success;
        }

        //Generate access code for resetting password, replace password with code in database
        //return access code as string
        private static string GenerateAccessCode(string email)
        {
            string accessCode = null;
            Random rnd = new Random();

            for (int i = 0; i < 6; i++)
            {
                accessCode += Convert.ToChar(rnd.Next(65, 91));
            }

            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var user = db.users.First((e) => e.Email == email);
                    user.Code = accessCode;
                    user.Expiration = DateTime.Now.AddHours(24);

                    db.SaveChanges();
                }
            }
            catch
            {
                return null;
            }
            return accessCode;
        }

        //Send email to reset user password
        //Return value from SendEmail, true on success or false on failure
        public bool ResetPassword(string email)
        {
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var user = db.users.First((e) => e.Email == email);
                }
                SendEmail emailObj = new SendEmail();
                return emailObj.Send(email, GenerateAccessCode(email));
            }
            catch
            {
                return false;
            }
        }

        //verify the correct access code was entered for user to reset password
        //return true on success, false on failure
        public bool VerifyResetPassword(string email, string accessCode)
        {
            bool verified = false;
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var user = db.users.First((e) => e.Email == email);

                    if (user.Code == accessCode && user.Expiration > DateTime.Now)
                        verified = true;
                }
            }
            catch
            {
                return false;
            }
            return verified;
        }

        //update password for user in database
        public void UpdatePassword(string email, string password)
        {
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var user = db.users.First((e) => e.Email == email);
                    user.Password = EncryptPassword(password);
                    user.Code = null;
                    user.Expiration = null;

                    db.SaveChanges();
                }
            }
            catch
            {
            }
        }

        //verify valid teacher access code entered for a teacher account
        //return true on success, false on failure
        public bool ValidateTeacherAccessCode(int code)
        {
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var accessCode = db.accesscodes.First((e) => e.AccessCode1 == code);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        //verify valid parent teacher code entered for a parent account
        //return true on success, false on failure
        public bool ValidateParentTeacherCode(int code)
        {
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var teacherCode = db.students.First((e) => e.TeacherCode == code);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        //Add teacher account
        //return true on success, false on failure
        public bool AddTeacherAccount(string email, int code, string username)
        {
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var user = db.users.First((e) => e.Email == email);

                    teacheraccount teacher = new teacheraccount();
                    teacher.UserID = user.UserID;
                    if (username == null)
                        username = email;
                    teacher.UserName = username;
                    teacher.AccessCode = code;

                    db.teacheraccounts.Add(teacher);
                    db.SaveChanges();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        //Add parent account
        //return true on success, false on failure
        public bool AddParentAccount(string email, string username)
        {
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var user = db.users.First((e) => e.Email == email);

                    parentaccount parent = new parentaccount();
                    parent.UserID = user.UserID;
                    if (username == null)
                        username = email;
                    parent.UserName = username;

                    db.parentaccounts.Add(parent);
                    db.SaveChanges();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        //check if user has an existing admin account
        //return true if existing account, false if not account
        public bool ExistingAdminAccount(string email)
        {
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var user = db.users.First((e) => e.Email == email);
                    var admin = db.adminaccounts.First((a) => a.UserID == user.UserID);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        //check if user has an existing teacher account
        //return true if existing account, false if not account
        public bool ExistingTeacherAccount(string email)
        {
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var user = db.users.First((e) => e.Email == email);
                    var teacher = db.teacheraccounts.First((t) => t.UserID == user.UserID);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        //check if user has an existing parent account
        //return true if existing account, false if not account
        public bool ExistingParentAccount(string email)
        {
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var user = db.users.First((e) => e.Email == email);
                    var parent = db.parentaccounts.First((p) => p.UserID == user.UserID);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        //add student to existing parent account
        //return true on success, false on failure
        public bool AddStudentToParentAccount(string email, int teachercode)
        {
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var user = db.users.First((e) => e.Email == email);
                    var parent = db.parentaccounts.First((i) => i.UserID == user.UserID);
                    var student = db.students.First((s) => s.TeacherCode == teachercode);

                    //check if parent already has student
                    var result = db.parenttostudents.Where(p => p.ParentID == parent.ParentID &&
                                                            p.StudentID == student.StudentID).FirstOrDefault();
                    if (result != null)
                    {
                        return false;
                    }

                    parenttostudent pTos = new parenttostudent();
                    pTos.ParentID = parent.ParentID;
                    pTos.StudentID = student.StudentID;

                    db.parenttostudents.Add(pTos);
                    db.SaveChanges();

                    //add home tracking form to student
                    AddFormToStudent(HOMETRACKINGFORM, null, student.FirstName, student.LastName);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        //return data for a specific student
        public StudentData GetStudent(string fname, string lname)
        {
            StudentData studentData = null;

            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    studentData = new StudentData();
                    student matchingStudent = db.students.First((s) => s.FirstName == fname && s.LastName == lname);

                    studentData.FirstName = matchingStudent.FirstName;
                    studentData.LastName = matchingStudent.LastName;
                    studentData.BirthDate = matchingStudent.BirthDate;
                    studentData.Grade = matchingStudent.Grade;
                    studentData.Parent1Name = matchingStudent.ParentGuardian1;
                    studentData.Parent1Phone = matchingStudent.ParentGuardian1Phone;
                    studentData.Parent1Address = matchingStudent.ParentGuardian1Address;
                    studentData.Parent2Name = matchingStudent.ParentGuardian2;
                    studentData.Parent2Phone = matchingStudent.ParentGuardian2Phone;
                    studentData.Parent2Address = matchingStudent.ParentGuardian2Address;
                    studentData.TeacherCode = matchingStudent.TeacherCode;

                    return studentData;
                }
            }
            catch
            {
                return null;
            }
        }

        //return a list of students
        public List<string> ListStudents(string email)
        {
            List<string> studentsList = new List<string>();

            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var user = db.users.First((e) => e.Email == email);
                    var teacher = db.teacheraccounts.First((t) => t.UserID == user.UserID);
                    var students = db.students.ToList();

                    foreach (var s in students)
                    {
                        if (s.TeacherID == teacher.TeacherID)
                            studentsList.Add(s.LastName + ", " + s.FirstName);
                    }
                    studentsList.Sort();
                }
            }
            catch
            {
            }
            return studentsList;
        }

        //encrypt user's password
        //return encrypted password
        static private string EncryptPassword(string password)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(utf8.GetBytes(password));
                return Convert.ToBase64String(data);
            }
        }

        //return teacher's username
        public string GetTeacherUserName(string email)
        {
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var user = db.users.First((e) => e.Email == email);
                    var teacher = db.teacheraccounts.First((t) => t.UserID == user.UserID);

                    return teacher.UserName;
                }
            }
            catch
            {
                return null;
            }
        }

        //return parent's username
        public string GetParentUserName(string email)
        {
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var user = db.users.First((e) => e.Email == email);
                    var parent = db.parentaccounts.First((p) => p.UserID == user.UserID);

                    return parent.UserName;
                }
            }
            catch
            {
                return null;
            }
        }

        //return admin's last access date
        public string GetAdminAccessDate(string email)
        {
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var user = db.users.First((e) => e.Email == email);
                    var admin = db.adminaccounts.First((a) => a.UserID == user.UserID);

                    return (admin.LastAccess.ToString()).Split(' ')[0]; ;
                }
            }
            catch
            {
                return null;
            }
        }

        //return teacher's last access date
        public string GetTeacherAccessDate(string email)
        {
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var user = db.users.First((e) => e.Email == email);
                    var teacher = db.teacheraccounts.First((t) => t.UserID == user.UserID);

                    return (teacher.LastAccess.ToString()).Split(' ')[0]; ;
                }
            }
            catch
            {
                return null;
            }
        }

        //return parent's last access date
        public string GetParentAccessDate(string email)
        {
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var user = db.users.First((e) => e.Email == email);
                    var parent = db.parentaccounts.First((p) => p.UserID == user.UserID);

                    return (parent.LastAccess.ToString()).Split(' ')[0];
                }
            }
            catch
            {
                return null;
            }
        }

        //update admin's last access date
        public void UpdateAdminLastAccess(string email)
        {
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var user = db.users.First((e) => e.Email == email);
                    var admin = db.adminaccounts.First((a) => a.UserID == user.UserID);
                    admin.LastAccess = admin.LastAccess2;
                    admin.LastAccess2 = DateTime.Now;

                    db.SaveChanges();
                }
            }
            catch
            {
            }
        }

        //update teacher's last access date
        public void UpdateTeacherLastAccess(string email)
        {
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var user = db.users.First((e) => e.Email == email);
                    var teacher = db.teacheraccounts.First((t) => t.UserID == user.UserID);
                    teacher.LastAccess = teacher.LastAccess2;
                    teacher.LastAccess2 = DateTime.Now;

                    db.SaveChanges();
                }
            }
            catch
            {
            }
        }

        //update parent's last access date
        public void UpdateParentLastAccess(string email)
        {
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var user = db.users.First((e) => e.Email == email);
                    var parent = db.parentaccounts.First((p) => p.UserID == user.UserID);
                    parent.LastAccess = parent.LastAccess2;
                    parent.LastAccess2 = DateTime.Now;

                    db.SaveChanges();
                }
            }
            catch
            {
            }
        }

        //Add teacher and generate access code
        //Return access code on success, -1 on failure
        public int AddTeacher(string fname, string lname)
        {
            try
            {
                if (fname.Length < 1 || lname.Length < 1)
                    return -1;

                using (test_dbEntities db = new test_dbEntities())
                {
                    accesscode newTeacher = new accesscode();
                    newTeacher.FullName = fname + " " + lname;

                    Random rand = new Random();
                    bool codeAssigned = false;
                    int accessCode = -1;

                    while (!codeAssigned)
                    {
                        accessCode = rand.Next(1000, 100000);
                        var result = db.teacheraccounts.Where(x => x.AccessCode == accessCode).FirstOrDefault();

                        if (result == null)
                        {
                            newTeacher.AccessCode1 = accessCode;
                            codeAssigned = true;
                        }
                    }
                    db.accesscodes.Add(newTeacher);
                    db.SaveChanges();

                    return accessCode;
                }
            }
            catch
            {
                return -1;
            }
        }

        //remove link between user and teacher account
        //return true on success, false on failure
        public bool RemoveTeacher(string fname, string lname)
        {
            try
            {
                if (fname.Length < 1 || lname.Length < 1)
                    return false;

                using (test_dbEntities db = new test_dbEntities())
                {
                    var accessCode = db.accesscodes.First((a) => a.FullName == fname + " " + lname);
                    var teacher = db.teacheraccounts.First((t) => t.AccessCode == accessCode.AccessCode1);

                    //delete students from parent accounts
                    var students = db.students.Where((s) => s.TeacherID == teacher.TeacherID);

                    using (test_dbEntities db2 = new test_dbEntities())
                    {
                        foreach (var x in students)
                        {
                            var toDelete = db2.parenttostudents.Where((y) => y.StudentID == x.StudentID);
                            foreach (var parent in toDelete)
                                db2.parenttostudents.Remove(parent);
                        }
                        db2.SaveChanges();
                    }

                    teacher.UserID = teacher.UserID * 1000000;   //manipulate user id to invalidate teacher account
                    db.SaveChanges();

                    return true;
                }
            }
            catch
            {
                //try removing access code from database
                try
                {
                    using (test_dbEntities db = new test_dbEntities())
                    {
                        var accessCode = db.accesscodes.First((a) => a.FullName == fname + " " + lname);
                        db.accesscodes.Remove(accessCode);
                        db.SaveChanges();

                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

        //update user's email
        //return true on success, false on failure
        public bool UpdateEmail(string oldEmail, string newEmail)
        {
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var user = db.users.First((e) => e.Email == oldEmail);
                    user.Email = newEmail;
                    db.SaveChanges();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        //Lookup teacher access code by name
        //Return access code on success, -1 on failure
        public int AccessCodeLookup(string name)
        {
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var access = db.accesscodes.First((a) => a.FullName == name);
                    return access.AccessCode1;
                }
            }
            catch
            {
                return -1;
            }
        }

        //Add a student to teacher account
        //Return teacher code on success, -1 on failure
        public int AddStudent(string email, StudentData stud)
        {
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var user = db.users.First((e) => e.Email == email);
                    var teacher = db.teacheraccounts.First((t) => t.UserID == user.UserID);

                    //check if student with same name already exists
                    var result = db.students.Where(x => x.FirstName == stud.FirstName &&
                                                            x.LastName == stud.LastName).FirstOrDefault();
                    if (result != null)
                    {
                        return -1;
                    }

                    student s = new student();

                    s.TeacherID = teacher.TeacherID;
                    s.FirstName = stud.FirstName;
                    s.LastName = stud.LastName;
                    s.BirthDate = stud.BirthDate;
                    s.Grade = stud.Grade;
                    s.ParentGuardian1 = stud.Parent1Name;
                    s.ParentGuardian1Phone = stud.Parent1Phone;
                    s.ParentGuardian1Address = stud.Parent1Address;
                    s.ParentGuardian2 = stud.Parent2Name;
                    s.ParentGuardian2Phone = stud.Parent2Phone;
                    s.ParentGuardian2Address = stud.Parent2Address;

                    //generate teacher code
                    Random rand = new Random();
                    bool codeAssigned = false;
                    int teacherCode = -1;

                    while (!codeAssigned)
                    {
                        teacherCode = rand.Next(1000, 100000);
                        result = db.students.Where(x => x.TeacherCode == teacherCode).FirstOrDefault();

                        if (result == null)
                        {
                            s.TeacherCode = teacherCode;
                            codeAssigned = true;
                        }
                    }
                    db.students.Add(s);
                    db.SaveChanges();

                    return teacherCode;
                }
            }
            catch
            {
                return -1;
            }
        }

        //Update teacher username
        //Return true on success, false on failure
        public bool UpdateTeacherUserName(string email, string newUserName)
        {
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var user = db.users.First((e) => e.Email == email);
                    var teacher = db.teacheraccounts.First((t) => t.UserID == user.UserID);
                    if (newUserName == null || newUserName == "")
                        newUserName = user.Email;
                    teacher.UserName = newUserName;
                    db.SaveChanges();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        //Update parent username
        //Return true on success, false on failure
        public bool UpdateParentUserName(string email, string newUserName)
        {
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var user = db.users.First((e) => e.Email == email);
                    var parent = db.parentaccounts.First((p) => p.UserID == user.UserID);
                    if (newUserName == null || newUserName == "")
                        newUserName = user.Email;
                    parent.UserName = newUserName;
                    db.SaveChanges();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        //Delete a student
        //Also deletes parent account if parent has no other students
        //Return true on success, false on failure
        public bool DeleteStudent(string email, string fname, string lname)
        {
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var user = db.users.First((e) => e.Email == email);
                    var teacher = db.teacheraccounts.First((t) => t.UserID == user.UserID);
                    var student = db.students.First((s) => s.FirstName == fname && s.LastName == lname
                                                        && s.TeacherID == teacher.TeacherID);

                    //get list of parent to student accounts for student
                    var ptos = db.parenttostudents.Where((id) => id.StudentID == student.StudentID);

                    using (test_dbEntities db2 = new test_dbEntities())
                    {
                        foreach (var p in ptos)
                        {
                            //get parent
                            var parent = db2.parentaccounts.FirstOrDefault((x) => x.ParentID == p.ParentID);

                            using (test_dbEntities db3 = new test_dbEntities())
                            {
                                var delete = db3.parenttostudents.First((i) => i.ParentID == p.ParentID && i.StudentID ==
                                                                            student.StudentID);
                                db3.parenttostudents.Remove(delete);
                                db3.SaveChanges();
                            }
                        }
                        db2.SaveChanges();
                    }
                    db.students.Remove(student);
                    db.SaveChanges();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        //Update student information
        //Return true on success, false on failure
        public bool UpdateStudent(string email, string fname, string lname, StudentData stud)
        {
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var user = db.users.First((e) => e.Email == email);
                    var teacher = db.teacheraccounts.First((t) => t.UserID == user.UserID);
                    var student = db.students.First((s) => s.FirstName == fname && s.LastName == lname
                                                    && s.TeacherID == teacher.TeacherID);

                    if (stud.FirstName == "" || stud.LastName == "")
                        return false;

                    //check if student with same name already exists
                    var result = db.students.Where(s => s.FirstName == stud.FirstName && s.LastName == stud.LastName
                                                        && s.TeacherCode != stud.TeacherCode).FirstOrDefault();
                    if (result != null)
                    {
                        return false;
                    }

                    student.FirstName = stud.FirstName;
                    student.LastName = stud.LastName;
                    student.BirthDate = stud.BirthDate;
                    student.Grade = stud.Grade;
                    student.ParentGuardian1 = stud.Parent1Name;
                    student.ParentGuardian1Phone = stud.Parent1Phone;
                    student.ParentGuardian1Address = stud.Parent1Address;
                    student.ParentGuardian2 = stud.Parent2Name;
                    student.ParentGuardian2Phone = stud.Parent2Phone;
                    student.ParentGuardian2Address = stud.Parent2Address;
                    student.TeacherCode = student.TeacherCode;
                    student.TeacherID = student.TeacherID;
                    db.SaveChanges();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        //return list of teacher behavior traking forms by category (behavior or other)
        public List<string> GetTeacherForms(string category)
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
        public List<string> GetStudentForms(string fname, string lname, string category)
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

        //return a list of children for parent account
        public List<string> ListChildren(string email)
        {
            List<string> childrenList = new List<string>();

            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var user = db.users.First((e) => e.Email == email);
                    var parent = db.parentaccounts.First((p) => p.UserID == user.UserID);
                    var studentIDs = (from x in db.parenttostudents
                                      where x.ParentID == parent.ParentID
                                      select x.StudentID);

                    using (test_dbEntities db2 = new test_dbEntities())
                    {
                        foreach (var id in studentIDs)
                        {
                            var student = db2.students.First((y) => y.StudentID == id);
                            childrenList.Add(student.LastName + ", " + student.FirstName);
                        }
                    }
                    childrenList.Sort();
                }
            }
            catch
            {
            }
            return childrenList;
        }

        //return a completed form for student
        //returns form completed that day or null if no form exists
        public StudentFormData GetDailyForm(string formName, string fname, string lname)
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
                                //time calculation here
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
                            //time calculation here
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
