using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOut : MonoBehaviour {

    // Use this for initialization

    private bool down;
    private bool up;
    private float distaceRange = 0;
    private bool lastUp = true;
    public float Distace = 10;
    public float speed = 10;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    
        if(distaceRange  > 0)
        {
            if (down)
                transform.Translate(0, -speed * Time.deltaTime, 0);
            if(up)
                transform.Translate(0, speed * Time.deltaTime, 0);
            distaceRange -= speed * Time.deltaTime;
        }
	}

    public void MoveDown()
    {
        if (distaceRange > 0)
            return;
        down = true;
        up = false;
        distaceRange = Distace;
    }

    public void MoveUp()
    {
        if (distaceRange > 0)
            return;
        up = true;
        down = false;
        distaceRange = Distace;
    }

    public void MoveIt()
    {
        if(lastUp)
        {
            lastUp = false;
            MoveDown();
        }
        else
        {
            lastUp = true;
            MoveUp();
        }
    }
}
