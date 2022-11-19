using BigProjectOne.Libraries.Models.Interfaces;

namespace BigProjectOne.Libraries.Models.Bussiness.Contacts
{
    public class Address : IIdentifiableModel
    {
        public long ID { get; set; }
        public long ContactID { get; set; }
        public string Name { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string Line4 { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public bool Default { get; set; }
    }
}