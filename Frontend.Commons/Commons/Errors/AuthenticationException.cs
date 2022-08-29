using Frontend.Commons.Commons.Errors;

namespace Frontend.Commons.Commons.Errors
{
    public class AuthenticationException : BusinessException
    {
        public AuthenticationException(string code, string message) : base(code, message)
        {
        }
    }
}
