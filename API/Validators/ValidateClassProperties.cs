using Models;
using System.Reflection;

namespace API.Validators
{
    public class ValidateClassProperties
    {
        public static List<(bool, CustomException)> GetValidateResult(object model)
        {
            var errorList = new List<(bool, CustomException)>();
            bool isError = false;

            PropertyInfo[] properties = model.GetType()
             .GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo property in properties)
            {
                for (int i = 0; i < property.GetCustomAttributes(true).Length; i++)
                {
                    Type type = property.GetCustomAttributes(true)[i].GetType();
                    IValidator<string>? validator = ValidatorFactory<string>.GetValidator(type);
                    string propValue = property.GetValue(model).ToString();
                    IDictionary<string, object> attrValues = new Dictionary<string, object>();

                    if (property.GetCustomAttributesData()[i].NamedArguments.Count > 0)
                    {
                        foreach (var item in property.GetCustomAttributesData()[i].NamedArguments)
                        {
                            attrValues.Add(item.MemberName, item.TypedValue.Value);
                        }
                    }

                    isError = false;

                    foreach (var err in validator.Validate(propValue, attrValues, property.Name, property))
                    {
                        isError = true;
                        errorList.Add(err);
                    }
                    if (isError) break;
                }
            }
            return errorList;
        }
    }
}
