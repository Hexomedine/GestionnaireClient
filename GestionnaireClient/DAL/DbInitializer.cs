using GestionnaireClient.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionnaireClient.DAL
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<CustomerContext>
    {
        protected override void Seed(CustomerContext context)
        {
            var customer = new List<Customer>
            {
                new Customer() { Id=1, FirstName ="Regis", LastName="Robert", Email="regis.robert@gmel.co"},
                new Customer() { Id=2, FirstName ="Ababou", LastName="Kaker", Email="akaker@gmel.co"},
                new Customer() { Id=3, FirstName ="John", LastName="Fitz", Email="jf@gmel.co"}

            };

            customer.ForEach(s => context.Customers.Add(s));
            context.SaveChanges();

        }
    }
}
