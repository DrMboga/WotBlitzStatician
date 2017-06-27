using System;
using System.Linq;
using WotBlitzStatician.Data;
using Xunit;

namespace WotBlitzStatician.Logic.Tests
{
    public class TestDatabase
    {
        public TestDatabase()
        {
        }

        [Fact(Skip = "Database is not ready yet")]
        public void TestSimple()
        {
            using(var context = new BlitzStaticianDataContext())
            {
                var accounts = context.Account.ToList();
            }
        }
    }
}
