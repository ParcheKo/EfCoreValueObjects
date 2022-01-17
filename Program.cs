using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EfCoreValueObjects
{
    class Program
    {
        static async Task Main1(string[] args)
        {
            InitDb();

            await using var context = new CompanyContext();
            var company = new Company(Guid.NewGuid(), "My Company");
            company.AssignBillingAddress(new BillingAddress("Sofia", "Billing Address", "test"));
            company.AssignShippingAddress(new ShippingAddress("Plovdiv", "Shipping Address"));
            context.Companies.Add(company);

            var people = new List<Person>()
            {
                new Employee(Guid.NewGuid()),
                new Student(Guid.NewGuid()),
            };
            await context.AddRangeAsync(people);

            await context.SaveChangesAsync();

            var companies = await context.Set<Company>()
                .Where(p => p.Name == "My Company").ToListAsync();
            foreach (var myCompany in companies)
            {
                Console.WriteLine($"Name: {myCompany.Name}");
                
                Console.WriteLine($"Addresses:");
                
                foreach (var billingAddress in myCompany.BillingAddresses)
                {
                    Console.WriteLine($"\t- Billing address: {billingAddress.AddressLine1}, {billingAddress.City}");
                }

                Console.WriteLine("-------------------");

                foreach (var shippingAddress in myCompany.ShippingAddresses)
                {
                    Console.WriteLine($"\t- Shipping address: {shippingAddress.AddressLine1}, {shippingAddress.City}");
                }

                Console.WriteLine("=============================");
            }

            Console.ReadLine();
        }

        private static void InitDb()
        {
            using var context = new CompanyContext();
            context.Database.EnsureCreated();
            context.Database.Migrate();
        }
    }
}