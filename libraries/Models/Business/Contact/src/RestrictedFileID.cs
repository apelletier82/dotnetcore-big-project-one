using BigProjectOne.Libraries.Models.Interfaces;
namespace BigProjectOne.Libraries.Models.Bussiness.Contacts
{
    public class RestrictedFileID: IIdentifiableModel
    {
        public long ID { get; set; }
        public long ContactID { get; set; }
        public long FileID { get; set; }        
    }
}