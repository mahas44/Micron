using Models;
using System.Reflection;

namespace API.Validators
{
    public interface IValidator<T>
    {
        List<(bool, CustomException)> Validate(T value, IDictionary<string, object>? attrValues, string source, PropertyInfo pi);
    }
}
