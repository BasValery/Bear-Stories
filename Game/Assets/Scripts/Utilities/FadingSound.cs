using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingSound : MonoBehaviour
{

    // Use this for initialization
    private float sound = 50;
    private AudioSource audioSource;
    private GameObject m_player;

    public float startPlayingDistance;
    [Range(0, 1)]
    public float maxVolume = 1f;
    private void Awake()
    {
        sound = Global.SoundManager.soundVolume;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = sound / 100f;
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        sound = Global.SoundManager.soundVolume;
        if (audioSource != null)
        {
            float soundFading;
            /*
            soundFading = Mathf.Abs(m_player.transform.position.x - transform.position.x) - maxSound;
            */
            soundFading = Vector2.Distance(m_player.transform.position, transform.position) - maxVolume;
            if (soundFading <= 0)
            {
                soundFading = 1;
            }
            else if (soundFading > startPlayingDistance)
            {
                soundFading = 0;
            }
            else
            {
                soundFading = 1 - soundFading / startPlayingDistance;
            }
            audioSource.volume = (sound / 100f) * soundFading;
        }


    }
}
