using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class CanvasSorintgLayer : MonoBehaviour
{
    #region Fields
    public string m_layerName = "Default";
    public int m_order = 0;

    private Canvas m_canvas;
    #endregion //Fields

    #region Messages
    private void Awake()
    {
        m_canvas = GetComponent<Canvas>();
        m_canvas.sortingLayerName = m_layerName;
        m_canvas.sortingOrder = m_order;
    }

    public void Update()
    {
        if (m_canvas.sortingLayerName != m_layerName)
        {
            m_canvas.sortingLayerName = m_layerName;
        }
        if (m_canvas.sortingOrder != m_order)
        {
            m_canvas.sortingOrder = m_order;
        }
    }

    public void OnValidate()
    {
        var canvas = GetComponent<Canvas>();
        canvas.sortingLayerName = m_layerName;
        canvas.sortingOrder = m_order;
    }
    #endregion //Message
}
