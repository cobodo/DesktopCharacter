using System;

using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace DesktopCharacter.Model
{
    /// <summary>
    /// Characterの発言を操作する機能を提供するモデル
    /// </summary>
    sealed class CharacterTalkModel
    {
        public Subject<string> TalkSubject { get; } = new Subject<string>();

        /// <summary>
        /// Modelのインスタンス
        /// </summary>
        public static CharacterTalkModel Instance { get; } = new CharacterTalkModel();

        private CharacterTalkModel()
        {

        }

        /// <summary>
        /// Characterに発言をさせる
        /// </summary>
        /// <param name="text">発言させる内容</param>
        public void Talk(string text)
        {
            TalkSubject.OnNext(text);
        }
    }
}
