using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheckListApp.CustomValidation
{
    public class DateDueAttributes : ValidationAttribute
    {
        public DateDueAttributes() { }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;
            return ((DateTime)value < DateTime.Now) ? new ValidationResult("Task due date cannot be in the past") : ValidationResult.Success; 
        }

    }
}
