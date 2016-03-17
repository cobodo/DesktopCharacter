using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DesktopCharacter.ViewModel
{
    class TalkViewModel : Livet.ViewModel
    {
        private Queue<string> mMessageQueue = new Queue<string>();
        private BackgroundWorker mWorker;

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

        public TalkViewModel()
        {
            Util.WindowInstance.TalkInstance = this;

            mWorker = new BackgroundWorker();
            mWorker.DoWork += new DoWorkEventHandler(MessageWorker);
            mWorker.RunWorkerAsync();
        }

        private void MessageWorker(object sender, DoWorkEventArgs e)
        {
            while (!mWorker.CancellationPending)
            {
                string text = GetMessageSafe();
                if (text != "")
                {
                    for ( int i = 0; i < text.Length; ++i )
                    {
                        Text += text[i];
                        Thread.Sleep(50);
                    }
                    Thread.Sleep(200);
                    Text = "";
                }
            }
            e.Cancel = true;
        }

        private string GetMessageSafe()
        {
            if ( mMessageQueue.Count > 0 )
            {
                return mMessageQueue.Dequeue();
            }
            return "";
        }

        public void AddMessage( string text )
        {
            mMessageQueue.Enqueue( text );
        }

    }
}
