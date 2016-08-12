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
    class DragAndDropBehavior : Behavior<FrameworkElement>
    {
        /// <summary>
        /// 移動し終わったあとに実行されるコマンド
        /// </summary>
        public ICommand PostCommand
        {
            get { return (ICommand)GetValue(PostCommandProperty); }
            set { SetValue(PostCommandProperty, value); }
        }
        public static readonly DependencyProperty PostCommandProperty =
            DependencyProperty.RegisterAttached("PostCommand", typeof(ICommand), typeof(DragAndDropBehavior), new UIPropertyMetadata(null));

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.Drop += OnDropHandler;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.Drop -= OnDropHandler;
        }

        public void OnDropHandler(object sender, DragEventArgs args)
        {
            var control = sender as FrameworkElement;
            if (control == null)
            {
                return;
            }

            string[] files = args.Data.GetData(DataFormats.FileDrop) as string[];

            if (PostCommand != null && PostCommand.CanExecute(sender))
            {
                PostCommand.Execute(files);
            }
        }
    }
}
