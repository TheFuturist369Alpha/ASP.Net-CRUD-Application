using System;
using ServiceContracts.Enums;
using Entities;
using System.ComponentModel.DataAnnotations;


namespace ServiceContracts.DTO
{
    public class PersonAddRequest
    {
        [Required(ErrorMessage ="Name Required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Email Required")]
        [EmailAddress(ErrorMessage ="email not in the right format")]
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Password { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public Guid CountryId { get; set; }
        public bool RecieveNewsLetters { get; set; }

        public Person ToPerson()
        {
            return new Person()
            {
                PersonId=Guid.NewGuid(),
                Name = Name,
                Email = Email,
                DateOfBirth = DateOfBirth,
                Password = Password,
                Address = Address,
                CountryId = CountryId,
                RecieveNewsLetters = RecieveNewsLetters,
                Gender=Gender

            };
        }
    }
}
