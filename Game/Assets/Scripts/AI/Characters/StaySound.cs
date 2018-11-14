using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaySound : MonoBehaviour {

    private AudioSource audio;
    private Animator animator;
    public AudioClip clip;
    public NPCSTate requestedState;


    // Use this for initialization
    void Start()
    {
        audio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audio.enabled)
            return;
        if (animator.GetInteger("state") == (int)requestedState)
        {
            if (!audio.isPlaying)
            {
                if (audio.clip != clip)
                {
                    audio.clip = clip;
                }
                audio.Play();
            }
        }
        else
        {
            audio.Stop();
        }
    }
}
