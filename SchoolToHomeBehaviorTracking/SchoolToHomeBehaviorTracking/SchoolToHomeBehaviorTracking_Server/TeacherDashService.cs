using SchoolToHomeBehaviorTracking_Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolToHomeBehaviorTracking_Server
{
    public partial class SchoolToHomeService : IMetaInterface
    {
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
    }
}
