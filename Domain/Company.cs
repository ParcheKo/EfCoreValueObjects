using System;
using System.Collections.Generic;
using System.Linq;

namespace EfCoreValueObjects
{
    public class Company
    {
        private readonly List<BillingAddress> _billingAddresses = new();
        private readonly List<ShippingAddress> _shippingAddresses = new();

        public Company(Guid id, string name)
        {
            Assertions.AssertNotNullAndNotEmpty(name, "Must provide name");

            this.Id = id;
            this.Name = name;
        }

        public Guid Id { get; }
        
        public string Name { get; }

        public IReadOnlyCollection<BillingAddress> BillingAddresses => this._billingAddresses;
        public IReadOnlyCollection<ShippingAddress> ShippingAddresses => this._shippingAddresses;

        public void AssignBillingAddress(BillingAddress billingAddress)
        {
            Assertions.AssertNotNull(billingAddress, "Must provide address");

            var exists = this._billingAddresses.Contains(billingAddress);

            if (!exists)
            {
                this._billingAddresses.Add(billingAddress);
            }
        }
        
        public void AssignShippingAddress(ShippingAddress shippingAddress)
        {
            Assertions.AssertNotNull(shippingAddress, "Must provide address");

            var exists = this._shippingAddresses.Contains(shippingAddress);

            if (!exists)
            {
                this._shippingAddresses.Add(shippingAddress);
            }
        }
    }
}