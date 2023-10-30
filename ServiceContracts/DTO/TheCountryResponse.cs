using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace ServiceContracts.DTO
{
    public class TheCountryResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }


       public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() != typeof(TheCountryResponse))
            {
                return false;
            }
            TheCountryResponse tr = (TheCountryResponse)obj;
            return (this.Id==tr.Id)&&(this.Name==tr.Name);
        }
    }

    public static class Extension
    {
        public static TheCountryResponse ToResponse(this Country response)
        {
            return new TheCountryResponse()
            {
                Name = response.Name,
                Id = response.Id
            }; 
        }
    }
}
