using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoBoard : MonoBehaviour
{
    #region Fields
    public TextMeshProUGUI m_text;
    public TextMeshProUGUI m_title;
    public Image m_characterImage;
    #endregion //Fields

    #region Properties
    public string Text
    {
        get { return m_text.text; }
        set { m_text.text = value; }
    }

    public string Title
    {
        get { return m_title.text; }
        set { m_title.text = value;}
    }

    public Image CharacterImage
    {
        get { return m_characterImage; }
        set { m_characterImage = value; }
    }
    #endregion //Proerties

    #region Messages
    private void OnDisable()
    {
    }

    private void OnEnable()
    {
    }
    #endregion //Messages


    #region Methods
    public void Display(string text)
    {
        m_text.text = text;
    }
    #endregion //Methods
}
