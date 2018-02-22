using SchoolToHomeBehaviorTracking_Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolToHomeBehaviorTracking_Server
{
    public partial class SchoolToHomeService : IMetaInterface
    {
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
    }
}
