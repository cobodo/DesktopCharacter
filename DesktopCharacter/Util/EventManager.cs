using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCharacter.Util
{
    class EventManager
    {
        static List<Handler> mHandlers = new List<Handler>();

        public static void Add(String @event, Action<dynamic> hanlder)
        {
            if (mHandlers.Where(i => i.Event == @event).Count() > 0)
            {
                mHandlers
                    .Single(i => i.Event == @event)
                    .Handlers
                    .Add(hanlder);
            }
            else
            {
                mHandlers.Add(new Handler
                {
                    Event = @event,
                    Handlers = new List<Action<dynamic>>
                    {
                        hanlder
                    }
                });
            }
        }

        public static void Raise(String @event, dynamic parameters)
        {
            mHandlers
                .Single(h => h.Event == @event)
                .Handlers
                .ForEach(h =>
                {
                    h(parameters);
                });
        }
    }

    class Handler
    {
        public String Event { get; set; }
        public List<Action<dynamic>> Handlers { get; set; }
    }

}
