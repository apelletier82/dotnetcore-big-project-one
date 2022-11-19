using System;
using System.Collections.Generic;

namespace BigProjectOne.Libraries.Models.Bussiness.Contacts
{
    public class Contact : CustomContact
    {
        public String Title { get; set; }
        public virtual List<Phone> Phones { get; set; }
        public virtual List<EMail> Emails { get; set; }
        public virtual List<Messaging> Messagings { get; set; }
        public virtual List<Address> Addresses { get; set; }
        public ProfessionalInformation Professional { get; set; }
        public virtual List<ImportantDate> ImportantDates { get; set; }
        public String VATIntra { get; set; }
        public String CurrencyISOCode { get; set; }
        public Contact Parent { get; set; }
        public long? VATId { get; set; }
        public long? TariffId { get; set; }
        public long? PaymentTypeId { get; set; }
        public long? PaymentModeId { get; set; }
        public virtual List<RestrictedFileID> RestrictedFilesID { get; set; }
    }
}
