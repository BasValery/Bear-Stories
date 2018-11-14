using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUpdeter : MonoBehaviour {
    public Slider sliderSound;
    public Slider sliderMusic;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Set()
    {
        sliderMusic.value = Global.SoundManager.musicVolume;
        sliderSound.value = Global.SoundManager.soundVolume;

    }
}
