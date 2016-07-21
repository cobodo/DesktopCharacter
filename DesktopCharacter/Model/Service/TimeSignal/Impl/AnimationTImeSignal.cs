using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCharacter.Model.Service.TimeSignal.Impl
{
    class AnimationTImeSignal : TimeSignalServiceAdapter
    {
        public override void OnMessage(DateTime time)
        {
            CharacterNotify.Instance.SetAnimation("");
        }
    }
}
