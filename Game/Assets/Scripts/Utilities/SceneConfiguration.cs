using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

class SceneConfiguration : MonoBehaviour
{
    #region Fields
    //public string[] m_timeAffectedScenes;

    private GameTime m_time;
    #endregion //Fields
    #region Messages
    private void Start()
    {
    }

    private void Awake()
    {
        m_time = Global.Time;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    #endregion //Messages

    #region Methods
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        //var scene = arg0;

        //if (m_timeAffectedScenes.Contains(scene.name))
        //{
        //    m_time.Freeze(false);
        //}
        //else
        //{
        //    m_time.Freeze(false);
        //}
    }
    #endregion //Methdos
}