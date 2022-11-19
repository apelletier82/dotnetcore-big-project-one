using System.Net.Http;
using BigProjectOne.Libraries.Data.DB;
using BigProjectOne.Libraries.Models.Bussiness.Contacts;
using BigProjectOne.Libraries.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BigProjectOne.App.Services.Contacts.DB
{
    public class ContactDBContext : TenanciableDbContext
    {
        private DbSet<Group> Groups { get; set; }
        private DbSet<Category> Categories { get; set; }
        private DbSet<Contact> Contacts { get; set; }
        private DbSet<Address> Adresses { get; set; }
        private DbSet<EMail> EMails { get; set; }
        private DbSet<Phone> Phones { get; set; }
        private DbSet<ImportantDate> ImportantDates { get; set; }
        private DbSet<Messaging> Messagings { get; set; }
        private DbSet<RestrictedFileID> RestrictedFileIDs { get; set; }
        
        #region Constructors
        public ContactDBContext(ISimpleUser user, IIdentifiableModel tenant) : base(user, tenant)
        {
        }

        public ContactDBContext(DbContextOptions options, ISimpleUser user, IIdentifiableModel tenant) : base(options, user, tenant)
        {
        }
        #endregion
    }
}