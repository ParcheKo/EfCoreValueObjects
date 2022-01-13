using System;
using System.Collections.Generic;
using System.Linq;

namespace EfCoreValueObjects
{
    public class Company
    {
        private readonly List<CompanyAddress> _addresses = new();

        public Company(Guid id, string name)
        {
            Assertions.AssertNotNullAndNotEmpty(name, "Must provide name");

            this.Id = id;
            this.Name = name;
        }

        public Guid Id { get; }
        
        public string Name { get; }

        public IReadOnlyCollection<CompanyAddress> Addresses
        {
            get
            {
                return this._addresses;
            }
        }

        public void AssignAddress(CompanyAddress address)
        {
            Assertions.AssertNotNull(address, "Must provide address");

            var exists = this._addresses.Contains(address);

            if (!exists)
            {
                this._addresses.Add(address);
            }
        }
    }
}