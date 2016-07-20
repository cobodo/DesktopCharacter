using BabumiGraphics.Graphics;
using NLog;
using SharpGL.SceneGraph;
using SharpGL.WPF;
using System;
using System.Windows;
using System.Windows.Input;

namespace DesktopCharacter.Util.Behavior
{
    class SharpGLBehavior 
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static readonly DependencyProperty OpenGLDrawCommandProperty =
            DependencyProperty.RegisterAttached("OpenGLDrawCommand", typeof(ICommand), typeof(SharpGLBehavior), new UIPropertyMetadata(null, OnOpenGLDrawCommandChanged));

        public static readonly DependencyProperty OpenGLInitializeCommandProperty =
            DependencyProperty.RegisterAttached("OpenGLInitializeCommand", typeof(ICommand), typeof(SharpGLBehavior), new UIPropertyMetadata(null, OnOpenGLInitializeChanged));

        public static void SetOpenGLDrawCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(OpenGLDrawCommandProperty, value);
        }

        public static void SetOpenGLInitializeCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(OpenGLInitializeCommandProperty, value);
        }

        private static void OnOpenGLDrawCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as OpenGLControl;
            if (control == null)
            {
                return;
            }

            if (e.NewValue is ICommand)
            {
                control.OpenGLDraw += OpenGLDrawEventHandler;
            }
            else
            {
                control.OpenGLDraw -= OpenGLDrawEventHandler;
            }
        }

        private static void OnOpenGLInitializeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as OpenGLControl;
            if (control == null)
            {
                return;
            }

            if (e.NewValue is ICommand)
            {
                control.OpenGLInitialized += OpenGLInitializeEventHandler;
            }
            else
            {
                control.OpenGLInitialized -= OpenGLInitializeEventHandler;
            }
        }

        public static void OpenGLDrawEventHandler(object sender, OpenGLEventArgs args)
        {
            var control = sender as OpenGLControl;
            if(control == null)
            {
                return;
            }

            Func<OpenGLControl, ICommand> GetCommand = (obj) => { return (ICommand)obj.GetValue(OpenGLDrawCommandProperty); };
            var command = GetCommand.Invoke(control);
            if (command.CanExecute(sender))
            {
                GraphicsManager.Instance.Device = args.OpenGL;
                GraphicsManager.Instance.Initialize();

                command.Execute(sender);
            }
        }

        public static void OpenGLInitializeEventHandler(object sender, OpenGLEventArgs args)
        {
            var control = sender as OpenGLControl;
            if (control == null)
            {
                return;
            }

            Func<OpenGLControl, ICommand> GetCommand = (obj) => { return (ICommand)obj.GetValue(OpenGLInitializeCommandProperty); };
            var command = GetCommand.Invoke(control);
            if (command.CanExecute(sender))
            {
                GraphicsManager.Instance.Device = args.OpenGL;
                GraphicsManager.Instance.Initialize();

                command.Execute(sender);
            }
        }
    }
}
