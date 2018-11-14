using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMuter : MonoBehaviour {

    private float music;
    private AudioSource audioSource;
    private void Awake()
    {
       
    }

    void Start () {

        music = Global.SoundManager.musicVolume;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = music / 100f;

    }
	
	// Update is called once per frame
	void Update () {
        if (music != Global.SoundManager.musicVolume)
        {
            music = Global.SoundManager.musicVolume;
            if (audioSource != null)
            {
                audioSource.volume = music / 100f;
            }
        }
    }
}
