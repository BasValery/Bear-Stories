using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForestShower : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Global.DataBase.getFromInventoryById(14).Id != -1)
        {
            Global.DataBase.deleteFromInventory(14);
            GetComponent<Image>().enabled = true;
            GetComponent<Button>().enabled = true;
            GetComponent<ForestShower>().enabled = false;
            Global.PlayerInfo.DailyTaskCompleted(20);
        }
    }
}
