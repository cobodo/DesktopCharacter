using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows;
using NLog;
using DesktopCharacter.Model.Locator;

namespace DesktopCharacter
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        protected override void OnStartup(StartupEventArgs e)
        {
            Thread.GetDomain().UnhandledException += (sender, args) =>
            {
                var exception = args.ExceptionObject as Exception;
                if (exception != null)
                {
                    logger.Error(exception);
                }
            };
            base.OnStartup(e);
            ServiceLocator.Instance.InitializeServiceLocator();
        }
    }
}
