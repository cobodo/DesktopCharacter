using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCharacter.Model.AI
{
    class BehaviorTree
    {
        public BehaviorTree()
        {

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
