
using Xamarin.Forms;

namespace Frontend.Core.Commons.Validations
{
    public class IsLowerThanRule<T> : IValidationRule<T>
    {
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(T), typeof(Entry), default(T));

        public T Value{ get; set; }

        public string ValidationMessage { get; set; }

        public bool Checked(T value)
        {
            double doubleValue;
            var isValidDouble = double.TryParse(value.ToString(), out doubleValue);

            double doubleValueComparer;
            var isValidDoubleComparer = double.TryParse(Value.ToString(), out doubleValueComparer);


            return isValidDouble && isValidDoubleComparer && doubleValue < doubleValueComparer;
        }
    }
}
