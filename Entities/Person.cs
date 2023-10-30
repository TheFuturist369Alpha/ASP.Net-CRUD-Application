using System;
using System.ComponentModel.DataAnnotations;


namespace Entities
{
    public class Person
    {
        [Key]
        public Guid PersonId { get; set; }
       
        public string? Name { get; set; }
       
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        
        public string? Password { get; set; }
        public string? Gender { get; set; }
       
        public string? Address { get; set; }
        public Guid? CountryId { get; set; }
        public bool RecieveNewsLetters { get; set; }
        public string? TIN { get; set; }
        
      



    }
}
