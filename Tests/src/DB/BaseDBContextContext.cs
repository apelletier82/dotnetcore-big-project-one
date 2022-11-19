using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BigProjectOne.Libraries.Data.DB;
using BigProjectOne.Libraries.Models.Interfaces;
using BigProjectOne.Libraries.Models.Bussiness.Contacts;

namespace BigProjectOne.Libraries.LibrairiesUnitTest
{
    public class BaseDbContextContext : CustomDbContext
    {
        public BaseDbContextContext(ISimpleUser user) : base(user) { }

        private DbSet<TestModel> testModels { get; set; }
        private DbSet<Contact> Contacts {get;set;}

        protected override void OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TestModel>(p => p.Property("Name").HasMaxLength(50));

            if (Database.IsSqlite())
            {
                var timestampProperties = modelBuilder.Model
                    .GetEntityTypes()
                    .SelectMany(t => t.GetProperties())
                    .Where(p => p.ClrType == typeof(byte[])
                        && p.ValueGenerated == ValueGenerated.OnAddOrUpdate
                        && p.IsConcurrencyToken);

                foreach (var property in timestampProperties)
                {
                    property.SetValueConverter(new SqliteTimestampConverter());
                    property.SetDefaultValueSql("CURRENT_TIMESTAMP");
                }
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlite(@"Data Source=BaseDBContextDB.db");
        }
    }
}