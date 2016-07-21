using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCharacter.Model.Service.TimeSignal.Impl
{
    /// <summary>
    /// 現在の時刻のみを表示するシンプルな時報
    /// </summary>
    class SimpleTimeSignalService : TimeSignalServiceAdapter
    {
        public override void OnMessage(DateTime time)
        {
            CharacterNotify.Instance.Talk(time.Hour + "時になったよ");
        }
    }
}
