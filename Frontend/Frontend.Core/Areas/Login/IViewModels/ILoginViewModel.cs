using Frontend.Core.Commons;

namespace Frontend.Core.Areas.Login.IViewModels
{
    public interface ILoginViewModel
    {
        void ValidateUsernameInput();

        void ValidatePasswordInput();
    }
}
