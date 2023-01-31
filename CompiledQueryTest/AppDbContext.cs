using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompiledQueryTest
{
    public class AppDbContext : DbContext
    {
        private static readonly Func<AppDbContext, int, Customer?> GetById =
            EF.CompileQuery((AppDbContext context, int id) => context.Set<Customer>().FirstOrDefault(x => x.Id == id));

        private static readonly Func<AppDbContext, int, Customer?> GetByIdNonTracking =
            EF.CompileQuery((AppDbContext context, int id) => context.Set<Customer>().AsNoTracking().FirstOrDefault(x => x.Id == id));

        private static readonly Func<AppDbContext, int, string, Task<Customer?>> GetByAgeAndName =
            EF.CompileAsyncQuery((AppDbContext context, int age, string name) => context.Set<Customer>().FirstOrDefault(x => x.Name == name && x.Age == age));
        public Customer? GetCustomerById(int id)
        {
            return Set<Customer>().FirstOrDefault(x => x.Id == id);
        }
        public Customer? GetCustomerByIdNonTracking(int id)
        {
            return Set<Customer>().AsNoTracking().FirstOrDefault(x => x.Id == id);
        }
        public Customer? GetCustomerByIdCompiled(int id)
        {
            return GetById(this, id);
        }
        public Customer? GetCustomerByIdNonTrackingCompiled(int id)
        {
            return GetByIdNonTracking(this, id);
        }
        public async Task<Customer?> GetCustomerByAgeAndNameCompiled(int age, string name)
        {
            return await GetByAgeAndName(this, age, name);
        }
        public async Task<Customer?> GetCustomerByAgeAndName(int age, string name)
        {
            return await Set<Customer>().FirstOrDefaultAsync(x => x.Name == name && x.Age == age);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(builder =>
            {
                List<Customer> customers = new();

                for (int i = 0; i < 10_000; i++)
                {
                    customers.Add(new Customer
                    {
                        Id = i + 1,
                        Age = Faker.RandomNumber.Next(15, 50),
                        Name = Faker.Name.FullName()
                    });
                }

                builder.HasData(customers);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=compiledQueryDb;User ID=sa;Password=abc_123;TrustServerCertificate=True");
        }
    }
}
