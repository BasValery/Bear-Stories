using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour {

    #region Fields
    public Text m_text;
    #endregion //Fields

    #region Methods
    public string Value
    {
        get { return m_text.text; }
        set
        {
            m_text.text = value;
        }
    }
    #endregion //Methods
}
