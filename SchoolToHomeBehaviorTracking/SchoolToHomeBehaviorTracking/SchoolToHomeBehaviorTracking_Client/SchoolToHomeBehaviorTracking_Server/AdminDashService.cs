using SchoolToHomeBehaviorTracking_Interface;
using System;
using System.Linq;

namespace SchoolToHomeBehaviorTracking_Server
{
    public partial class SchoolToHomeService : IMetaInterface
    {
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
    }
}
