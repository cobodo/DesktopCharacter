using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;
using System.Collections;

namespace DesktopCharacter.ViewModel
{
    class TalkViewModel : Livet.ViewModel
    {
        private ConcurrentQueue<string> mMessageQueue = new ConcurrentQueue<string>();
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
                string text;
                if (GetMessageSafe( out text ))
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

        private bool GetMessageSafe( out string value )
        {
            return mMessageQueue.TryDequeue( out value );
        }

        public void AddMessage( string text )
        {
            mMessageQueue.Enqueue( text );
        }

    }
}
