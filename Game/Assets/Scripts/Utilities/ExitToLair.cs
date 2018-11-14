using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitToLair : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Escape))
        {
            Global.Hud.GuideManager.StopDialogue();
            Global.Hud.m_arrowcontroller.Hide();
            Global.Time.Freeze(false);
           Global.Load.LoadScene("LairInside");
        }
	}
}
