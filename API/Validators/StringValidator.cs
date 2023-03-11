using Models;
using System.Reflection;

namespace API.Validators
{
    public class StringValidator<T> : IValidator<T>
    {
        public List<(bool, CustomException)> Validate(T value, IDictionary<string, object>? attrValues, string source, PropertyInfo pi)
        {
            List<(bool, CustomException)>? errorList = new List<(bool, CustomException)>();
            if (!typeof(T).IsValueType && typeof(T) != typeof(String))
            {
                throw new ArgumentException("T must be a value type or System.String.");
            }

            string stringValue = value.ToString();
            List<string> invalidChars = new List<string>() { "!", "@", "#", "$", "%", "^", "&", "*", "(", ")" };
            attrValues.TryGetValue("Max", out object? max);
            attrValues.TryGetValue("Min", out object? min);

            // Check for length
            if (max != null && stringValue.Length > Convert.ToInt32(max))
            {
                Console.WriteLine("String too long");
                errorList.Add((false, new CustomException
                {
                    ErrorMessage = $"String too Long. Text Must Shorter Than < {max}",
                    Source = source,
                    IsSuccesful = false,
                    ErrorType = ErrorType.Error
                }));
            }

            if (min != null && stringValue.Length < Convert.ToInt32(min))
            {
                Console.WriteLine("String too short");
                errorList.Add((false, new CustomException
                {
                    ErrorMessage = $"String too Short. Text Must Longer Than < {min}",
                    Source = source,
                    IsSuccesful = false,
                    ErrorType = ErrorType.Error
                }));
            }

            foreach (string s in invalidChars)
            {
                if (stringValue.Contains(s))
                {
                    Console.WriteLine("String contains invalid character: " + s);
                    CustomException exception = new CustomException {
                        ErrorMessage = "String contains invalid character: " + s,
                        Source = source,
                        IsSuccesful = false,
                        ErrorType = ErrorType.Error
                    };
                    errorList.Add((false, exception));
                    break;
                }
            }
            if (errorList.Count == 0) Console.WriteLine("All tests succesful");
            return errorList;
        }
    }
}
