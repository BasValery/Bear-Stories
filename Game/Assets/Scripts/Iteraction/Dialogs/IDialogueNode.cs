using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dialogs
{
    public interface IDialogueNode : IEnumerable<IDialogueNode>
    {
        int Id { get; }
        string Title { get; }
        string FirstMemberText { get; }
        string SecondMemberText { get; }
        int ChildrenCount { get; }
        IDialogueNode this[int index] { get; }
    }
}
