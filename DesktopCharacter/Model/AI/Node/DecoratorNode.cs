using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCharacter.Model.AI.Node
{
    abstract class DecoratorNode : Node
    {
        public virtual bool Update()
        {
            if (Result)
            {
                if (Child != null)
                {
                    return Child.Update();
                }
            }
            return false;
        }

        public bool Result { get; set; }
        public Node Child { get; set; }
    }
}
