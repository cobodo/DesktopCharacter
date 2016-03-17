using Livet.Messaging.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Threading;

namespace DesktopCharacter.ViewModel
{
    class TimerViewModel : Livet.ViewModel
    {
        private DispatcherTimer mTimer;
        private int mCount;
        private DateTime mStartTime;
        private TimeSpan mNowTimeSpan;

        public TimerViewModel()
        {
            
        }

        public TimerViewModel(int count)
        {
            mCount = count;

            mTimer = new DispatcherTimer(DispatcherPriority.Normal);
            mTimer.Interval = new TimeSpan(0, 0, 0, 1);
            mTimer.Tick += new EventHandler((object sender, EventArgs e) =>
            {
                mNowTimeSpan = DateTime.Now.Subtract(mStartTime);
                Text = mNowTimeSpan.ToString(@"mm\:ss\:fff");
                if (TimeSpan.Compare(mNowTimeSpan, new TimeSpan(0, 0, count)) >= 0)
                {
                    Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Close"));
                }
            });

            mStartTime = DateTime.Now;
            mTimer.Start();
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
