using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrickShower : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Image>().enabled = false;
        GetComponent<Button>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		if(Global.DataBase.getFromInventoryById(13).Id != -1)
        {
            Global.DataBase.deleteFromInventory(13);
            GetComponent<Image>().enabled = true;
            GetComponent<Button>().enabled = true;
            GetComponent<BrickShower>().enabled = false;
        }
	}
}
