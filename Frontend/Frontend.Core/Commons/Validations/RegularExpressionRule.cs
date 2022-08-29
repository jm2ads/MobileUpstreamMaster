using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Frontend.Core.Commons.Validations
{
    public class RegularExpressionRule<T> : IValidationRule<T>
    {
        public static readonly BindableProperty RegularExpressionProperty = BindableProperty.Create(nameof(RegularExpression), typeof(string), typeof(Entry), default(string));

        public string RegularExpression { get; set; }

        public string ValidationMessage { get; set; }

        public bool Checked(T value)
        {
            var input = value?.ToString();
            var regex = new Regex(RegularExpression);
            var match = regex.IsMatch(input);

            return match;
        }
    }
}
