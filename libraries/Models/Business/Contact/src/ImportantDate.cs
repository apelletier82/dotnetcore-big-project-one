using System;
using BigProjectOne.Libraries.Models.Interfaces;

namespace BigProjectOne.Libraries.Models.Bussiness.Contacts
{
    public class ImportantDate : IIdentifiableModel
    {
        public long ID { get; set; }
        public long ContactID { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public ImportantDateRepeatUnit RepeatUnit { get; set; }
    }
}