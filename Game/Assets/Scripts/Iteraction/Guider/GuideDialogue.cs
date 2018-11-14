using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class GuideDialogue
{
    #region Fields
    private string m_name;
    private List<Pair<string, Action>> m_sentences = new List<Pair<string, Action>>();
    #endregion //Fields

    #region Constructors
    public GuideDialogue(string name)
    {
        m_name = name;
    }

    public GuideDialogue()
    {
        m_name = "Сенсей";
    }
    #endregion Constructors

    #region Properties
    public IEnumerable<Pair<string, Action>> Sentences
    {
        get { return m_sentences; }
    }
    #endregion //Properties

    #region Methods
    public void AddSentence(string sentence, Action action = null)
    {
        m_sentences.Add(Pair<string, Action>.Create(sentence, action));
    }

    public string Name
    {
        get { return m_name; }
    }
    #endregion //Methods
}
