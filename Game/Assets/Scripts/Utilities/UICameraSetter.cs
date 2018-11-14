using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

class UICameraSetter : MonoBehaviour
{
    #region Messages
    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    #endregion //Messages

    #region Methods
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        var canvas = Global.Hud.gameObject.GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
    }
    #endregion' //Methods
}
