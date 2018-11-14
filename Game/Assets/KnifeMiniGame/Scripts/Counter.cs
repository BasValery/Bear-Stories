using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Counter : MonoBehaviour {

    // Use this for initialization

    private int hited = 0;
    private int unhited = 0;
    private float delay = 1f;
    private bool wrote = false;
    public SliceScript sliceScript;
    //public DataBase dataBase;
    public UnityEvent finish = new UnityEvent();

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if ( hited >= 3)
        {
            sliceScript.inputAccept = false;
            if (delay <= 0)
            {
                int quality = 100 - unhited * 20;
                quality = quality < 0 ? 0 : quality;

                if (!wrote)
                {
                    try
                    {

                        Global.DataBase.addItemToInventory(KnifeConverter.getAfterKnife(GetComponent<CuttingItem>().cutting.Id), quality);
                        Global.DataBase.deleteFromInventory(GetComponent<CuttingItem>().cutting.Id);
                    }
                    catch { }

                    wrote = true;
                }

                Global.Load.LoadScene("LairInside");
                finish.Invoke();
            }
            else
                delay -= Time.deltaTime;
        }
	}

    public void Hit()
    {
       
        ++hited;
    }

    public void UnHit()
    {
        ++unhited;
    }
}
