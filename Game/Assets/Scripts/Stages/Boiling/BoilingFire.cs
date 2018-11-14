using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoilingFire : MonoBehaviour {

    public GameControl gameController;
    public GameObject particals;
    private AudioSource Audio;
    private ParticleSystem particalSystem;
    private Animator[] animators;
    // Use this for initialization
    void Start () {
        Audio = GetComponent<AudioSource>();
        particalSystem = particals.GetComponent<ParticleSystem>();
        animators = GetComponentsInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        var emission = particalSystem.emission;
        float animationSpeed = 1;

        if (gameController.Heating < 1)
        {
            particals.SetActive(false);
            Audio.Stop();
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            particals.SetActive(true);

            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(true);

            if (!Audio.isPlaying)
            {
                Audio.Play();
            }

            animationSpeed *= gameController.Heating / 360f + 1f;

            if (gameController.Heating > 0 && gameController.Heating <= 90)
            {
                emission.rateOverTime = 5;
            }
            else if (gameController.Heating > 90 && gameController.Heating <= 270)
            {
                emission.rateOverTime = 7.5f;
            }
            else if (gameController.Heating > 270)
            {
                emission.rateOverTime = 10;
            }

            foreach(var animator in animators)
            {
                animator.speed = animationSpeed;
            }
                /*
                if (gameController.Heating < 1)
                {
                    particals.SetActive(false);
                    Audio.Stop();
                    transform.GetChild(0).gameObject.SetActive(false);
                    transform.GetChild(1).gameObject.SetActive(false);
                    transform.GetChild(2).gameObject.SetActive(false);

                }
                else
                {
                    particals.SetActive(true);
                    var particalEmission = particals.GetComponent<ParticleSystem>().emission;
                    if (!Audio.isPlaying)
                        Audio.Play();

                    if (gameController.Heating > 0 && gameController.Heating <= 90)
                    {
                        particalEmission.rateOverTime = 5;
                        transform.GetChild(0).gameObject.SetActive(true);
                        transform.GetChild(1).gameObject.SetActive(false);
                        transform.GetChild(2).gameObject.SetActive(false);
                    }
                    else if (gameController.Heating > 90 && gameController.Heating <= 270)
                    {
                        particalEmission.rateOverTime = 10;
                        transform.GetChild(0).gameObject.SetActive(false);
                        transform.GetChild(1).gameObject.SetActive(true);
                        transform.GetChild(2).gameObject.SetActive(false);
                    }
                    else if (gameController.Heating > 270)
                    {
                        particalEmission.rateOverTime = 15;
                        transform.GetChild(0).gameObject.SetActive(false);
                        transform.GetChild(1).gameObject.SetActive(false);
                        transform.GetChild(2).gameObject.SetActive(true);
                    }
                    */
                }
            }

   
}
