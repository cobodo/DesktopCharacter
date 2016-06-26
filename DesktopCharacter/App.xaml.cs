using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows;
using NLog;
using DesktopCharacter.Model.Locator;
using DesktopCharacter.Model.Repository;
using DesktopCharacter.Model.Database.Domain;

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

            //!< プロジェクトのコンフィグファイルを読み込む
            var repo = ServiceLocator.Instance.GetInstance<BabumiConfigRepository>();
            try
            {
                repo.Save("Babumi.config");
            }
            catch (Exception exception)
            {
                MessageBox.Show( string.Format( "[ERROR]\n{0}", exception.Message ) );
                Environment.Exit(0);
            }
        }

        protected override void OnExit(System.Windows.ExitEventArgs e)
        {
            //!< プロジェクトのコンフィグファイルを読み込む
            var repo = ServiceLocator.Instance.GetInstance<BabumiConfigRepository>();
            repo.ExportXML("Babumi.config"); 
        }
    }
}
