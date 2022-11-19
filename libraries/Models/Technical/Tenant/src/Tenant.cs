using System;
using BigProjectOne.Libraries.Models.Interfaces;

namespace BigProjectOne.Libraries.Models.Technical.Tenant
{
    public class Tenant : IIdentifiableModel, IVersionableModel, IAuditableModel,
        ISoftDeletableModel
    {
        public long ID { get; set; }
        public byte[] RowVersion { get; set; }
        public string Name { get; set; }
        public string[] Hosts { get; set; }
        public Audit Creation { get; set; }
        public Audit Change { get; set; }
        public bool Deleted { get; set; }
        public Audit Deletion { get; set; }
    }
}
