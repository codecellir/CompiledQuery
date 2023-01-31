using System.ComponentModel.DataAnnotations;

namespace CompiledQueryTest
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
    }
}
