﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptySceneControl : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Global.Hud.hudDisable();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit ();
        #endif
        }
    }
}
