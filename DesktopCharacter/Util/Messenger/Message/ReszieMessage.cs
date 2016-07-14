using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DesktopCharacter.Util.Messenger.Message
{
    class ReszieMessage : Livet.Messaging.InteractionMessage
    {
        public ReszieMessage() : base()
        {
        }

        public ReszieMessage(string MessageKey) : base(MessageKey)
        {
        }

        public ReszieMessage(string MessageKey, Point windowSize) : base(MessageKey)
        {
            this.WindowSize = windowSize;
        }

        protected override Freezable CreateInstanceCore()
        {
            return new ReszieMessage(MessageKey);
        }

        public Point WindowSize
        {
            get { return (Point)GetValue(WindowSizeProperty); }
            set { SetValue(WindowSizeProperty, value); }
        }
        public static readonly DependencyProperty WindowSizeProperty = DependencyProperty.Register(nameof(WindowSize), typeof(Point), typeof(ReszieMessage), new PropertyMetadata(null));

    }
}
