using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EfCoreValueObjects
{
    class VO_Demo
    {
        static async Task Main(string[] args)
        {
            InitDb();

            await using var context = new CompanyContext();

            // TESTING IF .NET REMOVE A NEWLY-CREATED (DEEPLY-CLONED) VALUE-OBJECT 
            // FROM THE VALUE-OBJECT COLLECTION OR NOT? (ANSWER = YES)
            var addressBook = new List<BillingAddress>();
            var address = new BillingAddress(
                "Tehran",
                "TehranSar",
                "1st");
            // addressBook.Add(address);
            var equivalentAddress = address.Clone();
            //
            // Console.WriteLine($"AddressBook before operation");
            // Print(addressBook);
            // addressBook.Remove(equivalentAddress);
            // Console.WriteLine($"AddressBook after operation");
            // Print(addressBook);

            // TESTING IF EF DELETE/INSERT A VALUE-OBJECT RECORD FROM MAPPED TABLE AN OWNED ENTITY COLLECTION
            // WHEN REMOVING/ADDING THE SAME EQUAL VALUE-OBJECT OR NOT? (ANSWER = YES)
            var company = new Company(Guid.NewGuid(), "test1");
            company.AssignBillingAddress(address);
            var anotherAddress = new BillingAddress(
                "Nowshahr",
                "Plage Hosseini",
                "1st");
            company.AssignBillingAddress(anotherAddress);
            context.Add(company);
            await context.SaveChangesAsync();

            company.RemoveAllBillingAddresses();
            var thirdAddress = new BillingAddress(
                "Nowshahr",
                "Plage Hosseini",
                "1st");
            company.AssignBillingAddress(thirdAddress);
            await context.SaveChangesAsync();

            Console.ReadLine();
        }

        private static void Print(List<BillingAddress> addressBook)
        {
            if (!addressBook.Any())
            {
                Console.WriteLine("No addresses to display!");
                return;
            }

            // Console.WriteLine($"AddressBook:");
            foreach (var address in addressBook)
            {
                Console.WriteLine($"\tAddress: {address.AddressLine1}, {address.City}");
            }

            Console.WriteLine(Environment.NewLine);
        }

        private static void InitDb()
        {
            using var context = new CompanyContext();
            context.Database.EnsureCreated();
            context.Database.Migrate();
        }
    }
}