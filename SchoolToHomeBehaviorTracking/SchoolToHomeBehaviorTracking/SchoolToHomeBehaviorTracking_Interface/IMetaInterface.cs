using System.ServiceModel;

namespace SchoolToHomeBehaviorTracking_Interface
{
    [ServiceContract]
    public interface IMetaInterface : IAccountService, IAdminDashService, ITeacherDashService, IParentDashService, IFormsService
    {
    }
}
