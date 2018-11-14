using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowScript : MonoBehaviour {


    public float visible = 0.2f;
    private float timer;
    private Vector3 startPos;
    // Use this for initialization
    void Start () {
        gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		if(this.enabled)
        {
            timer -= Time.deltaTime;
            if(timer  <= 0)
            {
                this.gameObject.SetActive(false);
            }
        }
	}

    public void Show()
    {
        gameObject.SetActive(true);
        timer = visible;
    }
}
