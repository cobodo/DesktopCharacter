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
using System.IO;
using DesktopCharacter.Model.Service.Template;
using static DesktopCharacter.Model.Service.Template.TemplateVariables;

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

            InitializeTemplate();

            //!< プロジェクトのコンフィグファイルを読み込む
            var repo = ServiceLocator.Instance.GetInstance<BabumiConfigRepository>();
            try
            {
                repo.Save("Babumi.config");
            }
            catch (Exception exception) when (exception is FileNotFoundException)
            {
                MessageBox.Show(string.Format("コンフィグファイルがないようなので自動作成します。 \n"));
                //!< Configファイルを自動作成してみてアプリケーションを再起動を試みる
                var config = BabumiConfig.DefaultConfig();
                if ( config != null )
                {
                    repo.Save(config);
                    repo.ExportXML("Babumi.config");
                    System.Windows.Forms.Application.Restart();
                }
                else
                {
                    MessageBox.Show(string.Format("Res/Live2D以下のフォルダにモデルデータがないためコンフィグファイルの作成に失敗しました\n"));
                }
                //例外が発生したら必ず終了する
                Environment.Exit(0);
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

        public void InitializeTemplate()
        {
            var template = ServiceLocator.Instance.GetInstance<ITemplateService>();

            //Twitter
            template.RegisterTemplate(BABUMI_NAMESPACE, TWITTER_FAVORITE,
                "@Model.Event.Source.Name にふぁぼられました");
            template.RegisterTemplate(BABUMI_NAMESPACE, TWITTER_UNFAVORITE,
                "@Model.Event.Source.Name にあんふぁぼされました");
            template.RegisterTemplate(BABUMI_NAMESPACE, TWITTER_FOLLOW,
                "@Model.Event.Source.Name にフォローされました");
            template.RegisterTemplate(BABUMI_NAMESPACE, TWITTER_UNFOLLOW,
                "@Model.Event.Source.Name にフォロー解除されました");
            template.RegisterTemplate(BABUMI_NAMESPACE, TWITTER_BLOCK,
                "@Model.Event.Source.Name にブロックされました");
            template.RegisterTemplate(BABUMI_NAMESPACE, TWITTER_UNBLOCK,
                "@Model.Event.Source.Name にブロック解除されました");
            template.RegisterTemplate(BABUMI_NAMESPACE, TWITTER_MEMBER_ADDED,
                "@Model.Event.Source.Name にリスト追加されました");
            template.RegisterTemplate(BABUMI_NAMESPACE, TWITTER_MEMBER_REMOVED,
                "@Model.Event.Source.Name にリストから外されました");
            template.RegisterTemplate(BABUMI_NAMESPACE, TWITTER_RT,
                "@Model.Event.Status.User.Name にRTされました");
            template.RegisterTemplate(BABUMI_NAMESPACE, TWITTER_MENTION,
                "@Model.Event.Status.User.Name からメンション");
            template.RegisterTemplate(BABUMI_NAMESPACE, TWITTER_DM,
                "@Model.Event.DirectMessage.Sender.Name からDM");

            //TimeSignal
            template.RegisterTemplate(BABUMI_NAMESPACE, TIME_SIGNAL,
                "@Model.Hour 時になったよ");
        }
    }
}
