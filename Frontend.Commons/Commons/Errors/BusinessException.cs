using System;

namespace Frontend.Commons.Commons.Errors
{
    public class BusinessException : Exception
    {
        public string Codigo { get ; set; }

        public string Mensaje { get; set; }

        public BusinessException(string code, string message)
        {
            Codigo = code;
            Mensaje = message;
        }
    }
}
