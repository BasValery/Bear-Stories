using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalCounter : MonoBehaviour {

    public int Count { get
        {
            return count;
        }
    }
    private int count = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Hit()
    {
        ++count;
    }
}
