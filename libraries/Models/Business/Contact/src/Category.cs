using System;
using System.Drawing;
using BigProjectOne.Libraries.Models.Interfaces;

namespace BigProjectOne.Libraries.Models.Bussiness.Contacts
{
    public class Category : IIdentifiableModel, ITenanciableModel
    {
        public long ID { get; set; }
        public long TenantID { get; set; }
        public string Name { get; set; }
        public String Description { get; set; }
        public CategoryType CategoryType { get; set; }
        public String Color { get; set; }
    }
}