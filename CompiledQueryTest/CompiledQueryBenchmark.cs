using BenchmarkDotNet.Attributes;

namespace CompiledQueryTest
{
    [MemoryDiagnoser]
    public class CompiledQueryBenchmark
    {
        //private const int _customerId = 7000;
        private const int _age = 33;
        private const string _name = "Carli Hermiston";

        //[Benchmark]
        //public Customer? GetCustomerById()
        //{
        //    using var context = new AppDbContext();
        //    return context.GetCustomerById(_customerId);
        //}

        //[Benchmark]
        //public Customer? GetCustomerByIdCompiled()
        //{
        //    using var context = new AppDbContext();
        //    return context.GetCustomerByIdCompiled(_customerId);
        //}


        //[Benchmark]
        //public Customer? GetCustomerByIdNonTracking()
        //{
        //    using var context = new AppDbContext();
        //    return context.GetCustomerByIdNonTracking(_customerId);
        //}

        //[Benchmark]
        //public Customer? GetCustomerByIdNonTrackingCompiled()
        //{
        //    using var context = new AppDbContext();
        //    return context.GetCustomerByIdNonTrackingCompiled(_customerId);
        //}

        [Benchmark]
        public async Task<Customer?> GetCustomerByIdAndName()
        {
            using var context = new AppDbContext();
            return await context.GetCustomerByAgeAndName(_age,_name);
        }

        [Benchmark]
        public async Task<Customer?> GetCustomerByIdAndNameCompiled()
        {
            using var context = new AppDbContext();
            return await context.GetCustomerByAgeAndNameCompiled(_age,_name);
        }
    }
}
