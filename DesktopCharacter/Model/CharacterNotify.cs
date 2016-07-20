using System.Reactive.Subjects;

namespace DesktopCharacter.Model
{
    class CharacterNotify
    {
        public Subject<string> TalkSubject { get; } = new Subject<string>();

        public Subject<string> CharacterLoadSubject { get; } = new Subject<string>();

        public Subject<string> SetAnimationSubject { get; } = new Subject<string>();

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
    }
}
