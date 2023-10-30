using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Country
    {
        [Key]
        public Guid Id { get; set; }
       
        public string? Name { get; set; }
    }
}