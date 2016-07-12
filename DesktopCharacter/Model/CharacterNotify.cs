using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCharacter.Model
{
    class CharacterPropertyNotify
    {
        public Subject<string> CharacterLoadSubject { get; } = new Subject<string>();

        public Subject<string> SetAnimationSubject { get; } = new Subject<string>();

        /// <summary>
        /// CharacterPropertyNotifyのインスタンス
        /// </summary>
        public static CharacterPropertyNotify Instance { get; } = new CharacterPropertyNotify();

        private CharacterPropertyNotify()
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
    }
}
