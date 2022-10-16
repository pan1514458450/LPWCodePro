using LPWService.StaticFile;
using System.ComponentModel.DataAnnotations;

namespace LPWService.ModelValidation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RedisKeyValiddation : ValidationAttribute
    {

        public sealed override bool IsValid(object? value)
        {
            return true;
        }
        protected sealed override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var array = value.ToString().Split(',');
            if (array.Length != 2)
                return new ValidationResult(ErrorMessage);

            var result = ExtensionMethods.csredis.GetDeleteRedis<string>(array[0]).Result;
            if (result == array[1].ToUpper())
            {
                return base.IsValid(value, validationContext);
            }
            return new ValidationResult(ErrorMessage);
        }
    }
}
