using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesktopCharacter.Util.Thread
{
    /// <summary>
    /// 1つのワーカースレッドで順に処理を行うExecutorです。
    /// </summary>
    class SingleThreadExecutor
    {
        private readonly BackgroundWorker _worker;
        private readonly TaskQueue _queue;

        /// <summary>
        /// 現在Queueに積まれているタスクの数を返します。
        /// </summary>
        public int TaskCount => _queue.Count;

        public SingleThreadExecutor()
        {
            _queue = new TaskQueue();
            _worker = new BackgroundWorker();
            _worker.DoWork += DoWork;
            _worker.RunWorkerAsync();
        }

        /// <summary>
        /// タスクをQueueに積みます
        /// </summary>
        /// <param name="action">バックグラウンドで順に実行したいタスク</param>
        public void PostAction(Action action)
        {
            _queue.Enqueue(action);
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                var action = _queue.Dequeue();
                action.Invoke();
            }
        }

        //SingleThreadExecutorのタスクを管理するQueue
        class TaskQueue
        {
            private readonly Queue<Action> _taskQueue = new Queue<Action>();
            public int Count {
                get
                {
                    lock (_taskQueue)
                    {
                        return _taskQueue.Count;
                    }
                }
            }

            public void Enqueue(Action action)
            {
                lock (_taskQueue)
                {
                    _taskQueue.Enqueue(action);
                    Monitor.PulseAll(_taskQueue);
                }
            }

            public Action Dequeue()
            {
                lock (_taskQueue)
                {
                    if (_taskQueue.Count == 0)
                    {
                        Monitor.Wait(_taskQueue);
                    }

                    return _taskQueue.Dequeue();
                }
            }
        }
    }
}
