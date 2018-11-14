using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class SortingLayer : MonoBehaviour {

    #region Fields
    public string m_layerName = "Default";
    public int m_order;
    public bool m_applyOrder = true;

    private MeshRenderer m_renderer;
    #endregion //Fields

    #region Messages
    public void Awake () {
        m_renderer = GetComponent<MeshRenderer>();
        m_renderer.sortingLayerName = m_layerName;
        if (m_applyOrder)
        {
            m_renderer.sortingOrder = m_order;
        }
	}

    public void Update()
    {
        /*
        if (m_renderer.sortingLayerName != m_layerName)
        {
            m_renderer.sortingLayerName = m_layerName;
        }
        if (m_applyOrder && m_renderer.sortingOrder != m_order)
        {
            m_renderer.sortingOrder = m_order;
        }
        */
    }

    public void OnValidate()
    {
        var renderer = GetComponent<MeshRenderer>();
        renderer.sortingLayerName = m_layerName;
        if (m_applyOrder)
        {
            renderer.sortingOrder = m_order;
        }
    }
    #endregion //Messages
}
