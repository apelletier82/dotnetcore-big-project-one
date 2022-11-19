using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BigProjectOne.Libraries.Models.Interfaces;
using BigProjectOne.Libraries.Models;

namespace BigProjectOne.Libraries.Data.DB
{
    public class AuditableEntityTypeConfiguration<TEntity> :
        IEntityTypeConfiguration<TEntity> where TEntity : class, IAuditableModel
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.OwnsOne<Audit>(c => c.Creation);
            builder.OwnsOne<Audit>(c => c.Change);
        }
    }
}