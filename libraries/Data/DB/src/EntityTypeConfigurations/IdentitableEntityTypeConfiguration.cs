using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BigProjectOne.Libraries.Models.Interfaces;

namespace BigProjectOne.Libraries.Data.DB
{
    public class IdentitableEntityTypeConfiguration<TEntity> :
        IEntityTypeConfiguration<TEntity> where TEntity : class, IIdentifiableModel
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(k => k.ID);
            builder.Property(p => p.ID).ValueGeneratedOnAdd();
        }
    }
}