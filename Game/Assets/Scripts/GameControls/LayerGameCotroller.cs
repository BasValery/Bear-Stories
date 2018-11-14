using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerGameCotroller : MonoBehaviour {

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
            Global.Load.LoadScene("Game");
        }
	}
    
    public void LoadKnife()
    {
        Global.Load.LoadScene("MiniGameKnife");
    }

    public void LoadBoiling()
    {
        Global.Load.LoadScene("Boiling");
    }
}
