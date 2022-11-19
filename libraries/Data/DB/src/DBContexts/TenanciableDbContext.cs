using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using BigProjectOne.Libraries.Models.Interfaces;

namespace BigProjectOne.Libraries.Data.DB
{
    public abstract class TenanciableDbContext : CustomDbContext
    {
        public IIdentifiableModel Tenant { get; private set; }

        public TenanciableDbContext(ISimpleUser user, IIdentifiableModel tenant) : base(user) => Tenant = tenant;

        public TenanciableDbContext(DbContextOptions options, ISimpleUser user, IIdentifiableModel tenant)
        : base(options, user) => Tenant = tenant;

        protected override void fillInterfacableEntityTypeConfigurationForAnInterface(Type interfce, 
            IList<object> entityTypeConfigurationInstances, Type modelClass)
        {
            base.fillInterfacableEntityTypeConfigurationForAnInterface(interfce, entityTypeConfigurationInstances, modelClass);
            if (typeof(ITenanciableModel).IsAssignableFrom(interfce))
            {
                Type ct = typeof(TenanciableEntityTypeConfiguration<>);
                Type constructedType = ct.MakeGenericType(modelClass);
                entityTypeConfigurationInstances.Add(Activator.CreateInstance(constructedType, new object[] { Tenant.ID }));
            }
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ChangeTracker.DetectChanges();

            foreach (EntityEntry e in ChangeTracker.Entries()
                .Where(en => en.State == EntityState.Added || en.State == EntityState.Modified))
            {
                if (e.Entity is ITenanciableModel)
                {
                    ITenanciableModel tm = (ITenanciableModel)e.Entity;
                    if (tm.TenantID != Tenant.ID)
                        throw new TenantContextException(string.Format("Bad Tenant ID : Actual={0} - Expected={1}", 
                            tm.TenantID, Tenant.ID));
                }
            }

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
    }
}