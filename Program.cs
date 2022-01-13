using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EfCoreValueObjects
{
    class Program
    {
        static async Task Main(string[] args)
        {
            InitDb();

            await using var context = new CompanyContext();
            var company = new Company(Guid.NewGuid(), "My Company");
            company.AssignAddress(new CompanyAddress("Sofia", "Mlados1"));
            company.AssignAddress(new CompanyAddress("Plovdiv", "Mlados1"));
            context.Companies.Add(company);

            var people = new List<Person>()
            {
                new Employee(Guid.NewGuid()),
                new Student(Guid.NewGuid()),
            };
            await context.AddRangeAsync(people);
                
            await context.SaveChangesAsync();
        }

        private static void InitDb()
        {
            using var context = new CompanyContext();
            context.Database.EnsureCreated();
            context.Database.Migrate();
        }
    }
}
