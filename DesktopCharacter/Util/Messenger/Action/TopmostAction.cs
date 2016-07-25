using Livet.Behaviors.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Livet.Messaging;
using DesktopCharacter.Util.Messenger.Message;

namespace DesktopCharacter.Util.Messenger.Action
{
    class TopmostAction : InteractionMessageAction<Window>
    {
        protected override void InvokeAction(InteractionMessage message)
        {
            var msg = message as TopmostMessage;

            if(msg != null)
            {
                this.AssociatedObject.Topmost = msg.Topmost;
            }
        }
    }
}
