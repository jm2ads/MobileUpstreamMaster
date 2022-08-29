using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Core.Commons.Validations
{
    public interface IValidationRule<T>
    {
        string ValidationMessage { get; set; }

        bool Checked(T value);
    }
}
