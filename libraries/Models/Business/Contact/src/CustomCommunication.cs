using BigProjectOne.Libraries.Models.Interfaces;

namespace BigProjectOne.Libraries.Models.Bussiness.Contacts
{
    public abstract class CustomCommunication : IIdentifiableModel
    {
        public long ID { get; set; }
        public long ContactID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool Default { get; set; }
    }
}