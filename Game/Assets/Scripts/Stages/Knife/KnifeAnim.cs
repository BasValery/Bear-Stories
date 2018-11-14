using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KnifeAnim : MonoBehaviour {

    public bool Done { get; private set; }
    // Use this for initialization
    public float Motion;
    public UnityEvent onCuted = new UnityEvent();
    public GameObject cutLine;
    private GameObject pump;
    //   private bool goUp = false;
    private float hightPosition;
    private float lowPosition = -3f;
    private bool hited = false;
    void Start () {
        Done = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (!Done)
            Animate();
	}
    private void Animate()
    {
        transform.Translate(0, Motion * UnityEngine.Time.deltaTime, 0);
        if (transform.position.y <= lowPosition && Motion < 0)
        {
            
            Motion *= -1;
            pump.GetComponent<ShowScript>().Show();
            var newCutLine = Instantiate(cutLine);
            newCutLine.transform.position = new Vector3(transform.position.x - 1, lowPosition + 0.3f, 0);
            if(hited)
            {
                onCuted.Invoke();
            }
        }
        else
        if (transform.position.y >= hightPosition && Motion > 0)
        {
            Motion *= -1;
           
            Done = true;
        }

    }

    public void startCut(bool hited)
    {
        pump = transform.GetChild(0).gameObject;
        Done = false;
        this.hited = hited;
        hightPosition = transform.position.y;
    }
}
