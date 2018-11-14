using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpraySpreading : MonoBehaviour {

    public List<GameObject> sprayList = new List<GameObject>();
    
    private int index = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowSpray()
    {
        if(index < sprayList.Count)
        {
            sprayList[index].SetActive(true);
            ++index;
        }
    }
}
