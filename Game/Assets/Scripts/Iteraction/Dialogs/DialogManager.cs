using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Dialogs
{
    public enum MainParitipant
    {
        First, Second
    }

    public abstract class DialogManager
    {
        #region Fields
        private Dictionary<int, Action> m_actions;
        private DialogueNode m_currentNode;
        private DialogueNode m_defaultNode = null;
        private DialogueNode[] m_allNodes;
        private string m_firstParticipantName;
        private string m_secondPariticpantName;
        private string m_firstHeroImageName;
        private string m_secondHeroImageName;
        private MainParitipant m_mainParticipant = MainParitipant.First;

        #endregion //Fields

        #region Constructors
        public DialogManager()
        {
            m_actions = ActionFactory();
            m_allNodes = DialogueLoader.FromJson(Path, m_actions);
        }
        #endregion //Constructors

        #region Events
        public event EventHandler Skipped;
        public event EventHandler DialogueIsOver;
        #endregion //Events

        #region Properties
        public string FirstParticipantName
        {
            get { return m_firstParticipantName; }
            set { m_firstParticipantName = value; }
        }

        public string SecondParticipantName
        {
            get { return m_secondPariticpantName; }
            set { m_secondPariticpantName = value; }
        }

        public string FirstHeroImageName
        {
            get { return m_firstHeroImageName; }
            set { m_firstHeroImageName = value; }
        }

        public string SecondHeroImageName
        {
            get { return m_secondHeroImageName; }
            set { m_secondHeroImageName = value; }
        }
        #endregion //Properties

        #region Properties
        public abstract string Path { get; }

        public IDialogueNode CurrentNode
        {
            get { return m_currentNode; }
        }

        #endregion //Properties

        #region Methods
        public abstract Dictionary<int, Action> ActionFactory();

        public virtual void NewIteraction()
        {
            if (m_defaultNode == null)
            {
                m_currentNode = m_allNodes.FirstOrDefault();
                if (m_currentNode != null)
                {
                    m_currentNode.Invoke();
                }
            }
            else
            {
                m_currentNode = m_defaultNode;
            }
        }

        public virtual MainParitipant GetMainParticipant()
        {
            return MainParitipant.First;
        }
        
        //Set active child node of current Node
        public bool Select(IDialogueNode dialogNode)
        {
            int index = m_currentNode.Index(dialogNode);
            if (index != 1)
            {
                return Select(m_currentNode[index]);
            }
            return false;
        }

        //Set active child node of current Node
        public bool Select(int index)
        {
            if (index < 0 || index > m_currentNode.ChildrenCount - 1)
            {
                return false;
            }
            m_currentNode = m_currentNode[index];
            m_currentNode.Invoke();
            if (m_currentNode.ChildrenCount == 0)
            {
                if (DialogueIsOver != null)
                {
                    DialogueIsOver.Invoke(this, new EventArgs());
                }
            }
            return true;
        }

        //move to another active node
        public bool Skip(int id)
        {
            DialogueNode node = m_allNodes.Where(i => i.Id == id).FirstOrDefault();

            if (node != null)
            {
                m_defaultNode = node;
                m_currentNode = node;
                m_defaultNode.Invoke();
                if (Skipped != null)
                {
                    Skipped.Invoke(this, new EventArgs());
                }
                if (m_currentNode.ChildrenCount == 0)
                {
                    if (DialogueIsOver != null)
                    {
                        DialogueIsOver.Invoke(this, new EventArgs());
                    }
                }
                return true;
            }
            return false;

        }

        protected bool Select(DialogueNode node)
        {
            if (node == null)
            {
                return false;
            }

            m_currentNode = node;
            m_currentNode.Invoke();
            if (m_currentNode.ChildrenCount == 0)
            {
                if (DialogueIsOver != null)
                {
                    DialogueIsOver.Invoke(this, new EventArgs());
                }
            }
            return true;
        }

        protected DialogueNode GetNodeById(int id)
        {
            return m_allNodes.FirstOrDefault(x => x.Id == id);
        }
        #endregion //Methods
    }
}