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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback">コールバックページへ移動した時のイベント</param>
        public SlackSignInViewModel(Callback callback)
        {
            _callback = callback;
        }


        public async void Complete(Uri uri)
        {
            await Task.Run(() => _callback.Invoke(uri));
        }
    }
}
