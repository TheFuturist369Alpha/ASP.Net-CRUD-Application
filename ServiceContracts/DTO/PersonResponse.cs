using System;
using System.Runtime.CompilerServices;
using Entities;
using ServiceContracts.Enums;



namespace ServiceContracts.DTO
{
    
    public class PersonResponse
    {
      
        public Guid PersonId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Password { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public Guid? CountryId { get; set; }
        public bool ReceiveNewsLetters { get; set; }
        public string? country { get; set; }

        private Country countryobj = new Country();

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() != typeof(PersonResponse))
            {
                return false;
            }
            PersonResponse tr = (PersonResponse)obj;
            return (this.PersonId == tr.PersonId) && (this.Name == tr.Name) && (this.Address==tr.Address)&&(this.CountryId==tr.CountryId);
        }

        public UpdatePerson Update()
        {
            return new UpdatePerson()
            {
                Id = PersonId, Name = Name, Address = Address,
                Email = Email,
                DateOfBirth = DateOfBirth, Password = Password,
                Gender = (GenderEnumeration)Enum.Parse(typeof(GenderEnumeration), Gender),
                CountryId=CountryId,
                RecieveNewsLetters=ReceiveNewsLetters,
                Country = country,
            };
        }
    }
    public static class PersonExtension
    {
        public static PersonResponse ToResponse(this Person p)
        {
            return new PersonResponse()
            {
                PersonId = p.PersonId,
                Name = p.Name,
                Email = p.Email,
                Address = p.Address,
                CountryId = p.CountryId,
                Gender = p.Gender,
                ReceiveNewsLetters = p.RecieveNewsLetters,
                Password = p.Password,
                DateOfBirth = p.DateOfBirth,
                Age=(p.DateOfBirth!=null)?(int)Math.Round((DateTime.Now - p.DateOfBirth.Value).TotalDays/365.25):null
            };
        }
    }

    
}
