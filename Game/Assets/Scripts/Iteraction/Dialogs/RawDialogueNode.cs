using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dialogs
{
    class RawDialogueNode
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FirstMemberText { get; set; }
        public string SecondMemberText { get; set; }
        public int[] Children { get; set; }
    }
}