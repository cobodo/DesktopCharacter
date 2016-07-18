using Codeplex.Data;
using Live2DWrap;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabumiGraphics.Utility;
using System.Runtime.InteropServices;

namespace BabumiGraphics.Live2D
{
    public class Manager
    {
        private Live2DMotionQueueManager mAnimationQueue = null;

        private AnimationContainer mAnimationContainer = new AnimationContainer();
        private Model mModel = new Model();

        private List<Tuple<string, bool>> mRegisterAnimationQueue = new List<Tuple<string, bool>>();

        public Manager()
        {
            Live2DWrapping.init();
        }

        public void Load( string modeljson )
        {
            mAnimationQueue = new Live2DMotionQueueManager();

            JsonObject modelObject = new JsonObject();
            JsonObject physicsObject = null;

            string loadPath = System.IO.Path.GetDirectoryName(modeljson);
            //!< ModelJsonの読み込み
            modelObject.LoadJson( modeljson );
            
            {
                var data = modelObject.LoadObject;
                //!< モデル読み込み
                {
                    mModel.LoadModel(loadPath, data.model, data.textures);
                }
                //!< アニメーション読み込み
                {
                    foreach ( var motion in (dynamic[])data.motions )
                    {
                        //System.Console.WriteLine(t.Keys);
                        foreach (var file in (dynamic[])motion.Value)
                        {
                            //!< 2byte文字をLive2D側が読み込めない様子...
                            using (var fs = new FileStream(Path.Combine(loadPath, file.file), FileMode.Open))
                            {
                                byte[] readBuffer = new byte[fs.Length];
                                fs.Read(readBuffer, 0, (int)fs.Length);
                                IntPtr dest = Marshal.AllocHGlobal((int)fs.Length);
                                Marshal.Copy(readBuffer, 0, dest, (int)fs.Length);
                                var animation = new Live2DAnimation();
                                animation.loadMotion(dest, (int)fs.Length);
                                mAnimationContainer.Add(Path.GetFileNameWithoutExtension(Path.Combine(loadPath, file.file)), animation);
                                Marshal.FreeHGlobal(dest);
                            }
                        }
                    }
                }
                //<! 物理ファイルを読み込む
                if (data.IsDefined("physics"))
                {
                    physicsObject = new JsonObject();
                    physicsObject.LoadJson(loadPath + "\\" + data.physics);
                }
            }

            if(physicsObject != null)
            {
                //!< PhysicsJson読み込み
                var data = physicsObject.LoadObject;
            }
        }

        public void Update( int Width, int Height )
        {
            bool animationFinish = mAnimationQueue.isFinished();

            //!< 登録されたアニメーションがなければアイドルアニメーションへ
            if (IsEmpty())
            {
                if (animationFinish)
                {
                    mAnimationQueue.startMotion(mAnimationContainer.GetAnimation("idle"), false);
                }
            }
            else
            {
                var animation = Front();
                //!< 即座に切り替える
                if (animation.Item2)
                {
                    mAnimationQueue.startMotion(mAnimationContainer.GetAnimation(animation.Item1), false);
                    //!< 切り替えたので削除する
                    Remove();
                }
                else
                {
                    //!< アニメーションが終了してから切り替える
                    if (animationFinish)
                    {
                        mAnimationQueue.startMotion(mAnimationContainer.GetAnimation(animation.Item1), false);
                        //!< 切り替えたので削除する
                        Remove();
                    }
                }
            }
            mAnimationQueue.updateParam(mModel.ModelObject);
            mModel.Update( Width, Height );
        }

        /// <summary>
        /// モデル解放
        /// </summary>
        public void DeleteModel()
        {
            if (mModel.IsLoadComplete)
            {
                mModel.Delete();
            }
            mAnimationQueue?.deleteMotionManager();
            mAnimationContainer?.Delete();
        }

        /// <summary>
        /// アニメーションセット
        /// </summary>
        /// <param name="name">アニメーション名</param>
        /// <param name="finish">即座に反映orアニメーションが終わったあとに反映のどちらか</param>
        public void SetAnimation(string name, bool finish)
        {
            mRegisterAnimationQueue.Add(new Tuple<string, bool>(name, finish));
        }

        private bool IsEmpty()
        {
            return mRegisterAnimationQueue.Count == 0;
        }

        private Tuple<string, bool> Front()
        {
            return mRegisterAnimationQueue[0];
        }

        private void Remove()
        {
            mRegisterAnimationQueue.RemoveAt(0);
        }
    }
}
