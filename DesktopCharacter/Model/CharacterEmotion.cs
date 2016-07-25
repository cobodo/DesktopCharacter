using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCharacter.Model
{
    class CharacterEmotion
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        const int EMOTION_SWITCH_PARAM = 3;     //!< 気持ちが変るカウント数
        const int TIMEMILLI_INTERVAL = 3000;    //!< 前回押されてからこのタイム内ならPushCountアップする 逆にそうでなければPushCountを初期化する

        /// <summary>
        /// キャラクターの気持ち
        /// </summary>
        public enum Type
        {
            Happy,  //!< 嬉しい
            Anger,  //!< 怒る
            Shy,    //!< 恥ずかしい
            None,
        }

        /// <summary>
        /// クリック位置
        /// </summary>
        public enum Part
        {
            UpperBody,
            LowerBody,
            None,
        }

        public struct Prameter
        {
            public double LastDateTime; //!< 前回押された時のタイム
            public int PushCount;  //!< クリックされた回数 一定回数押されると気持ちが変化する
        }

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<Part, Prameter> _parameterMap = new Dictionary<Part, Prameter>();

        /// <summary>
        /// キャラクターにアクションさせる
        /// </summary>
        /// <param name="screenSize"></param>
        /// <param name="mousePoint"></param>
        public Type GetEmotion( Util.Math.Point screenSize, Util.Math.Point mousePoint )
        {
            //!< 2分割
            var split = screenSize.Y / 2.0;
            Part part = Part.None;
            if(split > mousePoint.Y)
            {
                part = Part.UpperBody;
            }
            if(split < mousePoint.Y && split*2 > mousePoint.Y)
            {
                part = Part.LowerBody;
            }
            return GetEmotion(part);
        }

        private Type GetEmotion(Part type)
        {
            {
                var obj = _parameterMap.Where(e => e.Key == type);
                if (!obj.Any())
                {
                    _parameterMap.Add(type, new Prameter { LastDateTime = DateTime.Now.TimeOfDay.TotalMilliseconds, PushCount = 1 });
                }
                else
                {
                    var parameter = obj.FirstOrDefault().Value;
                    if (DateTime.Now.TimeOfDay.TotalMilliseconds - parameter.LastDateTime < TIMEMILLI_INTERVAL)
                    {
                        parameter.PushCount += 1;
                    }
                    else
                    {
                        parameter.PushCount = 0;
                    }
                    parameter.LastDateTime = DateTime.Now.TimeOfDay.TotalMilliseconds;
                    _parameterMap[type] = parameter;
                }
            }
            {
                var obj = _parameterMap.Where(e => e.Key == type).FirstOrDefault().Value;
                switch (type)
                {
                    case Part.UpperBody:
                        return obj.PushCount < EMOTION_SWITCH_PARAM ? Type.Happy : Type.Shy;
                    case Part.LowerBody:
                        return obj.PushCount < EMOTION_SWITCH_PARAM ? Type.Shy : Type.Anger;
                }
            }
            return Type.None;
        }
    }
}
