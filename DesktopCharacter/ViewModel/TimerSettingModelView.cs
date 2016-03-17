using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.Windows;

namespace DesktopCharacter.ViewModel
{
    class TimerSettingModelView : Livet.ViewModel
    {
        private ModelView mModelView = Util.WindowInstance.MainInstnace;

        public TimerSettingModelView()
        {
        }

        private int mTimerCount;
        public int TimerCount
        {
            get { return mTimerCount; }
            set
            {
                mTimerCount = value;
                this.RaisePropertyChanged("TimerCount");
            }
        }

        private bool mMessage_60;
        public bool Message_60
        {
            get { return mMessage_60; }
            set
            {
                mMessage_60 = value;
                this.RaisePropertyChanged("Message_60");
            }
        }

        private bool mMessage_30;
        public bool Message_30
        {
            get { return mMessage_30; }
            set
            {
                mMessage_30 = value;
                this.RaisePropertyChanged("Message_60");
            }
        }

        private ViewModelCommand mStartCommand;
        public ViewModelCommand StartCommand
        {
            get
            {
                if( mStartCommand == null)
                {
                    mStartCommand = new ViewModelCommand
                        (
                            // 閉じてタイマーウィンドウを開く(その時TimerCountの数値を教えてやる
                            () =>
                            {
                                Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Close"));
                                mModelView.Messenger.Raise(new TransitionMessage(new TimerModelView(mTimerCount), "Timer"));
                            }
                        );
                }
                return mStartCommand;
            }
        }
    }
}
