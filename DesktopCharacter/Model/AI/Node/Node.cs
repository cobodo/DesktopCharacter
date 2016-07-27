using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCharacter.Model.AI.Node
{
    interface Node
    {
        /// <summary>
        /// 実行する
        /// </summary>
        /// <returns>true:成功 false:失敗</returns>
        bool Update();
    }
}
