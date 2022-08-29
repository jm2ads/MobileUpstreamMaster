using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Business.Materiales.Validations
{
    public class MaterialValidator
    {
        public bool IsEqual(Material materialA, Material materialB)
        {
            return materialA.Codigo == materialB.Codigo;
        }
    }
}
