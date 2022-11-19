using System;
using BigProjectOne.Libraries.Models.Interfaces;

namespace BigProjectOne.Libraries.Models.Bussiness.Contacts
{
    public class Group : IIdentifiableModel, ITenanciableModel
    {
        public long ID { get; set; }
        public long TenantID { get; set; }
        public string GroupName { get; set; }
        public String Description { get; set; }
    }
}