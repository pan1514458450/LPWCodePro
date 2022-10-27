using LPWService.StaticFile;
using System.ComponentModel.DataAnnotations;

namespace LPWService.ModelValidation
{
    //[AttributeUsage(AttributeTargets.Class|AttributeTargets.Property)]
    public class SgValidation : ValidationAttribute
    {

        public sealed override bool IsValid(object? value)
        {
            return true;
        }


        protected sealed override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
                return base.IsValid(value, validationContext);
            if (value.CheckSgReflection())
                return base.IsValid(value, validationContext);
            else
                return new ValidationResult(ErrorMessage);
        }
    }
}
