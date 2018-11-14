using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Dialogs;
using UnityEngine.UI;
using System.Collections;

public class DialogControl : MonoBehaviour
{
    #region Fields
    public Button[] m_buttons;
    private InfoBoard m_infoBoardOne;
    private InfoBoard m_infoBoardTwo;
    protected Text[] m_textButtons;
    private DialogManager m_manager;
    private bool m_clear;
    private Sprite[] m_sprites;
    #endregion //Fields

    #region Properties
    public bool IsBusy
    {
        get { return m_manager != null; }
    }
    #endregion //Properties

    #region Messages
    public void Awake()
    {
    }

    private void Start()
    {
        var searchResult = GetComponentsInChildren<InfoBoard>(true);
        m_infoBoardOne = searchResult[0];
        m_infoBoardTwo = searchResult[1];

        m_infoBoardOne.gameObject.SetActive(false);
        m_infoBoardTwo.gameObject.SetActive(false);

        m_textButtons = new Text[m_buttons.Length];
        for (int i = 0; i < m_buttons.Length; i++)
        {
            m_textButtons[i] = m_buttons[i].GetComponentInChildren<Text>();
            m_buttons[i].gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        if (m_manager != null)
        {
            m_manager.DialogueIsOver -= DialougeIsOver;
        }
    }
    #endregion //Messages

    #region Methods
    public void PromptClear()
    {
        m_clear = true;
    }

    public void EndDialog()
    {
        for (int i = 0; i < m_buttons.Length; i++)
        {
            m_buttons[i].gameObject.SetActive(false);
        }
        m_infoBoardOne.gameObject.SetActive(false);
        m_infoBoardTwo.gameObject.SetActive(false);
        m_manager = null;
    }

    public void OptionClicked(int index)
    {
        m_manager.Select(index);
        DisplayOptions();
        DisplayDialogs();
    }

    public void RegisterDialogManager(DialogManager dialogManager)
    {
        if (m_manager != null)
        {
            m_manager.DialogueIsOver -= DialougeIsOver;
            m_manager.Skipped -= DialogSkipped;
        }
        m_manager = dialogManager;
        m_manager.NewIteraction();
        m_manager.DialogueIsOver += DialougeIsOver;
        m_manager.Skipped += DialogSkipped;
        m_infoBoardOne.gameObject.SetActive(true);
        m_infoBoardTwo.gameObject.SetActive(true);
        DisplayOptions();
        DisplayDialogs();
    }

    private void DialogSkipped(object sender, EventArgs e)
    {
        DisplayOptions();
        DisplayDialogs();
    }

    private void DisplayOptions()
    {
        int i = m_buttons.Length - 1;
        foreach (IDialogueNode node in m_manager.CurrentNode)
        {
            m_buttons[i].gameObject.SetActive(true);
            Text text = m_textButtons[i];
            text.text = node.Title;
            i--;
            if (i < 0)
            {
                return;
            }
        }
        for (; i >= 0; i--)
        {
            m_buttons[i].gameObject.SetActive(false);
        }
    }

    private void DialougeIsOver(object sender, EventArgs e)
    {
        m_clear = true;
    }

    private void DisplayDialogs()
    {
        //m_infoBoardOne.Display(m_manager.CurrentNode.FirstMemberText);
        //m_infoBoardTwo.Display(m_manager.CurrentNode.SecondMemberText);
        StopAllCoroutines();

        StartCoroutine(
            TypeSentences(
                m_manager.CurrentNode.FirstMemberText,
                m_manager.CurrentNode.SecondMemberText
                )
        );

    }

    private IEnumerator TypeSentences(string sentence1, string sentence2)
    {
        m_infoBoardOne.Display("");
        m_infoBoardTwo.Display("");
        var board1 = m_infoBoardOne;
        var board2 = m_infoBoardTwo;

        if (m_manager.GetMainParticipant() == MainParitipant.Second)
        {
            //string temp = sentence2;
            //sentence2 = sentence1;
            //sentence1 = temp;
            var tempBoard = board2;
            board2 = board1;
            board1 = tempBoard;
        }
        board1.Title = m_manager.FirstParticipantName;
        board2.Title = m_manager.SecondParticipantName;
        board1.CharacterImage.sprite = LoadHeroImage(m_manager.FirstHeroImageName);
        board2.CharacterImage.sprite = LoadHeroImage(m_manager.SecondHeroImageName);
        char[] chars = sentence1.ToCharArray();

        for (int i = 0; i < chars.Length; i++)
        {
            board1.Text += chars[i];
            yield return null;
        }

        chars = sentence2.ToCharArray();
        for (int i = 0; i < chars.Length; i++)
        {
            board2.Text += chars[i];
            yield return null;
        }

        if (m_clear)
        {
            yield return new WaitForSeconds(2.5f);
            EndDialog();
            m_clear = false;
        }
    }

    private Sprite LoadHeroImage(string heroName)
    {
        if (m_sprites == null)
        {
            m_sprites = Resources.LoadAll<Sprite>("Characters/Heroes");
        }

        foreach(var sprite in m_sprites)
        {
            if (sprite.name == heroName)
            {
                return sprite;
            }
        }
        return null;
    }
    #endregion //Methods
}
