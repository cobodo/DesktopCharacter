using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace DesktopCharacter.Util.Behavior
{
    class MouseWheelBeavior : Behavior<FrameworkElement>
    {
        /// <summary>
        /// マウスホイールした時に実行されるコマンド
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(MouseWheelBeavior), new UIPropertyMetadata(null));

        /// <summary>
        /// キーを押してる間に～を実現するためのプロバティ
        /// </summary>
        public Key BindKey
        {
            get { return (Key)GetValue(BindKeyProperty); }
            set { SetValue(BindKeyProperty, value); }
        }
        public static readonly DependencyProperty BindKeyProperty =
            DependencyProperty.Register("BindKey", typeof(Key), typeof(MouseWheelBeavior), new UIPropertyMetadata(null));

        /// <summary>
        /// バインドキーを設定するかどうか
        /// </summary>
        public bool IsBindKey
        {
            get { return (bool)GetValue(IsBindKeyProperty); }
            set { SetValue(IsBindKeyProperty, value); }
        }
        public static readonly DependencyProperty IsBindKeyProperty =
            DependencyProperty.Register("IsBindKey", typeof(bool), typeof(MouseWheelBeavior), new UIPropertyMetadata(null));

        /// <summary>
        /// キーの状態
        /// </summary>
        enum State
        {
            Down,
            Up,
        }
        private State _keyState = State.Up;

        protected override void OnAttached()
        {
             base.OnAttached();
            this.AssociatedObject.MouseWheel += OnMouseWheelHandler;
            this.AssociatedObject.KeyDown += OnKeyDownHandler;
            this.AssociatedObject.KeyUp += OnKeyUpHanlder;
           
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.MouseWheel -= OnMouseWheelHandler;
            this.AssociatedObject.KeyDown -= OnKeyDownHandler;
            this.AssociatedObject.KeyUp -= OnKeyUpHanlder;

        }

        /// <summary>
        /// マウスホイールが実行された時の処理。同時にキーの状態を確認して実行できるかどうかも確認している
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnMouseWheelHandler(object sender, MouseEventArgs args)
        {
            var control = sender as FrameworkElement;
            if (control == null)
            {
                return;
            }

            if( !IsBindKey || _keyState == State.Down)
            {
                var command = Command;
                if (command != null && command.CanExecute(sender))
                {
                    command.Execute(args);
                }

            }
        }

        /// <summary>
        /// BindKeyに設定されているものが押されてたらフラグをOnにする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnKeyDownHandler(object sender, KeyEventArgs args)
        {
            var control = sender as FrameworkElement;
            if (control == null)
            {
                return;
            }
            if (args != null && args.Key == BindKey)
            {
                _keyState = State.Down;
            }
        }

        /// <summary>
        /// BindKeyに設定されているものが離されていたらフラグをOffにする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnKeyUpHanlder(object sender, KeyEventArgs args)
        {
            var control = sender as FrameworkElement;
            if (control == null)
            {
                return;
            }
            if (args != null && args.Key == BindKey)
            {
                _keyState = State.Up;
            }
        }
    }
}
