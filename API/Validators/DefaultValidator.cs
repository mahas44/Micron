using Models;
using System.Reflection;

namespace API.Validators
{
    public record DefaultValidator<T>() : IValidator<T>
    {
        public List<(bool, CustomException)> Validate(T value, IDictionary<string, object>? attrValues, string source, PropertyInfo pi)
        {
            return new List<(bool, CustomException)>();
        }
    }
}
