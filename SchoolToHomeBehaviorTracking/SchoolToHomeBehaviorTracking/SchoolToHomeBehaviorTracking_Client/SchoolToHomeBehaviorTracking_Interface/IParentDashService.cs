using System.Collections.Generic;
using System.ServiceModel;

namespace SchoolToHomeBehaviorTracking_Interface
{
    [ServiceContract]
    public interface IParentDashService
    {
        //return parent's username
        [OperationContract]
        string GetParentUserName(string email);

        //return parent user's last access date
        [OperationContract]
        string GetParentAccessDate(string email);

        //update parent user's last access
        [OperationContract]
        void UpdateParentLastAccess(string email);

        //Update teacher username
        //return true on success, false on failure
        [OperationContract]
        bool UpdateParentUserName(string email, string newUserName);

        //return a list of children for parent
        //return list of children
        [OperationContract]
        List<string> ListChildren(string email);
    }
}
