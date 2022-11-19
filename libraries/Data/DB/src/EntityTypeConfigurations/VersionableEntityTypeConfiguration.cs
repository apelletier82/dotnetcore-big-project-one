using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BigProjectOne.Libraries.Models.Interfaces;

namespace BigProjectOne.Libraries.Data.DB
{
    public class VersionableEntityTypeConfiguration<TEntity> :
        IEntityTypeConfiguration<TEntity> where TEntity : class, IVersionableModel
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
            => builder.Property(p => p.RowVersion)
                .IsRowVersion()
                .IsRequired()
                .ValueGeneratedOnAddOrUpdate();
    }
}