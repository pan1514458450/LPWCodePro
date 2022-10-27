using LPWService.StaticFile;
using System.ComponentModel.DataAnnotations;

namespace LPWService.ModelValidation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RedisKeyValiddation : ValidationAttribute
    {

        public sealed override bool IsValid(object? value)
        {
            var array = value.ToString().Split(',');
            if (array.Length != 2) return false;
                

            var result = ExtensionMethods.csredis.GetDeleteRedis<string>(array[0]).Result;
             return result == array[1].ToUpper();
        }
    }
}
