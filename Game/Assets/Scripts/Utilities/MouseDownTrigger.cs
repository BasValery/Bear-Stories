using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
class MouseDownTrigger : MonoBehaviour
{
    #region Fields
    public UnityEvent m_mouseDown;
    #endregion //Fields

    #region Messages
    private void OnMouseDown()
    {
        m_mouseDown.Invoke();
    }
    #endregion //Messages
}
