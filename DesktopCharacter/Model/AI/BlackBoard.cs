using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCharacter.Model.AI
{
    public class BlackBoard
    {
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
        /// タッチ専用記憶ボード
        /// </summary>
        public class TouchActionBoard
        {
            /// <summary>
            /// 気持ちが変るカウント数
            /// </summary>
            public int EMOTION_SWITCH_PARAM = 3;
            /// <summary>
            /// 前回押されてからこのタイム内ならPushCountアップする 逆にそうでなければPushCountを初期化する
            /// </summary>
            public int TIMEMILLI_INTERVAL = 3000;
            /// <summary>
            /// クリック位置
            /// </summary>
            public enum Part
            {
                UpperBody,
                LowerBody,
                None,
            }
            /// <summary>
            /// クリックされたかどうか
            /// </summary>
            public bool IsClickAction;
            /// <summary>
            /// クリック位置
            /// </summary>
            public Util.Math.Point MousePoint;
            /// <summary>
            /// 分割サイズ
            /// </summary>
            public double SplitSize;
            /// <summary>
            /// タッチ位置
            /// </summary>
            public Part PartType;
            /// <summary>
            /// 前回押された時のタイム
            /// </summary>
            public double LastDateTime;
            /// <summary>
            /// タッチされた場所のカウントを記録する
            /// </summary>
            public Dictionary<Part, int> Parameter { get; set; } = new Dictionary<Part, int>()
            {
                { Part.LowerBody, 0 },
                { Part.UpperBody, 0 },
            };
        }


        public TouchActionBoard TouchAction { get; set; } = new TouchActionBoard();
    }
}
