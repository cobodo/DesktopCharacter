using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCharacter.Model.AI.Node
{
    abstract class ActionNode : Node
    {
        public virtual bool Update()
        {
            return true;
        }
    }
}
