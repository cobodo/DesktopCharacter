using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet;
using Livet.Commands;
using Livet.Messaging.Windows;
using Reactive.Bindings;
using System.Windows.Input;
using System.Reactive.Linq;
using Livet.Messaging;
using System.Collections.ObjectModel;
using DesktopCharacter.Model.Locator;

namespace DesktopCharacter.ViewModel
{
    class CharacterViewModel : Livet.ViewModel
    {
        /// <summary>
        /// TalkVMのインスタンス
        /// </summary>
        public TalkViewModel mTalkViewModel { private get; set; }

        public CharacterViewModel()
        {
        }

        /// <summary>
        /// 終了時に呼ばれるイベント
        /// </summary>
        public void ClosedEvent()
        {
        }
    }
}
