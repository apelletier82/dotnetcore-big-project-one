using System.Threading.Tasks;
using BigProjectOne.Libraries.Models.Technical.Tenant;
using BigProjectOne.Libraries.Models.Bussiness.Contacts;
using Xunit;
using System.Collections.Generic;

namespace BigProjectOne.Libraries.LibrairiesUnitTest
{
    public class UnitTestDBContext
    {
        SimpleUser _user = new SimpleUser() { UserName = "alex" };

        [Fact]
        public void TestBaseDbContext()
        {
            using (BaseDbContextContext dc = new BaseDbContextContext(_user))
            {
                dc.Database.EnsureDeleted();
                dc.Database.EnsureCreated();

                TestModel tm = new TestModel();
                tm.Name = "pelletier_alexandre";

                dc.Add<TestModel>(tm);
                dc.SaveChanges();
            }
        }

        [Fact]
        public void TestBaseDbContextWithTask()
        {
            using (BaseDbContextContext dc = new BaseDbContextContext(_user))
            {
                dc.Database.EnsureDeleted();
                dc.Database.EnsureCreated();

                TestModel tm = new TestModel();
                tm.Name = "pelletier_alexandre";

                dc.Add<TestModel>(tm);
                Task<int> savetsk = dc.SaveChangesAsync();
                savetsk.Wait();
                int res = savetsk.Result;
            }
        }

        [Fact]
        public void TestTenantDBContext()
        {
            Tenant _tenant = new Tenant { ID = 1, Name = "Tenant 1", Hosts = new[] { "localhost" } };
            using (TenantDBContextContext dc = new TenantDBContextContext(_user, _tenant))
            {
                dc.Database.EnsureDeleted();
                dc.Database.EnsureCreated();

                TenanciableTestModel ttm = new TenanciableTestModel();
                ttm.Name = "pelletier_alexandre";
                ttm.TenantID = _tenant.ID;
                dc.Add<TenanciableTestModel>(ttm);

                TestModel tm = new TestModel();
                tm.Name = "pelletier_alexandre";

                dc.Add<TestModel>(tm);

                dc.SaveChanges();
            }
        }

        [Fact]
        public void TestTenantDBContextWithTask()
        {
            Tenant _tenant = new Tenant { ID = 1, Name = "Tenant 1", Hosts = new[] { "localhost" } };
            using (TenantDBContextContext dc = new TenantDBContextContext(_user, _tenant))
            {
                dc.Database.EnsureDeleted();
                dc.Database.EnsureCreated();

                TenanciableTestModel ttm = new TenanciableTestModel();
                ttm.Name = "pelletier_alexandre";
                ttm.TenantID = _tenant.ID;
                dc.Add<TenanciableTestModel>(ttm);

                TestModel tm = new TestModel();
                tm.Name = "pelletier_alexandre";

                dc.Add<TestModel>(tm);
                Task<int> savetsk = dc.SaveChangesAsync();
                savetsk.Wait();
                int res = savetsk.Result;
            }
        }
    }
}
