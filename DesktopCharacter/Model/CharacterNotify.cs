using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DesktopCharacter.Model
{
    class CharacterNotify
    {
        public Subject<string> TalkSubject { get; } = new Subject<string>();

        public Subject<string> CharacterLoadSubject { get; } = new Subject<string>();

        public Subject<string> SetAnimationSubject { get; } = new Subject<string>();

        public Subject<bool> TopMostMessageSubject { get; } = new Subject<bool>();

        public Subject<Point> WindowSizeMessageSubject { get; } = new Subject<Point>();

        /// <summary>
        /// CharacterPropertyNotifyのインスタンス
        /// </summary>
        public static CharacterNotify Instance { get; } = new CharacterNotify();

        private CharacterNotify()
        {

        }

        /// <summary>
        /// ロードするキャラクター名
        /// </summary>
        /// <param name="name">キャラクター名</param>
        public void CharacterLoad(string name)
        {
            CharacterLoadSubject.OnNext(name);
        }

        /// <summary>
        /// ロードするキャラクター名
        /// </summary>
        /// <param name="name">キャラクター名</param>
        public void SetAnimation(string name)
        {
            SetAnimationSubject.OnNext(name);
        }

        /// <summary>
        /// Characterに発言をさせる
        /// </summary>
        /// <param name="text">発言させる内容</param>
        public void Talk(string text)
        {
            TalkSubject.OnNext(text);
        }

        /// <summary>
        /// ウィンドウを常に上にするかどうかの切り替えメッセージを送る
        /// </summary>
        /// <param name="topmost">最前面フラグ</param>
        public void TopMostMessage( bool topmost)
        {
            TopMostMessageSubject.OnNext(topmost);
        }

        /// <summary>
        /// ウィンドウサイズを変更する
        /// </summary>
        /// <param name="windowSize"></param>
        public void WindowSizeMessage(Point windowSize)
        {
            WindowSizeMessageSubject.OnNext(windowSize);
        }
    }
}
