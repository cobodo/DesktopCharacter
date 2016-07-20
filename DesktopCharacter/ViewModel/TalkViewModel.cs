using System;
using System.Threading;
using DesktopCharacter.Model;
using System.Reactive.Linq;
using DesktopCharacter.Util.Thread;
using System.Windows.Media;
using System.Globalization;
using System.Windows;
using System.Windows.Media.Animation;
using NLog;

namespace DesktopCharacter.ViewModel
{
    class TalkViewModel : Livet.ViewModel
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

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

        private readonly CharacterNotify _model = CharacterNotify.Instance;
        public double Width { get; set; }

        private string _line1 = "";
        public string Line1
        {
            get { return _line1; }
            set
            {
                _line1 = value;
                this.RaisePropertyChanged("Line1");
                
            }
        }
        private string _line2 = "";
        public string Line2
        {
            get { return _line2; }
            set
            {
                _line2 = value;
                this.RaisePropertyChanged("Line2");

            }
        }
        private string _line3 = "";
        public string Line3
        {
            get { return _line3; }
            set
            {
                _line3 = value;
                this.RaisePropertyChanged("Line3");

            }
        }
        private string _line4 = "";
        public string Line4
        {
            get { return _line4; }
            set
            {
                _line4 = value;
                this.RaisePropertyChanged("Line4");

            }
        }
        private string _line5 = "";
        public string Line5
        {
            get { return _line5; }
            set
            {
                _line5 = value;
                this.RaisePropertyChanged("Line5");

            }
        }

        public Storyboard StoryBoard { get; set; }

        private CharacterNotify model = CharacterNotify.Instance;

        public TalkViewModel()
        {
            _executor = new SingleThreadExecutor();
            _model.TalkSubject.Subscribe(AddMessage);
        }

        private void AddMessage( string text )
        {
            _executor.PostAction(() =>
            {
                var textArray = CalclateTextArray(text);
                Line1 = textArray[0];
                Line2 = textArray[1];
                Line3 = textArray[2];
                Line4 = textArray[3];
                Line5 = textArray[4];
                Application.Current.Dispatcher.Invoke(() =>
                {
                    StoryBoard.Begin();
                });

                Thread.Sleep(CalclateWaitTime(textArray));
            });
        }

        private int CalclateWaitTime(string[] textArray)
        {
            int waitTime = 2000;
            foreach (var line in textArray)
            {
                if (line.Length != 0)
                {
                    waitTime += 1000;
                }
                else
                {
                    break;
                }
            }
            return waitTime;
        }

        //与えられたテキストをウィンドウ内に収まるように配列に分割します
        private string[] CalclateTextArray(string text)
        {
            int lineCount = 5; //最大行数
            string[] array = new string[lineCount];
            for (int i = 0; i < lineCount; i++)
            {
                array[i] = "";
            }

            double windowWidth = 400;//ウィンドウの幅

            int currentLine = 0;//処理中の行

            //ループでゴリ押ししてる。
            //今のところ処理速度に問題ないけど、
            //今後問題出てきたら効率のよい探索に書き換える必要がある
            while (text.Length != 0 && currentLine < lineCount)
            {
                string line = "";
                //現在の行が枠の外にでるまで
                while (true)
                {
                    //改行文字なら一文字削除してループを抜けて改行
                    if (text[0] == '\n')
                    {
                        array[currentLine] = line;
                        text = text.Substring(1, text.Length - 1);
                        break;
                    }

                    //先頭から一文字取得
                    line += text[0];
                    var formattedText = new FormattedText(line, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Yu Gothic UI"), 22, Brushes.Black);

                    //幅を超えた
                    if (windowWidth < formattedText.Width)
                    {
                        //一番後ろの文字を削除した文字列を入れる
                        array[currentLine] = line.Substring(0, line.Length - 1);
                        break;
                    }
                    //すべての文字を処理した場合
                    if (text.Length == 1)
                    {
                        array[currentLine] = line;
                        text = "";
                        break;
                    }
                    //先頭から一文字削除
                    text = text.Substring(1, text.Length - 1);
                }
                //次の行
                currentLine++;
            }

            return array;
        }

    }
}
