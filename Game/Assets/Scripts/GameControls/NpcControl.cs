using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class NpcControl : MonoBehaviour
{
    #region Fields
    Buyer[] m_npcCollection;
    #endregion //Fields

    #region Messages
    private void Awake()
    {
        m_npcCollection = GetComponentsInChildren<Buyer>();
    }
    #endregion //Messages

    #region Methods
    #endregion //Methods
}
