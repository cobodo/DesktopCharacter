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
        private Subject<string> talkEvent = new Subject<string>();

        /// <summary>
        /// Modelのインスタンス
        /// </summary>
        public readonly static CharacterTalkModel Instance = new CharacterTalkModel();

        private CharacterTalkModel()
        {

        }

        /// <summary>
        /// Characterに発言をさせる
        /// </summary>
        /// <param name="text">発言させる内容</param>
        public void Talk(string text)
        {
            talkEvent.OnNext(text);
        }

        /// <summary>
        /// 発言のイベントを通知するObservableを返します
        /// </summary>
        /// <returns>発言イベントを流すObservable</returns>
        public IObservable<string> TalkObservable()
        {
            //observableOnでUIスレッド指定出来るようにすれば、シングルスレッドで実行されるのが保証されるのでQueueをブロッキングにする必要がなくなる
            return talkEvent;
        }
    }
}
