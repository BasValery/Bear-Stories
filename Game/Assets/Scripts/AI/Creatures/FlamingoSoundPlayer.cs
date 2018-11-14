using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamingoSoundPlayer : MonoBehaviour {

    private AudioSource audioSource;
    public AudioClip cry;
    public AudioClip fall;

    public void playCry()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = cry;
        audioSource.Play();
    }
    public void playFall()
    {
        audioSource.clip = fall;
        audioSource.Play();

    }
}
