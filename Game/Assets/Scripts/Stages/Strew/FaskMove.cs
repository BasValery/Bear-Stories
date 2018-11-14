using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaskMove : MonoBehaviour {

    public bool starter = false;
    private int move = 5;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(starter)
        {
            this.transform.Translate(new Vector3(move * Time.deltaTime, 0, 0));
            if((transform.position.x >= 6.5 && move > 0)|| (transform.position.x <= -6.5 && move < 0))
            {
                move *= -1;
            }
        }
	}
}
