using SchoolToHomeBehaviorTracking_Interface;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;

namespace SchoolToHomeBehaviorTracking_Server
{
    public partial class SchoolToHomeService : IMetaInterface
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

        //validate user's email for account creation
        //return true on success, false on failure
        public bool ValidateEmail(string email)
        {
            ValidateAccountCreation validate = new ValidateAccountCreation();
            return validate.IsValidEmail(email);
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
    }
}
