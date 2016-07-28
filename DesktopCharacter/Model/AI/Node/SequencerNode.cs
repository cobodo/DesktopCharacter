using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCharacter.Model.AI.Node
{
    class SequencerNode : Node
    {
        public bool Update()
        {
            foreach (var node in Child)
            {
                var ret = node.Update();
                if (!ret)
                {
                    return false;
                }
            }
            return true;
        }
        public ObservableCollection<Node> Child { get; set; } = new ObservableCollection<Node>();
    }
}
