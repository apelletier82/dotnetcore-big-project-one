using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BigProjectOne.Libraries.Models.Interfaces;

namespace BigProjectOne.Libraries.Data.DB
{
    public class TenanciableEntityTypeConfiguration<TEntity> :
        IEntityTypeConfiguration<TEntity> where TEntity : class, ITenanciableModel
    {
        public long TenantID { get; private set; }

        public TenanciableEntityTypeConfiguration(long tenantID) : base()
        {
            TenantID = tenantID;
        }

        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(p => p.TenantID).IsRequired();
            builder.HasQueryFilter(q => q.TenantID == TenantID);
            builder.HasIndex(i => i.TenantID);
        }
    }
}