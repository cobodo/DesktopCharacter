using DesktopCharacter.Util.Messenger.Message;
using DesktopCharacter.View;
using Livet.Behaviors.Messaging;
using Livet.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DesktopCharacter.Util.Messenger.Action
{
    class ResizeAction : InteractionMessageAction<Window>
    {
        protected override void InvokeAction(InteractionMessage message)
        {
            var msg = message as ReszieMessage;
            
            if (msg != null)
            {
                this.AssociatedObject.Width = msg.WindowSize.X;
                this.AssociatedObject.Height = msg.WindowSize.Y;
            }

            var character = this.AssociatedObject as Character;
            if ( character != null )
            {
                character.Image.Width = msg.WindowSize.X;
                character.Image.Height = msg.WindowSize.Y;
            }
        }
    }
}
