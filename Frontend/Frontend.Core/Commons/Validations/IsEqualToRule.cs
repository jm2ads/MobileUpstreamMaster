
using Xamarin.Forms;

namespace Frontend.Core.Commons.Validations
{
    public class IsEqualToRule<T> : IValidationRule<T>
    {
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(T), typeof(Entry), default(T));

        public T Value{ get; set; }

        public string ValidationMessage { get; set; }

        public bool Checked(T value)
        {
            return Value.Equals(value);
        }
    }
}
