using SchoolToHomeBehaviorTracking_Interface;
using System;
using System.Collections.Generic;
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
        public bool ValidateEmail(string email)
        {
            ValidateAccountCreation validate = new ValidateAccountCreation();
            return validate.IsValidEmail(email);
        }

        //validate user's password for account creation
        public bool ValidatePassword(string password)
        {
            ValidateAccountCreation validate = new ValidateAccountCreation();
            return validate.ValidatePassword(password);
        }

        //validate that passwords match
        public bool ValidateMatchingPasswords(string p1, string p2)
        {
            ValidateAccountCreation validate = new ValidateAccountCreation();
            return validate.MatchingPassword(p1, p2);
        }

        //create a new user account
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
        private static string GenerateAccessCode(string email)
        {
            string accessCode = null;
            Random rnd = new Random();

            for(int i = 0; i < 8; i++)
            {
                accessCode += Convert.ToChar(rnd.Next(65, 91));
            }

            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var user = db.users.First((e) => e.Email == email);
                    user.Code = accessCode;

                    db.SaveChanges();
                }
            }
            catch
            {
                Console.WriteLine("Error in Access Code");
            }
            return accessCode;
        }

        public bool ResetPassword(string email)
        {
            SendEmail emailObj = new SendEmail();
            return emailObj.Send(email, GenerateAccessCode(email));
        }

        //verify the correct access code was entered for user to reset password
        public bool VerifyResetPassword(string email, string accessCode)
        {
            bool verified = false;
            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    var user = db.users.First((e) => e.Email == email);

                    if (user.Code == accessCode)
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

                    db.SaveChanges();
                }
            }
            catch
            {
                Console.WriteLine("Error in Update Password");
            }
        }

        //return data for a specific student
        public StudentData GetStudent(string lastName)
        {
            StudentData studentData = null;

            try
            {
                using (test_dbEntities db = new test_dbEntities())
                {
                    studentData = new StudentData();
                    student matchingStudent = db.students.First((s) => s.LastName == lastName);

                    studentData.FirstName = matchingStudent.FirstName;
                    studentData.LastName = matchingStudent.LastName;
                    studentData.BirthDate = matchingStudent.BirthDate;
                    studentData.Grade = matchingStudent.Grade.Value;
                }
            }
            catch
            {
                Console.WriteLine("Error getting student data");
            }

            return studentData;
        }

        //return a list of students
        public List<string> ListStudents()
        {
            List<string> studentsList = new List<string>();

            try
            {
                using (test_dbEntities db = new test_dbEntities()) 
                {
                    var students = from s in db.students
                                   select s.LastName + ", " + s.FirstName;

                    studentsList = students.ToList();
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
    }
}
