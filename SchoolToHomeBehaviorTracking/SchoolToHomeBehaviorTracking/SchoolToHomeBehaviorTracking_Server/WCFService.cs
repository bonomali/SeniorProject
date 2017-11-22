using SchoolToHomeBehaviorTracking_Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;

namespace SchoolToHomeBehaviorTracking_Server
{
    public class WCFService : IWCFService
    {
        //validate user account by email and password
        //return true on success, false on failure
        public bool Login(string email, string password)
        {
            bool found = false;
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var user = db.users.First((e) => e.Email == email);
                    if (user.Password == EncryptPassword(password))
                        found = true;
                }
            }
            catch
            {
                Console.WriteLine("Error  in Login");
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
                Console.WriteLine("Error creating account");
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

            for(int i = 0; i < 6; i++)
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
                Console.WriteLine("Error in Access Code");
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
                Console.WriteLine("Error in Reset Password");
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
                Console.WriteLine("Error in Verify Access Code");
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
                Console.WriteLine("Error in Update Password");
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
                Console.WriteLine("Error in Validate Teacher Access Code");
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
                Console.WriteLine("Error in Validate ParentTeacher Code");
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
                Console.WriteLine("Error in Add Teacher Account");
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
                Console.WriteLine("Error in Add Parent Account");
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
                Console.WriteLine("Error in check existing admin account");
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
                Console.WriteLine("Error in check existing teacher account");
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
                Console.WriteLine("Error in check existing parent account");
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
                }
            }
            catch
            {
                Console.WriteLine("Error in add student to parent account");
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
                Console.WriteLine("Error getting student data");
            }

            return studentData;
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

                    foreach(var s in students)
                    {
                        if (s.TeacherID == teacher.TeacherID)
                            studentsList.Add(s.LastName + ", " + s.FirstName);
                    }
                    studentsList.Sort();
                }
            }
            catch
            {
                Console.WriteLine("Error getting list of students");
            }

            return studentsList;
        }

        //encrypt user's password
        static string EncryptPassword(string password)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(utf8.GetBytes(password));
                return Convert.ToBase64String(data);
            }
        }

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
                Console.WriteLine("Error in update admin last access");
            }
        }

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
                Console.WriteLine("Error in update teacher last access");
            }
        }

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
                Console.WriteLine("Error in update parent last access");
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
                Console.WriteLine("Error in add teacher");
                return -1;
            }
        }

        //remove link between user and teacher account
        //return true on success, false on failure
        public bool RemoveTeacher(string fname, string lname)
        {
            try
            {
                if(fname.Length < 1 || lname.Length < 1)
                    return false;

                using (test_dbEntities db = new test_dbEntities())
                {
                    var accessCode = db.accesscodes.First((a) => a.FullName == fname + " " + lname);
                    var teacher = db.teacheraccounts.First((t) => t.AccessCode == accessCode.AccessCode1);
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
                    Console.WriteLine("Error in remove teacher");
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
                Console.WriteLine("Error in update email");
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
                Console.WriteLine("Error in access code lookup");
                return -1;
            }
        }

        public int GenerateTeacherCode()
        {
            try
            {
               using(test_dbEntities db = new test_dbEntities())
                {
                    var count = db.students.Count();
                    int teacherCode = db.students.Count() + 1000;
                    
                    return teacherCode;
                }
            }
            catch
            {
                Console.WriteLine("Error in add teacher");
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
                Console.WriteLine("Error in add student");
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
                Console.WriteLine("Error in update teacher username");
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
                Console.WriteLine("Error in update parent username");
                return false;
            }
            return true;
        }

        //Delete a student
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

                    db.students.Remove(student);
                    db.SaveChanges();

                    return true;
                }
            }
            catch
            {
                Console.WriteLine("Error in delete student");
                return false;
            }
        }

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
                Console.WriteLine("Error in edit student");
                return false;
            }
        }
    }
}
