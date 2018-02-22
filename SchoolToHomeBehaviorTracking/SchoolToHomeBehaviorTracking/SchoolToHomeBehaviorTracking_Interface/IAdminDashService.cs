using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SchoolToHomeBehaviorTracking_Interface
{
    [ServiceContract]
    public interface IAdminDashService
    {
        //return admin user's last access date
        [OperationContract]
        string GetAdminAccessDate(string email);

        //update admin user's last access
        [OperationContract]
        void UpdateAdminLastAccess(string email);

        //Add teacher to database through admin account, generate access code
        //return teacher access code
        [OperationContract]
        int AddTeacher(string fname, string lname);

        //Remove link between teacher and teacher account
        //Keep teacher and student data in database for Archive reasons
        //Return true on success, false on failure
        [OperationContract]
        bool RemoveTeacher(string fname, string lname);

        //Lookup access code by teacher name
        //Return access code on success, -1 on failure
        [OperationContract]
        int AccessCodeLookup(string name);
    }
}
