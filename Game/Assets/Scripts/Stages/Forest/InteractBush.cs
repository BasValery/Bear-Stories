using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBush : Target {

    public int ItemBushId;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override void OnReached()
    {
        base.OnReached();
        Global.DataBase.addItemToInventory(ItemBushId, 100);
        Global.Hud.RefillInventory();
        gameObject.SetActive(false);
    }
}
