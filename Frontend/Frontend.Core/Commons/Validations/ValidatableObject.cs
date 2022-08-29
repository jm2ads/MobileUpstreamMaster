using Frontend.Core.Commons.Observables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Frontend.Core.Commons.Validations
{
    public class ValidatableObject<T> : NotificationObject
    {
        private IList<IValidationRule<T>> validations;
        public IList<IValidationRule<T>> Validations
        {
            get { return validations; }
            set{ SetProperty(ref validations, value);} 
        }

        private T _value;
        public T Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value, onChanged: OnValueChanged);
            }
        }

        private bool isValid;
        public bool IsValid
        {
            get { return isValid; }
            set { SetProperty(ref isValid, value); }
        }

        public Action OnValueChanged { get; }

        private IEnumerable<string> errors;
        public IEnumerable<string> Errors
        {
            get { return errors; }
            set { SetProperty(ref errors, value); } 
        }

        public ValidatableObject(Action onValueChanged = null)
        {
            validations = new List<IValidationRule<T>>();
            errors = new List<string>();
            IsValid = true;
            OnValueChanged = onValueChanged;
        }

        public bool Validate()
        {
            Errors = new List<string>();

            IEnumerable<string> errors = validations
                .Where(v => !v.Checked(Value))
                .Select(v => v.ValidationMessage);

            Errors = errors.ToList();
            IsValid = !Errors.Any();

            return IsValid;
        }
    }
}
