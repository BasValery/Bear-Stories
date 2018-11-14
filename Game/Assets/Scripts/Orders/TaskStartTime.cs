using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskStartTime : MonoBehaviour {

    public double StartTime { get; set; }
    public double TaskDuration { get; set; }
    public double FinishTime
    {
        get
        {
            return StartTime + TaskDuration;
        }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
