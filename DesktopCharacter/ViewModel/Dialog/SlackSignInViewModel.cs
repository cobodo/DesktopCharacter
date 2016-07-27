using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet.Commands;

namespace DesktopCharacter.ViewModel.Dialog
{
    class SlackSignInViewModel: Livet.ViewModel
    {
        public delegate void Callback(Uri result);
        private readonly Callback _callback;

        public SlackSignInViewModel(Callback callback)
        {
            _callback = callback;
        }


        public void Complete(Uri uri)
        {
            _callback.Invoke(uri);
        }
    }
}
