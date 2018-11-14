using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public enum SpecialGuideState
{
    ShowBrick, HideBrick
}

public class GuideManager : MonoBehaviour
{
    #region Field
    public Text m_nameText = null;
    public Text m_dialougeText = null;
    public Button m_next;
    public GameObject m_sensei = null;

    private Animator m_animator;
    private Animator m_bearAnimator;
    private Queue<Pair<string, Action>> m_sentences;
    #endregion //Fields

    #region Messages
    private void Start()
    {
        m_sentences = new Queue<Pair<string, Action>>();
        m_bearAnimator = m_sensei.GetComponent<Animator>();
        m_animator = GetComponent<Animator>();
        //m_sensei.SetActive(true);
    }

    private void Awake()
    {
        m_next.gameObject.SetActive(false);
        m_sensei.SetActive(false);
    }
    #endregion //Messages

    #region Methods
    public void StartDialouge(GuideDialogue dialogue)
    {
        m_animator.SetBool("IsOpen", true);
        m_sensei.SetActive(true);
        m_sentences.Clear();

        foreach (var sentence in dialogue.Sentences)
        {
            m_sentences.Enqueue(sentence);
        }
        m_nameText.text = dialogue.Name;
        m_next.gameObject.SetActive(true);
        DisplayNextSence();
    }

    public void StopDialogue()
    {
        EndDialogue();
    }

    public void DisplayNextSence()
    {
        if (m_sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        Pair<string, Action> sentence = m_sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void SpecialState(SpecialGuideState specialState)
    {
        switch (specialState)
        {
            case SpecialGuideState.ShowBrick:
                m_bearAnimator.SetTrigger("brickup");
                break;
            case SpecialGuideState.HideBrick:
                m_bearAnimator.SetTrigger("brickdown");
                break;
        }
    }

    IEnumerator TypeSentence(Pair<string, Action> sentence)
    {
        if (sentence.Item2 != null)
        {
            sentence.Item2.Invoke();
        }

        m_bearAnimator.SetBool("talk", true);
        m_dialougeText.text = "";
        
        /*
        foreach (char letter in sentence.ToCharArray())
        {
            m_dialougeText.text += letter;
            yield return null;
        }
        */

        char[] chars = sentence.Item1.ToCharArray();
        int miss = 2;
        for (int i = 0, j = 0 ; i < chars.Length; j++)
        {
            if (j % miss == 0)
            {
                yield return new WaitForSeconds(0.03f);
            }
            m_dialougeText.text += chars[i];
            i++;
            yield return null;
        }

        if (m_bearAnimator.GetBool("talk"))
        {
            m_bearAnimator.SetBool("talk", false);
        }
    }

    private void EndDialogue()
    {
        m_nameText.text = "";
        m_dialougeText.text = "";
        m_next.gameObject.SetActive(false);
        m_sensei.SetActive(false);
        m_animator.SetBool("IsOpen", false);
    }
    #endregion //Methods
}
