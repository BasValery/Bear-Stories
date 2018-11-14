using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Language : MonoBehaviour {

    public GameObject optionsRus;
    public GameObject optionsEng;
    public GameObject menuRus;
    public GameObject menuEng;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetRus()
    {
        optionsRus.SetActive(true);
        optionsEng.SetActive(false);
        menuRus.SetActive(true);
        menuEng.SetActive(false);
    }
    public void SetEng()
    {
        optionsRus.SetActive(false);
        optionsEng.SetActive(true);
        menuRus.SetActive(false);
        menuEng.SetActive(true);
    }
}
