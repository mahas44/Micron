namespace API.Validators
{
    public static class ValidatorFactory<T>
    {
        static Dictionary<string, IValidator<T>> validatorList = new();
        public static IValidator<T> GetValidator(Type type)
        {
            if (validatorList.Count == 0)
            {
                validatorList.Add("DateData", new DateValidator<T>());
                validatorList.Add("StringData", new StringValidator<T>());
                validatorList.Add("Default", new DefaultValidator<T>());
            }
            return validatorList.ContainsKey(type.Name) ? validatorList[type.Name] : validatorList["Default"];

        }
    }
}
