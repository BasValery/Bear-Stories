using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class InfoCloudControl : MonoBehaviour
{
    #region Fields
    public GameObject m_cloud;
    public GameObject m_arrow;
    public SmartTextMesh m_textMesh;
    public bool m_show;
    public float m_showDistance = 3f;
    private GameObject m_player;
    #endregion //Fields

    #region Messages
    private void Awake()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        bool active = (m_show && Math.Abs(m_player.transform.position.x - transform.position.x) <= m_showDistance);
        m_cloud.SetActive(active);
        m_arrow.SetActive(active);
    }
    #endregion //Messages

    #region Methods
    public void Show(bool show)
    {
        m_show = show;
    }

    public void DisplayText(string text)
    {
        m_textMesh.UnwrappedText = text;
        m_textMesh.NeedsLayout = true;
    }
    #endregion //Methods
}
