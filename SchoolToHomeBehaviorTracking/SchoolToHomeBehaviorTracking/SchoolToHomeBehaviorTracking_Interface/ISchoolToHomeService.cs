using System.ServiceModel;

namespace SchoolToHomeBehaviorTracking_Interface
{
    [ServiceContract]
    public interface ISchoolToHomeService
    {
        void DoWork();
    }
}
