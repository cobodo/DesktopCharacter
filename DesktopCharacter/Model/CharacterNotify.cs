using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using DesktopCharacter.Util.Math;
using DesktopCharacter.Model.Repository;
using DesktopCharacter.Model.Locator;

namespace DesktopCharacter.Model
{
    class CharacterNotify
    {
        public Subject<string> TalkSubject { get; } = new Subject<string>();

        public Subject<string> CharacterLoadSubject { get; } = new Subject<string>();

        public Subject<string> SetAnimationSubject { get; } = new Subject<string>();

        public Subject<bool> TopMostMessageSubject { get; } = new Subject<bool>();

        public Subject<Point> WindowSizeMessageSubject { get; } = new Subject<Point>();

        /// <summary>
        /// CharacterPropertyNotifyのインスタンス
        /// </summary>
        public static CharacterNotify Instance { get; } = new CharacterNotify();

        /// <summary>
        /// コンフィグファイルのリポジトリ
        /// </summary>
        private BabumiConfigRepository _babumiConfigRepository;

        private CharacterNotify()
        {
            _babumiConfigRepository = ServiceLocator.Instance.GetInstance<BabumiConfigRepository>();
        }

        /// <summary>
        /// ロードするキャラクター名
        /// </summary>
        /// <param name="name">キャラクター名</param>
        public void CharacterLoad(string name)
        {
            CharacterLoadSubject.OnNext(name);
        }

        /// <summary>
        /// ロードするキャラクター名
        /// </summary>
        /// <param name="name">キャラクター名</param>
        public void SetAnimation(string name)
        {
            SetAnimationSubject.OnNext(name);
        }

        /// <summary>
        /// Characterに発言をさせる
        /// </summary>
        /// <param name="text">発言させる内容</param>
        public void Talk(string text)
        {
            TalkSubject.OnNext(text);
        }

        /// <summary>
        /// ウィンドウを常に上にするかどうかの切り替えメッセージを送る
        /// </summary>
        /// <param name="topmost">最前面フラグ</param>
        public void TopMostMessage( bool topmost)
        {
            TopMostMessageSubject.OnNext(topmost);
        }

        /// <summary>
        /// ウィンドウサイズを変更する
        /// </summary>
        /// <param name="windowSize"></param>
        public void WindowResizeMessage(int zoomLevel)
        {
            const double VIEW_ANGLE = System.Math.PI / 4;
            var setting = _babumiConfigRepository.GetConfig();
            var screenSize = new Point();
            double x = 0.1 / (2.0 * 1.0 * System.Math.Tan(VIEW_ANGLE / 2));
            double d = 1 / (1 - (x * System.Math.Abs(zoomLevel)) * System.Math.Tan(VIEW_ANGLE / 2) * 2);
            if( d == 0.0)
            {
                //!< 異常値なので終了させる
                WindowSizeMessageSubject.OnError(new Exception("Scale計算で異常値を検出しました"));
            }
            if (zoomLevel >= 0)
            {
                screenSize = setting.WindowSizeOrigin * new Point(d);
            }
            else
            {
                screenSize = setting.WindowSizeOrigin * new Point(1 / d);
            }
            //!< 10の倍数に補正（描画がおかしくなるので）
            //!< dが0.0でなければここで0割りが発生することはないはずなのでエラー処理はない
            screenSize = new Point { X = (screenSize.X + 10 - (screenSize.X % 10)), Y = (screenSize.Y + 10 - (screenSize.Y % 10)) };
            WindowSizeMessageSubject.OnNext(screenSize);
        }
    }
}
