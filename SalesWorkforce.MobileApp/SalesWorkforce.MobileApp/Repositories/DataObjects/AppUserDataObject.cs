using SalesWorkforce.MobileApp.Repositories.Base;

namespace SalesWorkforce.MobileApp.Repositories.DataObjects
{
    public class AppUserDataObject : DataObjectBase
    {
        public long RecordId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string AgentId { get; set; }
        public string EmailAddress { get; set; }
        public string DisplayName { get; set; }
    }
}
