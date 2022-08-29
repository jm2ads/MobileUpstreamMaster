
using Xamarin.Forms;

namespace Frontend.Core.Commons.Validations
{
    public class IsLengthEqualToRule<T> : IValidationRule<T>
    {
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(T), typeof(Entry), default(T));

        public int Value{ get; set; }

        public string ValidationMessage { get; set; }

        public bool Checked(T value)
        { 
            if (value == null)
            {
                return false;
            }
            var str = value.ToString();
            return str.Length == Value;
        }
    }
}
