using Anima2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class OrderLayerByY : MonoBehaviour
{
    #region Fields
    public int m_initialOffset = 0;
    public float m_unitSize = 100;
    private int[] m_intialSortingLayer;
    //private int[] m_initialTextSoringLayer;
    private BoxCollider2D m_collider;
    private SpriteMeshInstance[] m_renderers;
    //private MeshRenderer[] m_textMeshes;
    #endregion //Fields

    #region Messages
    private void Awake()
    {
        m_renderers = GetComponentsInChildren<SpriteMeshInstance>(true);
        m_intialSortingLayer = new int[m_renderers.Length];
        m_collider = GetComponent<BoxCollider2D>();
        //m_textMeshes = GetComponentsInChildren<MeshRenderer>(true);
        //m_initialTextSoringLayer = new int[m_textMeshes.Length];
        
        for (int i = 0; i < m_intialSortingLayer.Length; i++)
        {
            m_intialSortingLayer[i] = m_renderers[i].sortingOrder;
            
        }
        
        /*
        for (int i = 0; i < m_initialTextSoringLayer.Length; i++)
        {
            m_initialTextSoringLayer[i] = m_textMeshes[i].sortingOrder;
        }
        */
    }

    private void Update()
    {
        Recalculate();
    }
    #endregion //Messages

    #region Methods
    public void SetLayer(string layerName)
    {
        foreach (var sprite in m_renderers)
        {
            sprite.sortingLayerName = layerName;
        }
    }

    public void Recalculate()
    {
        var y = GetY();

        for (int i = 0; i < m_renderers.Length; i++)
        {
            SpriteMeshInstance rend = m_renderers[i];
            rend.sortingOrder = m_initialOffset +
                m_intialSortingLayer[i] +
                y * -1;
        }
        /*
        for (int i = 0; i < m_textMeshes.Length; i++)
        {
            var mesh = m_textMeshes[i];
            mesh.sortingOrder = m_initialOffset +
                m_initialTextSoringLayer[i] +
                Mathf.RoundToInt(mesh.transform.position.y * m_unitSize) * -1;
        }
        */
    }

    private int GetY()
    {
        int result;
        if (m_collider == null)
        {
            result = Mathf.RoundToInt(transform.transform.position.y * m_unitSize);
        }
        else
        {
            var bounds = m_collider.bounds;
            result = Mathf.RoundToInt(bounds.min.y * m_unitSize);
        }
        return result;
    }
    #endregion //Methods
}
