using GestionnaireClient.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionnaireClient.DAL
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<DBContextMgr>
    {
        protected override void Seed(DBContextMgr context)
        {
            var customer = new List<Customer>
            {
                new Customer() { Id=1, FirstName ="Regis", LastName="Robert", Email="regis.robert@gmail.com"}

            };

            customer.ForEach(s => context.Customers.Add(s));
            context.SaveChanges();

        }
    }
}
