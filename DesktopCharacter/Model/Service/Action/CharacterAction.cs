using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCharacter.Model.Service.Action
{
    class CharacterAction
    {
        //!< 顔 = 嬉しい => 恥ずかしい
        //!< 胸 = 恥ずかしい => 怒る
        //!< 足 = 恥ずかしい => 怒る
        public enum Type
        {
            Happy,  //!< 嬉しい
            Anger,  //!< 怒る
            Sorrow, //!< 悲しい
            Enjoy,  //!< 楽しい
            None,
        }

        public enum Part
        {
            Head,
            Breast,
            Leg,
            None,
        }

        const int CHANGE_AI = 5;    //!< 気持ちが変るカウント数
        const int TIME = 60;        //!< 前回押されてからこのタイム内ならPushCountアップする 逆にそうでなければPushCountを初期化する

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
        public Type Action( Util.Math.Point screenSize, Util.Math.Point mousePoint )
        {
            //!< 3分割
            var split = screenSize.Y / 3.0;
            Part part = Part.None;
            if(split > mousePoint.Y)
            {
                part = Part.Head;
            }
            if(split < mousePoint.Y && split*2 > mousePoint.Y)
            {
                part = Part.Breast;
            }
            if (split * 2 < mousePoint.Y && split * 3 > mousePoint.Y)
            {
                part = Part.Leg;
            }
            Update(part);
            return GetAI(part, _parameterMap[part].PushCount);
        }

        private void Update(Part type)
        {
            var obj = _parameterMap.Where(e => e.Key == type);
            if( obj.Any() )
            {
                _parameterMap.Add(type, new Prameter { LastDateTime = DateTime.Now.TimeOfDay.TotalMilliseconds, PushCount = 1 });
            }
            else
            {
                var parameter = obj.FirstOrDefault().Value;
                if (DateTime.Now.TimeOfDay.TotalMilliseconds - parameter.LastDateTime < TIME)
                {
                    parameter.PushCount += 1;
                }
                parameter.LastDateTime = DateTime.Now.TimeOfDay.TotalMilliseconds;
            }
        }

        private Type GetAI(Part type, int pushCount )
        {
            switch(type)
            {
                case Part.Breast:
                    return pushCount < CHANGE_AI ? Type.Happy : Type.Enjoy;
                case Part.Head:
                    return pushCount < CHANGE_AI ? Type.Happy : Type.Enjoy;
                case Part.Leg:
                    return pushCount < CHANGE_AI ? Type.Happy : Type.Enjoy;
            }
            return Type.None;
        }
    }
}
