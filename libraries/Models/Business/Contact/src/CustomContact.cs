using System;
using BigProjectOne.Libraries.Models.Interfaces;

namespace BigProjectOne.Libraries.Models.Bussiness.Contacts
{
    public abstract class CustomContact : IIdentifiableModel,
        IVersionableModel, IAuditableModel, ITenanciableModel
    {
        public long ID { get; set; }
        public byte[] RowVersion { get; set; }
        public long TenantID { get; set; }        
        public string Code { get; set; }
        public string Name { get; set; }
        public String Name2 { get; set; }
        public string DisplayName { get; set; }
        public Category Category { get; set; }
        public Group Group { get; set; }
        public String PictureURL { get; set; }
        public Audit Creation { get; set; }
        public Audit Change { get; set; }
    }
}