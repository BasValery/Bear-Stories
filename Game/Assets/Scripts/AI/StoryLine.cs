using Dialogs;
using LitJson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

class StoryLine : MonoBehaviour
{
    #region Fields
    public BuyersController m_npcSystem;

    protected List<BuyerTask> m_tasks;
    protected int m_currentTask;
    protected bool f = true;
    #endregion //Fields

    #region Messages
    private void Awake()
    {
        LoadFromFile();
    }

    private float delay = 10;
    private float delayBuff = 4;
    private void Update()
    {
        //if (m_npcSystem != null && f == true)
        //{
        //    m_npcSystem.NewOrder(m_tasks[m_currentTask]);
        //    f = false;
        //    m_currentTask++;
        //}

        //if (m_npcSystem != null && Global.Time.TimeElapsed >= 4)
        //{
        //    m_npcSystem.NewOrder(m_tasks[m_currentTask]);
        //    m_currentTask++;
        //    m_npcSystem = null;
        //}
        /*
        if (delayBuff <= 0) {
            if (m_npcSystem != null && Global.Time.TimeOfDay == TimeOfDay.Day 
                && m_currentTask < m_tasks.Count
                && m_npcSystem.FreeQuanity > 0)
            {
                
                m_npcSystem.NewOrder(m_tasks[m_currentTask]);
                m_currentTask++;
            }
            else if (m_currentTask >= m_tasks.Count)
            {
                m_currentTask = 0;
              
            }
            delayBuff = delay;
        }
        else
        {
            delayBuff -= Time.deltaTime;
        }*/
        if (m_npcSystem != null && GnomeDialogManager.GnomeSpoke == true)
        {
            m_npcSystem.NewOrder(m_tasks[0]);
            GnomeDialogManager.GnomeSpoke = false;
        }
        else if(m_npcSystem != null && WitcherDialogManager.WhitcherSpoke == true)
        {
            m_npcSystem.NewOrder(m_tasks[1]);
            WitcherDialogManager.WhitcherSpoke = false;
        }
    }
    #endregion //Messages

    #region Methods
    public BuyerTask GetTask()
    {
        int task = m_currentTask % m_tasks.Count;
        m_currentTask++;
        return m_tasks[task];
    }

    private void LoadFromFile()
    {
        JsonData json = JsonMapper.ToObject(Resources.Load("Json/stories").ToString());
        m_tasks = new List<BuyerTask>(json.Count);
        for (int i = 0; i < json.Count; i++)
        {
            m_tasks.Add(BuyerTask.FromJson(json[i]));
        }
    }
    #endregion //Methods
}
