using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace DesktopCharacter.Util.Behavior
{
    class MouseDrawMoveBehavior : Behavior<Window>
    {
        /// <summary>
        /// ドラッグフラグ
        /// </summary>
        private bool _dragFlag;
        /// <summary>
        /// オフセット
        /// </summary>
        private Point _offset;
        /// <summary>
        /// 移動し終わったあとに実行されるコマンド
        /// </summary>
        public ICommand PostCommand
        {
            get { return (ICommand)GetValue(PostCommandProperty); }
            set { SetValue(PostCommandProperty, value); }
        }
        public static readonly DependencyProperty PostCommandProperty =
            DependencyProperty.RegisterAttached("PostCommand", typeof(ICommand), typeof(MouseDrawMoveBehavior), new UIPropertyMetadata(null));

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.MouseLeftButtonDown += OnMouseClickDownHandler;
            this.AssociatedObject.MouseLeftButtonUp += OnMouseClickOnHandler;
            this.AssociatedObject.MouseMove += OnMouseMoveHandler;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.MouseLeftButtonDown -= OnMouseClickDownHandler;
            this.AssociatedObject.MouseLeftButtonUp -= OnMouseClickOnHandler;
            this.AssociatedObject.MouseMove -= OnMouseMoveHandler;
        }

        public void OnMouseClickDownHandler(object sender, MouseEventArgs args)
        {
            var control = sender as Window;
            if (control == null)
            {
                return;
            }

            _dragFlag = true;
            _offset = args.GetPosition(control);
        }

        public void OnMouseClickOnHandler(object sender, MouseEventArgs args)
        {
            var control = sender as Window;
            if (control == null)
            {
                return;
            }
            _dragFlag = false;

            if (PostCommand != null && PostCommand.CanExecute(sender))
            {
                PostCommand.Execute( new Math.Point( control.Left, control.Top ) );
            }
        }

        public void OnMouseMoveHandler(object sender, MouseEventArgs args)
        {
            var control = sender as Window;
            if (control == null)
            {
                return;
            }
            if (_dragFlag)
            {
                Point p = args.GetPosition(control);
                control.Left += p.X - _offset.X;
                control.Top += p.Y - _offset.Y;
            }
        }
    }
}
