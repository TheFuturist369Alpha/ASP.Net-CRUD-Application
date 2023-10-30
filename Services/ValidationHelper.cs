using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ValidationHelper
    {
        public static void Validate(object? obj)
        {
            ValidationContext cxt = new ValidationContext(obj);
            List<ValidationResult> results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(obj, cxt, results, true))
            {
                throw new ArgumentException(results.FirstOrDefault()?.ErrorMessage);
            }
        }

    }
}
