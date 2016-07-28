using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCharacter.Model.AI.Node
{
    abstract class SelectorNode : Node
    {
        public bool Update()
        {
            foreach( var node in Child)
            {
                var ret = node.Update();
                if (ret)
                {
                    return true;
                }
            }
            return false;
        }

        public ObservableCollection<Node> Child { get; set; } = new ObservableCollection<Node>();
    }
}
