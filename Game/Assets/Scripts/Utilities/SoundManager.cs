using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public float musicVolume { get; set; }
    public float soundVolume { get; set; }
	// Use this for initialization
	void Start () {
        musicVolume = 50;
        soundVolume = 50;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
