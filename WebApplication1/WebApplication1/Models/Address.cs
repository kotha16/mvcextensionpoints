using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.Infrastructure;

namespace WebApplication1.Models
{
    public class Address : IValidatableObject
    {
        /*
        Attributes
        Reusable, SOC, Inheritance

        Downside: Cross Property Validation

        IValidatableObject
        Easy Cross Property Validation
        Places Logic in Model...Limited Reusability

        MVC: Validation during Model Binder.... Check Validation Attributes, Check IValidatable Object

        Populate ModelState
        */

        [BirthdateValidator]
        public DateTime? Dob { get; set; }
        public DateTime? Doe { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (Dob.Value > Doe.Value)
            {
                results.Add(new ValidationResult("Dob cannot be greater then Doe"));

            }

            return results;
        }
    }
}