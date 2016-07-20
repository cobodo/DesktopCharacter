using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace DesktopCharacter.Util.Behavior
{
    class MouseClickBeavior : Behavior<FrameworkElement>
    {
        //!< マウスの座標位置取得に使用する
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point lpPoint);

        /// <summary>
        /// マウスクリックした時に実行されるコマンド
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(MouseClickBeavior), new UIPropertyMetadata(null));

        /// <summary>
        /// ダブルクリックを有効にするかどうか
        /// </summary>
        public bool EnableDoubleClick
        {
            get { return (bool)GetValue(EnableDoubleClickProperty); }
            set { SetValue(EnableDoubleClickProperty, value); }
        }
        public static readonly DependencyProperty EnableDoubleClickProperty =
            DependencyProperty.Register("EnableDoubleClick", typeof(bool), typeof(MouseClickBeavior), new UIPropertyMetadata(null));

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.MouseLeftButtonDown += OnMouseClickHandler;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.MouseLeftButtonDown -= OnMouseClickHandler;
        }

        public void OnMouseClickHandler( object sender, MouseButtonEventArgs args )
        {
            var control = sender as FrameworkElement;
            if (control == null)
            {
                return;
            }

            //!< 座標位置取得
            Point pt = new Point();
            GetCursorPos(ref pt);
            //!< クリックした以外のイベントが欲しい場合、別途考える...
            //!< Tuple辺りで渡すのがいいか...
            //!< Single Click
            if (!EnableDoubleClick && args.ClickCount > 0)
            {
                if (Command != null && Command.CanExecute(sender))
                {
                    Command.Execute(pt);
                }
            }

            //!< Double Click
            if (EnableDoubleClick && args.ClickCount > 1)
            {
                if (Command != null && Command.CanExecute(sender))
                {
                    Command.Execute(pt);
                }
            }
        }
    }
}
