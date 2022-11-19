using System;
using BigProjectOne.Libraries.Models.Interfaces;

namespace BigProjectOne.Libraries.Models.Business.Parameters
{
    public abstract class CustomParameter : IIdentifiableModel,
        IVersionableModel, IAuditableModel, ITenanciableModel
    {
        public long ID { get; set; }
        public byte[] RowVersion { get; set; }
        public long TenantID { get; set; }
        public Audit Creation { get; set; }
        public Audit Change { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string Code { get; set; }
        public String Description { get; set; }
    }
}
