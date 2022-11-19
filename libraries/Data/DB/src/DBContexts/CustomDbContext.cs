using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using BigProjectOne.Libraries.Models.Interfaces;

namespace BigProjectOne.Libraries.Data.DB
{        
    public abstract class CustomDbContext : DbContext
    {
        #region "Constants"
        private const int CST_IntfListCapacity = 5;
        private const string CST_ApplyConfigurationMethod = "ApplyConfiguration";
        #endregion "Constants"

        public ISimpleUser User { get; private set; }                

        private void setAuditCreation(IAuditableModel audit, DateTime date)
        {
            if (audit.Creation == null)
                audit.Creation = new Audit();

            audit.Creation.Date = date;
            audit.Creation.User = User.UserName;

            setAuditChange(audit, date);
        }

        private void setAuditChange(IAuditableModel audit, DateTime date)
        {
            if (audit.Change == null)
                audit.Change = new Audit();

            audit.Change.Date = date;
            audit.Change.User = User.UserName;
        }

        private void setDeletion(ISoftDeletableModel deletion, DateTime date)
        {
            if (deletion.Deletion == null)
                deletion.Deletion = new Audit();

            deletion.Deletion.Date = date;
            deletion.Deletion.User = User.UserName;
        }

        private IEnumerable<PropertyInfo> getDBSets(BindingFlags bindingFlags)
        {
            return this.GetType()
                    .GetProperties(bindingFlags)
                        .Where(f => f.PropertyType.IsGenericType &&
                            typeof(DbSet<>).IsAssignableFrom(f.PropertyType.GetGenericTypeDefinition()));
        }
        
        private IEnumerable<PropertyInfo> getPrivateDBSets()
        {
            return getDBSets(BindingFlags.NonPublic | BindingFlags.Instance);
        }

        private IEnumerable<PropertyInfo> getPublicDBSets()
        {
            return getDBSets(BindingFlags.Public | BindingFlags.Instance);
        }

        private void applyInterfacableEntityTypeConfiguration(ModelBuilder modelBuilder)
        {
            List<PropertyInfo> props = new List<PropertyInfo>();
            props.AddRange(getPrivateDBSets());
            props.AddRange(getPublicDBSets());

            if (props == null || props.Count() == 0)
                return;
        
            foreach (PropertyInfo pi in props.Where(p => p.PropertyType.GetGenericArguments().FirstOrDefault() != null))
            {
                IList<object> entityTypeConfigurationInstances = new List<Object>(CST_IntfListCapacity);
                Type tp = pi.PropertyType.GetGenericArguments().FirstOrDefault();
                
                foreach (var intf in tp.GetInterfaces().Where(i =>
                     typeof(BigProjectOne.Libraries.Models.Interfaces.IModel).IsAssignableFrom(i)))
                    fillInterfacableEntityTypeConfigurationForAnInterface(intf, entityTypeConfigurationInstances, tp);

                MethodInfo mtd = modelBuilder.GetType().GetMethods().Where(m => m.IsGenericMethod &&
                        m.Name.Equals(CST_ApplyConfigurationMethod, StringComparison.CurrentCultureIgnoreCase))
                        .FirstOrDefault();

                if (mtd != null)
                    foreach (var obj in entityTypeConfigurationInstances)
                    {
                        MethodInfo genmtd = mtd.MakeGenericMethod(tp);
                        genmtd.Invoke(modelBuilder, new object[] { obj });
                    }
            }
        }

        protected virtual void fillInterfacableEntityTypeConfigurationForAnInterface(Type interfce, 
            IList<object> entityTypeConfigurationInstances, Type modelClass)
        {
            List<Type> classTypes = new List<Type>(CST_IntfListCapacity);

            if (typeof(IIdentifiableModel).IsAssignableFrom(interfce))
                classTypes.Add(typeof(IdentitableEntityTypeConfiguration<>));

            if (typeof(IVersionableModel).IsAssignableFrom(interfce))
                classTypes.Add(typeof(VersionableEntityTypeConfiguration<>));

            if (typeof(IAuditableModel).IsAssignableFrom(interfce))
                classTypes.Add(typeof(AuditableEntityTypeConfiguration<>));

            if (typeof(ISoftDeletableModel).IsAssignableFrom(interfce))
                classTypes.Add(typeof(SoftDeletableEntityTypeConfiguration<>));
                        
            foreach (Type ct in classTypes)
            {
                Type constructedType = ct.MakeGenericType(modelClass);
                entityTypeConfigurationInstances.Add(Activator.CreateInstance(constructedType, new object[] { }));
            }                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            applyInterfacableEntityTypeConfiguration(modelBuilder);            
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        #region "Constructors"
        public CustomDbContext(ISimpleUser user) : base() => User = user;        

        public CustomDbContext(DbContextOptions options, ISimpleUser user) :
            base(options) => User = user;

        #endregion "Constructors"

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ChangeTracker.DetectChanges();

            foreach (EntityEntry e in ChangeTracker.Entries()
                .Where(en => en.State == EntityState.Added || en.State == EntityState.Modified))
            {
                DateTime dateChanges = DateTime.UtcNow;
                if (e.Entity is IAuditableModel)
                {
                    IAuditableModel audit = (IAuditableModel)e.Entity;
                    switch (e.State)
                    {
                        case EntityState.Added:
                            setAuditCreation(audit, dateChanges);
                            break;
                        case EntityState.Modified:
                            setAuditChange(audit, dateChanges);
                            break;
                    }
                }

                if (e.Entity is ISoftDeletableModel)
                {
                    ISoftDeletableModel del = (ISoftDeletableModel)e.Entity;
                    if (e.Property("Deleted").IsModified && del.Deleted)
                        setDeletion(del, dateChanges);
                }
            }

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, System.Threading.CancellationToken cancellationToken)
        {
            Task<int> result = new Task<int>(() => this.SaveChanges(acceptAllChangesOnSuccess),
                cancellationToken);
            result.Start();            
            return result;
        }            
     }
}
