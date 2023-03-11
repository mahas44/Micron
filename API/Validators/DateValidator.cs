using Models;
using Microsoft.OpenApi.Extensions;
using System.Reflection;

namespace API.Validators
{
    public class DateValidator<T> : IValidator<T>
    {
        public List<(bool, CustomException)> Validate(T value, IDictionary<string, object>? attrValues, string source, PropertyInfo pi)
        {
            List<(bool, CustomException)>? errorList = new List<(bool, CustomException)> ();
            if(!DateTime.TryParse(value.ToString(), out DateTime dt))
            {
                throw new ArgumentException("T must be proper System.DateTime");
            }
            
            string stringValue = value.ToString();
            attrValues.TryGetValue("MinYear", out object? minYear);

            if (DateTime.Parse(stringValue).Year < 1990)
            {
                Console.WriteLine("Time is too old. Wrong date!");

                errorList.Add((false, new CustomException
                {
                    ErrorMessage = $"Time is too old. Wrong Date! Year Must Bigger Than > {minYear}",
                    Source = source,
                    ErrorType = ErrorType.Error,
                    IsSuccesful = false
                }));
            }
            if (errorList.Count == 0) Console.WriteLine("All tests succesful");

            return errorList;
        }

    }
}
