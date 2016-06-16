using System;
using System.ComponentModel;
using System.Threading;
using System.Collections.Concurrent;
using DesktopCharacter.Model;
using System.Reactive.Linq;
using DesktopCharacter.Util.Thread;

namespace DesktopCharacter.ViewModel
{
    class TalkViewModel : Livet.ViewModel
    {

        private string mText;
        public string Text
        {
            get { return mText; }
            set
            {
                mText = value;
                this.RaisePropertyChanged("Text");
            }
        }

        private readonly SingleThreadExecutor _executor;

        private readonly CharacterTalkModel _model = CharacterTalkModel.Instance;

        public TalkViewModel()
        {
            Util.WindowInstance.TalkInstance = this;

            _executor = new SingleThreadExecutor();
            _model.TalkSubject.Subscribe(AddMessage);
        }

        private void AddMessage( string text )
        {
            _executor.PostAction(() =>
            {
                foreach (var t in text)
                {
                    Text += t;
                    Thread.Sleep(50);
                }
                Thread.Sleep(200);
                Text = "";
            });
        }

    }
}
