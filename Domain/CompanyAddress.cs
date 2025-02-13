using System;
using System.Collections.Generic;
using System.Linq;

namespace EfCoreValueObjects
{
    public abstract class CompanyAddress : ValueObject
    {
        protected CompanyAddress(string city, string addressLine1)
        {
            Assertions.AssertNotNullAndNotEmpty(city, "Must provide city");
            Assertions.AssertNotNullAndNotEmpty(addressLine1, "Must provide address line");

            this.City = city;
            this.AddressLine1 = addressLine1;
        }

        public string City { get; }

        public string AddressLine1 { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.City;
            yield return this.AddressLine1;
            yield return this.Type;
        }

        public abstract AddressType Type { get; protected set; }
    }

    public enum AddressType
    {
        Billing = 1,
        Shipping = 2
    }

    public class BillingAddress : CompanyAddress
    {
        public BillingAddress(string city, string addressLine1, string billingAddressCustomInfo)
            : base(city, addressLine1)
        {
            BillingAddressCustomInfo = billingAddressCustomInfo;
            CreationMoment= DateTimeOffset.UtcNow;
        }

        public string BillingAddressCustomInfo { get; private set; }

        public DateTimeOffset CreationMoment { get; private set; }
        
        public override AddressType Type
        {
            get => AddressType.Billing;
            protected set { }
        }
        
        public BillingAddress Clone()
        {
            return new BillingAddress(City, AddressLine1, BillingAddressCustomInfo);
        }
    }

    public class ShippingAddress : CompanyAddress
    {
        public ShippingAddress(string city, string addressLine1)
            : base(city, addressLine1)
        {
        }

        public string ShippingAddressCustomInfo { get; private set; }

        public override AddressType Type
        {
            get => AddressType.Shipping;
            protected set { }
        }
    }
}