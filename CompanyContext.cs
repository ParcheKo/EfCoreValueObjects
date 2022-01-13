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
                c => c.BillingAddresses,
                a =>
                {
                    var foreignKeyPropertyName = $"{nameof(Company)}{nameof(Company.Id)}";
                    a.WithOwner()
                        .HasForeignKey(foreignKeyPropertyName);
                    a.Property(ca => ca.City);
                    a.Property(ca => ca.AddressLine1);
                    a.HasKey(
                        foreignKeyPropertyName,
                        nameof(CompanyAddress.City),
                        nameof(CompanyAddress.AddressLine1),
                        nameof(CompanyAddress.Type)
                    );
                });
            modelBuilder.Entity<Company>().OwnsMany(
                c => c.ShippingAddresses,
                a =>
                {
                    var foreignKeyPropertyName = $"{nameof(Company)}{nameof(Company.Id)}";
                    a.WithOwner()
                        .HasForeignKey(foreignKeyPropertyName);
                    a.Property(ca => ca.City);
                    a.Property(ca => ca.AddressLine1);
                    a.HasKey(
                        foreignKeyPropertyName,
                        nameof(CompanyAddress.City),
                        nameof(CompanyAddress.AddressLine1),
                        nameof(CompanyAddress.Type)
                    );
                });

            modelBuilder.Entity<Person>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Person>().ToTable(nameof(Person))
                .HasDiscriminator<PersonTypes>(nameof(Person.Type))
                .HasValue<Employee>(PersonTypes.Employee)
                .HasValue<Student>(PersonTypes.Student);
        }
    }
}