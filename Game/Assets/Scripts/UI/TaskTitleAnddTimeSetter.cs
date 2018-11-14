using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskTitleAnddTimeSetter : MonoBehaviour {

    public Text title;
    public Text time;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetTitleAndTask(string title, string time)
    {
        this.time.text = time;
        this.title.text = title;
    }
}
