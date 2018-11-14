using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFinish : MonoBehaviour {

    private float wait = 3f;
    private bool finishing = false;
    private bool wrote = false;
    //public DataBase dataBase;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!finishing)
            return;
        wait -= Time.deltaTime;
        if (wait <= 0)
            Finish();
	}

    public void WaitToFinish()
    {
        finishing = true;
    }

    public void Finish()
    {
        Debug.Log("Finished");
        int quality = 100 - (25 - GetComponent<ParticalCounter>().Count) * 4;
        if (!wrote)
        {
           Global.DataBase.deleteFromInventory(GetComponent<StrewingItem>().strewingItem.Id);
            wrote = true;

           var potion = PotionConverter.GetPotion(
                (int)Global.PlayerInfo.Storage["LastPotion"],
                GetComponent<StrewingItem>().strewingItem.Id,
                GetComponent<StrewingItem>().strewingItem.Quality,
                 Global.PlayerInfo.Storage["LastPotionTemp"]);
            if(potion.Id != -1)
            {
                Global.DataBase.addItemToInventory(potion.Id, potion.Quality);
            }
        }
        Global.Load.LoadScene("LairInside");
    }
}
