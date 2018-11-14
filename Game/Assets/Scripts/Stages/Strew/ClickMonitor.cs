using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickMonitor : MonoBehaviour {

    public GameObject particle;
    //public DataBase dataBase;

    private bool started = false;
    private int strewParts = 25;
    private float delay = 0.2f;
    private float currentDelay = 0;
    public UnityEvent strewFinished = new UnityEvent();
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!started)
            return;

        if (currentDelay > 0)
            currentDelay -= Time.deltaTime;

        if(Input.GetMouseButton(0))
        {

            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f;
         

            if(currentDelay <= 0)
            {
                GameObject copy = Instantiate(particle);
                copy.GetComponent<SpriteRenderer>().sprite = Global.DataBase.GetStrewSpriteById(GetComponent<StrewingItem>().strewingItem.Id);
                copy.transform.position = pos;
                copy.GetComponent<Rigidbody2D>().angularVelocity += (Random.value - 0.5f) * 100;
                currentDelay = delay;
                --strewParts;
            }
        }
       
		
        if(0 == strewParts)
        {
            StopStrewing();
            strewFinished.Invoke();
        }
	}

    public void StartStrewing()
    {
        started = true;
       }

    public void StopStrewing()
    {
        started = false;
        GetComponent<SceneFinish>().WaitToFinish();
    }

}
