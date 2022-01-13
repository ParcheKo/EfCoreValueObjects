using Microsoft.EntityFrameworkCore;

namespace EfCoreValueObjects
{
    public class CompanyContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=ValueObjectsEFCore22;Integrated Security=true;uid=sa;pwd=A123456r@!");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(
                c =>
                {
                    c.HasKey("Id");
                    c.Property(e => e.Name);
                });

            // modelBuilder.Entity<Company>().OwnsMany<CompanyAddress>("Addresses", a =>
            // {
            //     a.HasForeignKey("CompanyId");
            //     a.Property(ca => ca.City);
            //     a.Property(ca => ca.AddressLine1);
            //     a.HasKey("CompanyId", "City", "AddressLine1");
            // });

            modelBuilder.Entity<Company>().OwnsMany(
                c => c.Addresses,
                a =>
                {
                    var foreignKeyPropertyName = $"{nameof(Company)}{nameof(Company.Id)}";
                    a.WithOwner()
                        // .Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field)
                        .HasForeignKey(foreignKeyPropertyName);
                    // a.HasForeignKey("CompanyId");
                    a.Property(ca => ca.City);
                    a.Property(ca => ca.AddressLine1);
                    a.HasKey(
                        foreignKeyPropertyName,
                        nameof(CompanyAddress.City),
                        nameof(CompanyAddress.AddressLine1),
                        nameof(CompanyAddress.Type)
                    );
                    // a.ToTable(nameof(CompanyAddress)).HasDiscriminator<AddressType>(nameof(CompanyAddress.Type))
                    //     .HasValue<BillingAddress>(AddressType.Billing)
                    //     .HasValue<ShippingAddress>(AddressType.Shipping);
                });

            modelBuilder.Entity<Person>()
                .HasKey(p=>p.Id);
            modelBuilder.Entity<Person>().ToTable(nameof(Person))
                .HasDiscriminator<PersonTypes>(nameof(Person.Type))
                .HasValue<Employee>(PersonTypes.Employee)
                .HasValue<Student>(PersonTypes.Student);
        }
    }
}