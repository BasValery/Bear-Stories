using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dialogs
{
    public class DialogueNode : IDialogueNode, IEnumerable<IDialogueNode>
    {
        #region Members
        private int m_id;
        private string m_title;
        private string m_firtsMemberText;
        private string m_secondMemberText;
        private Action m_action;
        private List<DialogueNode> m_children = new List<DialogueNode>(4);
        #endregion //Members

        #region Constructors
        public DialogueNode(int id, string title, string firstMemberText,
            string secondMemeberText, Action action = null)
        {
            Id = id;
            Title = title;
            FirstMemberText = firstMemberText;
            SecondMemberText = secondMemeberText;
            m_action = action;
        }
        #endregion //Conctructors

        #region Properties
        public int Id
        {
            get { return m_id; }
            private set { m_id = value; }
        }

        public string FirstMemberText
        {
            get { return m_firtsMemberText; }
            private set { m_firtsMemberText = value; }
        }

        public string SecondMemberText
        {
            get { return m_secondMemberText; }
            private set { m_secondMemberText = value; }
        }

        public string Title
        {
            get { return m_title; }
            private set { m_title = value; }
        }

        public int ChildrenCount
        {
            get { return m_children.Count; }
        }

        IDialogueNode IDialogueNode.this[int index]
        {
            get { return m_children[index]; }
        }

        public DialogueNode this[int index]
        {
            get { return m_children[index]; }
        }
        #endregion //Properties

        #region Messages

        #endregion //Messages

        #region Methods
        public void SetAction(Action action)
        {
            m_action = action;
        }

        public void AddChildren(DialogueNode node)
        {
            m_children.Add(node);
        }

        public bool RemoveChildren(DialogueNode node)
        {
            return m_children.Remove(node);
        }

        public int Index(IDialogueNode node)
        {
            return m_children.FindIndex(x => x == node);
        }

        public void Invoke()
        {
            if (m_action != null)
            {
                m_action.Invoke();
            }
        }

        public IEnumerator<IDialogueNode> GetEnumerator()
        {
            for (int i = 0; i < m_children.Count; i++)
            {
                yield return m_children[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion //Methods
    }
}