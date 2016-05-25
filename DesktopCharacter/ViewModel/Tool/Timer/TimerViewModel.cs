using DesktopCharacter.Model;
using Livet.Messaging.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Threading;

namespace DesktopCharacter.ViewModel.Tool.Timer
{
    class TimerViewModel : Livet.ViewModel
    {

        public TimerViewModel()
        {
            
        }

        public TimerViewModel(TimerModel model)
        {
            model.StateObservable().Subscribe((TimerState state) => {
                if(state == TimerState.FINISHED)
                {
                    Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Close"));
                }
            });
            model.TimeObservable().Subscribe((int time) => {
                int h = time / (60 * 60);
                time -= h * (60 * 60);
                int m = time / 60;
                time -= m * 60;
                int s = time;
                Text = String.Format("{0:D2}:{1:D2}:{2:D2}", h, m, s);
            });
            model.Start();
        }

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
    }
}
