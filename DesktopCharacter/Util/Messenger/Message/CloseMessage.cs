using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DesktopCharacter.Util.Messenger.Message
{
    /// <summary>
    /// メッセージボックスを出してアプリケーションを終了するカスタムメッセージ
    /// </summary>
    class CloseMessage : Livet.Messaging.InteractionMessage
    {
        public bool Conditonal
        {
            get { return (bool)GetValue(ConditonalProperty); }
            set { SetValue(ConditonalProperty, value); }
        }
        public static readonly DependencyProperty ConditonalProperty
            = DependencyProperty.Register(nameof(Conditonal), typeof(bool), typeof(CloseMessage), new System.Windows.PropertyMetadata(null));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty
            = DependencyProperty.Register(nameof(Text), typeof(string), typeof(CloseMessage), new System.Windows.PropertyMetadata(null));

        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }
        public static readonly DependencyProperty CaptionProperty
            = DependencyProperty.Register(nameof(Caption), typeof(string), typeof(CloseMessage), new System.Windows.PropertyMetadata(null));

        public MessageBoxImage Image
        {
            get { return (MessageBoxImage)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }
        public static readonly DependencyProperty ImageProperty
            = DependencyProperty.Register(nameof(Image), typeof(MessageBoxImage), typeof(CloseMessage), new System.Windows.PropertyMetadata(null));


        public CloseMessage() : base()
        {
        }

        public CloseMessage(string MessageKey) : base(MessageKey)
        {
        }

        public CloseMessage(bool conditonal, string message, string caption, MessageBoxImage image, string MessageKey) : base(MessageKey)
        {
            Conditonal = conditonal;
            Text = message;
            Image = image;
            Caption = caption;
        }

        public CloseMessage(bool conditonal, string message, string MessageKey) : base(MessageKey)
        {
            Conditonal = conditonal;
            Text = message;
            Image = MessageBoxImage.None;
            Caption = "";
        }

        protected override Freezable CreateInstanceCore()
        {
            return new CloseMessage(MessageKey);
        }
    }
}
