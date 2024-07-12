namespace Core.CrossCuttingConcerns.Validation
{
    public class CustomValidationAttribute : Attribute
    {
        public Type ValidatorType { get; }

        public CustomValidationAttribute(Type validatorType)
        {
            ValidatorType = validatorType;
        }
    }
}

