using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
class MarkController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    #region Fields
    private Animator m_animator;
    private EventSystem m_eventSystem;
    #endregion //Fields

    #region Messages 
    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    private void Start()
    {
        m_eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }
    #endregion //Messages

    #region Methods
    public void OnPointerEnter(PointerEventData eventData)
    {
        m_animator.SetBool("show", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_animator.SetBool("show", false);
    }
    #endregion //Methods
}
