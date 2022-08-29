
namespace Frontend.Core.Commons.Validations
{
    public class IsNotNullOrEmptyRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Checked(T value)
        { 
            var str = value?.ToString();
            return !string.IsNullOrWhiteSpace(str) && !string.IsNullOrEmpty(str);
        }
    }
}
