using System;
using BigProjectOne.Libraries.Models.Interfaces;

namespace BigProjectOne.Libraries.LibrairiesUnitTest
{
    public class TenanciableTestModel :
        ITenanciableModel,
        IIdentifiableModel,
        IVersionableModel,
        IAuditableModel,
        ISoftDeletableModel
    {
        public long ID { get; set; }
        public byte[] RowVersion { get; set; }
        public Audit Creation { get; set; }
        public Audit Change { get; set; }
        public bool Deleted { get; set; } = false;
        public Audit Deletion { get; set; }
        public String Name { get; set; }    
        public long TenantID { get; set; }        
    }
}