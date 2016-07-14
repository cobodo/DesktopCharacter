using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DesktopCharacter.Util.Messenger.Message
{
    class TopmostMessage : Livet.Messaging.InteractionMessage
    {
        public TopmostMessage() : base()
        {
        }

        public TopmostMessage(string MessageKey) : base(MessageKey)
        {
        }

        public TopmostMessage(string MessageKey, bool topmost) : base(MessageKey)
        {
            this.Topmost = topmost;
        }

        protected override Freezable CreateInstanceCore()
        {
            return new TopmostMessage(MessageKey);
        }

        public bool Topmost
        {
            get { return (bool)GetValue(TopmostProperty); }
            set { SetValue(TopmostProperty, value); }
        }
        public static readonly DependencyProperty TopmostProperty = DependencyProperty.Register(nameof(Topmost), typeof(bool), typeof(TopmostMessage), new PropertyMetadata(null));

    }
}
