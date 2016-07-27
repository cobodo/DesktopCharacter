using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using NLog;
using DesktopCharacter.Model.Locator;
using DesktopCharacter.Model.Repository;
using DesktopCharacter.Model.Database.Domain;
using System.IO;
using DesktopCharacter.Model;
using DesktopCharacter.Model.Service.Slack;
using Microsoft.Win32;

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

            SetupRegistory();

            base.OnStartup(e);
            ServiceLocator.Instance.InitializeServiceLocator();

            //!< プロジェクトのコンフィグファイルを読み込む
            var repo = ServiceLocator.Instance.GetInstance<BabumiConfigRepository>();
            try
            {
                repo.Save("Babumi.config");
            }
            catch (Exception exception) when (exception is FileNotFoundException)
            {
                MessageBox.Show(DesktopCharacter.Properties.Resources.App_ConfigCreateNotify);
                //!< Configファイルを自動作成してみてアプリケーションを再起動を試みる
                try
                {
                    var config = BabumiConfig.DefaultConfig();
                    if (config != null)
                    {
                        repo.Save(config);
                        repo.ExportXML("Babumi.config");
                        System.Windows.Forms.Application.Restart();
                    }
                    else
                    {
                        MessageBox.Show(DesktopCharacter.Properties.Resources.App_Live2DModelNotFound);
                    }
                    //例外が発生したら必ず終了する
                    Environment.Exit(0);
                }
                catch(Exception ex) when (ex is DirectoryNotFoundException)
                {
                    MessageBox.Show(DesktopCharacter.Properties.Resources.App_Live2DFolderNotFound);
                    Environment.Exit(0);
                }
            }
            catch(Exception exception)
            {
                //!< 致命的エラーなので終了する
                MessageBox.Show(string.Format("[ERROR]\n{0}", exception.Message));
                //例外が発生したら必ず終了する
                Environment.Exit(0);
            }
        }

        protected override void OnExit(System.Windows.ExitEventArgs e)
        {
            //!< プロジェクトのコンフィグファイルを読み込む
            var repo = ServiceLocator.Instance.GetInstance<BabumiConfigRepository>();
            repo.ExportXML("Babumi.config"); 
        }

        //WebBrowserコンポーネントでIEの新しいバージョンが使えるようにレジストリをいじる
        private void SetupRegistory()
        {
            var name = Process.GetCurrentProcess().ProcessName + ".exe";
            var IEVAlue = 10001;
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION",
                name, IEVAlue, RegistryValueKind.DWord);
        }
    }
}
