using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet;
using Livet.Commands;
using Livet.Messaging.Windows;
using Reactive.Bindings;
using System.Windows.Input;
using System.Reactive.Linq;
using Livet.Messaging;
using System.Collections.ObjectModel;

namespace DesktopCharacter
{
    class ModelView : Livet.ViewModel
    {
        public ModelView()
        {
            Util.WindowInstance.MainInstnace = this;
        }
    }
}
