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

            var addressBook = new List<BillingAddress>();
            var address = new BillingAddress("Tehran", "Tehransar");
            addressBook.Add(address);
            var equivalentAddress = address.Clone();

            Console.WriteLine($"AddressBook before");
            Print(addressBook);
            addressBook.Remove(equivalentAddress);
            Console.WriteLine($"AddressBook after");
            
            Console.ReadLine();
        }

        private static void Print(List<BillingAddress> addressBook)
        {
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