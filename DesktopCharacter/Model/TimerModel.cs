using System;
using System.Reactive.Subjects;
using System.Windows.Threading;

namespace DesktopCharacter.Model
{
    class TimerModel
    {
        private int timerCountAtSecond;
        private int time;
        private int Time
        {
            get { return time; }
            set
            {
                time = Math.Max(value, 0);
                timeSubject.OnNext(time);
            }
        }
        private TimerState state;
        private TimerState State
        {
            get { return state; }
            set
            {
                state = value;
                timerStateSubject.OnNext(state);
            }
        }

        private Subject<TimerState> timerStateSubject = new Subject<TimerState>();
        private Subject<int> timeSubject = new Subject<int>();

        private DispatcherTimer timer;

        public TimerModel(int timerCountAtSecond)
        {
            state = TimerState.INITIALIZED;
            this.timerCountAtSecond = timerCountAtSecond;

            timer = new DispatcherTimer(DispatcherPriority.Normal);
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += OnTimerUpdate;
            //TimerStateの状態によってDispatcherTimerの状態を遷移させる
            timerStateSubject.Subscribe((TimerState state) => {
                if (State == TimerState.RUNNING && !timer.IsEnabled)
                {
                    timer.Start();
                }
                if(State != TimerState.RUNNING && timer.IsEnabled)
                {
                    timer.Stop();
                }
            });
        }

        /// <summary>
        /// 現在の時間
        /// </summary>
        /// <returns></returns>
        public IObservable<int> TimeObservable()
        {
            return timeSubject;
        }

        /// <summary>
        /// タイマーの状態
        /// </summary>
        /// <returns></returns>
        public IObservable<TimerState> StateObservable()
        {
            return timerStateSubject;
        }

        /// <summary>
        /// タイマーをリセットして開始
        /// </summary>
        public void Start()
        {
            Reset();
            Resume();
        }

        /// <summary>
        /// タイマーをリセット
        /// </summary>
        public void Reset()
        {
            //タイマーを停止
            State = TimerState.INITIALIZED;
            Time = timerCountAtSecond;
        }

        /// <summary>
        /// タイマーを一時停止
        /// </summary>
        public void Pause()
        {
            //タイマーを一時停止
            State = TimerState.PAUSE;
        }

        /// <summary>
        /// タイマーを再開
        /// </summary>
        public void Resume()
        {
            //タイマーを再開
            State = TimerState.RUNNING;
        }

        private void OnTimerUpdate(object sender, EventArgs e)
        {
            if (State == TimerState.RUNNING)
            {
                Time--;
                if(Time == 0)
                {
                    State = TimerState.FINISHED;
                }
            }
        }
    }

    enum TimerState
    {
        /// <summary>
        /// 初期化済
        /// </summary>
        INITIALIZED,
        /// <summary>
        /// 実行中
        /// </summary>
        RUNNING,
        /// <summary>
        /// 一時停止中
        /// </summary>
        PAUSE,
        /// <summary>
        /// 完了
        /// </summary>
        FINISHED
    }
}
