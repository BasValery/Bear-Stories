using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour {

    private bool hide = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(hide)
        {
            this.gameObject.SetActive(false);
              
        }
     
	}

    public void HideIt()
    {
        hide = true;
    }

    public void ChangeState()
    {
        if (isActiveAndEnabled)
            gameObject.SetActive(false);
        else
        {
            gameObject.SetActive(true);
        }
    }
}
