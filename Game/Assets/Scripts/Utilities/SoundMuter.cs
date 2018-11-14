using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMuter : MonoBehaviour {

    // Use this for initialization
    private float sound = 50;
    private AudioSource audioSource;
    private void Awake()
    {
        sound = Global.SoundManager.soundVolume;
    }

    void Start () {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = sound / 100f;
    }
	
	// Update is called once per frame
	void Update () {
		if(sound != Global.SoundManager.soundVolume)
        {
            sound = Global.SoundManager.soundVolume;
            if (audioSource != null)
            {
                audioSource.volume = sound / 100f;
            }
        }
	}
}
