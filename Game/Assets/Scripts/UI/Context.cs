using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Context : MonoBehaviour {

    public Slider SliderSound;
    public Slider SliderMusic;
    public Text soundText;
    public Text musicText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetSound()
    {
        Global.SoundManager.soundVolume = SliderSound.value;
        Global.SoundManager.musicVolume = SliderMusic.value;
    }

    public void ChangeTextValues()
    {
        soundText.text = SliderSound.value.ToString();
        musicText.text = SliderMusic.value.ToString();
    }

    public void Exit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit ();
    #endif
    }
}
