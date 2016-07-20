﻿using System;
using System.Threading;
using DesktopCharacter.Model.Locator;
using NLog;

namespace DesktopCharacter.Model.Service.TimeSignal
{
    /// <summary>
    /// 時報を提供するサービス
    /// </summary>
    interface ITimeSignalService : IDisposable, IInitializable
    {
        void OnMessage(DateTime time);
    }

    /// <summary>
    /// 時報を扱うサービスのアダプタ
    /// </summary>
    abstract class TimeSignalServiceAdapter : ITimeSignalService
    {

        public virtual void Dispose(){ }

        public virtual void Initialize(){ }

        public virtual void OnMessage(DateTime time){ }
    }

    /// <summary>
    /// 現在の時刻のみを表示するシンプルな時報
    /// </summary>
    class SimpleTimeSignalService : TimeSignalServiceAdapter
    {
        public override void OnMessage(DateTime time)
        {
            CharacterNotify.Instance.Talk(time.Hour + "時になったよ");
        }
    }

    /// <summary>
    /// 複数の時報を入れ子にして使うためのComposite
    /// </summary>
    class CompositeTimeSignalService : TimeSignalServiceAdapter
    {
        private Timer _timer;
        private readonly ITimeSignalService[] _timeSignalServices;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 引数で与えられた複数の時報サービスを1つにまとめて振る舞う
        /// </summary>
        /// <param name="services">同時に使用したいTimeSignalService</param>
        public CompositeTimeSignalService(params ITimeSignalService[] services)
        {
            _timeSignalServices = services;
        }

        public override void Initialize()
        {
            var hour = 3600000;//1時間
            var now = DateTime.Now.TimeOfDay.TotalMilliseconds;
            var nextHour = hour - (now % hour);
            _timer = new Timer(Callback, null,
                TimeSpan.FromMilliseconds(nextHour),
                TimeSpan.FromHours(1));
        }

        private void Callback(object state)
        {
            OnMessage(DateTime.Now);
        }

        public override void OnMessage(DateTime time)
        {
            logger.Info("OnMessage");
            foreach (var service in _timeSignalServices)
            {
                service.OnMessage(time);
            }
        }

        public override void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
