using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dialogs;

namespace Dialogs
{
    class BunnyDialogManager : DialogManager
    {
        public override string Path
        {
            get
            {
                return "Bunny/BunnyBase.json";
            }
        }

        public override Dictionary<int, Action> ActionFactory()
        {
            var result = new Dictionary<int, Action>();
            return result;
        }
    }
}
