using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Live2DWrap;

namespace BabumiGraphics.Live2D
{
    public class AnimationContainer
    {
        private Dictionary<string, Live2DAnimation> AnimationDict = new Dictionary<string, Live2DAnimation>();
        private Random mRandom = new Random();

        public void Add( string name, Live2DAnimation animation)
        {
            AnimationDict.Add(name, animation);
        }

        public void Delete()
        {
            foreach ( var animation in AnimationDict )
            {
                animation.Value.deleteMotion();
            }
            AnimationDict.Clear();
        }

        /// <summary>
        /// アニメーションコンテナにアニメーション名が含まれていればその中のランダムなものを再生する
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Live2DAnimation GetAnimation( string name )
        {
            var animationSelect = AnimationDict.Where(e => e.Key.Contains(name)).ToList();
            return animationSelect[mRandom.Next(animationSelect.Count)].Value;
        }
    }
}
