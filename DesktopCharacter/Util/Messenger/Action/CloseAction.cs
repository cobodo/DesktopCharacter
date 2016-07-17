using DesktopCharacter.Util.Messenger.Message;
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
    class CloseAction : InteractionMessageAction<Window>
    {
        protected override void InvokeAction(InteractionMessage message)
        {
            var msg = message as CloseMessage;

            if( msg.Conditonal )
            {
                MessageBox.Show( msg.Text, msg.Caption, MessageBoxButton.OK, msg.Image );

                this.AssociatedObject.Close();
            }
        }
    }
}
