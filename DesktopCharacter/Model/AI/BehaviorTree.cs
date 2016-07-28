using DesktopCharacter.Model.Locator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCharacter.Model.AI
{
    class ClickDecorator : Node.DecoratorNode
    {
        public override bool Update()
        {
            var board = ServiceLocator.Instance.GetInstance<BlackBoard>();

            Result = board.TouchAction.IsClickAction;
            return base.Update();
        }
    }

    class ClickPosition : Node.DecoratorNode
    {
        public override bool Update()
        {
            var board = ServiceLocator.Instance.GetInstance<BlackBoard>();

            if (board.TouchAction.SplitSize > board.TouchAction.MousePoint.Y)
            {
                board.TouchAction.PartType = BlackBoard.TouchActionBoard.Part.UpperBody;
            }
            else if (board.TouchAction.SplitSize < board.TouchAction.MousePoint.Y
                && board.TouchAction.SplitSize * 2 > board.TouchAction.MousePoint.Y)
            {
                board.TouchAction.PartType = BlackBoard.TouchActionBoard.Part.LowerBody;
            }
            //!< タッチ場所の記録だけなので無条件で成功
            Result = true;
            return base.Update();
        }
    }

    class CharacterAnimation : Node.ActionNode
    {
        public override bool Update()
        {
            var board = ServiceLocator.Instance.GetInstance<BlackBoard>();

            //!< どこをタッチされたか
            var part = board.TouchAction.PartType;
            //!< パラメーターマップ
            var parameterMap = board.TouchAction.Parameter;
            //!< 前回おされた時刻
            var lastTime = board.TouchAction.LastDateTime;
            //!< タッチカウント
            var tochCount = parameterMap.Where(e => e.Key == part).FirstOrDefault().Value;

            if (DateTime.Now.TimeOfDay.TotalMilliseconds - lastTime < board.TouchAction.TIMEMILLI_INTERVAL)
            {
                tochCount += 1;
            }
            else
            {
                tochCount = 0;
            }

            //!< 現在の気持ちを計算
            BlackBoard.Type type = BlackBoard.Type.None;
            switch (part)
            {
                case BlackBoard.TouchActionBoard.Part.UpperBody:
                    {
                        type = tochCount < board.TouchAction.EMOTION_SWITCH_PARAM ? BlackBoard.Type.Happy : BlackBoard.Type.Shy;
                    }
                    break;
                case BlackBoard.TouchActionBoard.Part.LowerBody:
                    {
                        type =  tochCount < board.TouchAction.EMOTION_SWITCH_PARAM ? BlackBoard.Type.Shy : BlackBoard.Type.Anger;
                    }
                    break;
            }

            //!< アクション実行！
            CharacterNotify.Instance.SetAnimation(type.ToString());
            switch (type)
            {
                case BlackBoard.Type.Anger:
                    CharacterNotify.Instance.Talk("激おこ");
                    break;
                case BlackBoard.Type.Happy:
                    CharacterNotify.Instance.Talk("嬉しい！");
                    break;
                case BlackBoard.Type.Shy:
                    CharacterNotify.Instance.Talk("恥ずかしいぃよ...");
                    break;
            }

            //!< タッチされた現在時刻を記録する
            board.TouchAction.LastDateTime = DateTime.Now.TimeOfDay.TotalMilliseconds;
            //!< タッチされたカウントを記録する
            board.TouchAction.Parameter[part] = tochCount;
            //!< パラメーターをリセットする
            board.TouchAction.IsClickAction = false;
            board.TouchAction.PartType = BlackBoard.TouchActionBoard.Part.None;
            //!< かならずアクションは実行されるので無条件成功
            return true;
        }
    }


    class BehaviorTree
    {
        public BehaviorTree()
        {
            var rootNode = new Node.SequencerNode();
            var clickNode = new ClickDecorator();
            var clickPos = new ClickPosition();
            var action = new CharacterAnimation();
            clickNode.Child = clickPos;
            clickPos.Child = action;
            rootNode.Child.Add(clickNode);
            RootNode = rootNode;
        }

        public void Update()
        {
            if( RootNode != null)
            {
                RootNode.Update();
            }
        }
        
        public Node.Node RootNode { get; set; }
    }
}
