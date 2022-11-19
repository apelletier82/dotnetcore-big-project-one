using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BigProjectOne.Libraries.Models.Interfaces;
using BigProjectOne.Libraries.Models;

namespace BigProjectOne.Libraries.Data.DB
{
    public class SoftDeletableEntityTypeConfiguration<TEntity> :
        IEntityTypeConfiguration<TEntity> where TEntity : class, ISoftDeletableModel
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.OwnsOne<Audit>(c => c.Deletion);
            builder.Property(p => p.Deleted)
                .HasDefaultValue(false)
                .IsRequired();
        }
    }
}