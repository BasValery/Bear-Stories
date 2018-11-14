using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Sell : MonoBehaviour {

    public Item item;
    public Button button;
    public Text count;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void sellById()
    {
        if (Global.PlayerInfo.m_money < Global.DataBase.getWhitcherPrice(item.Id))
            return;
        if (Global.DataBase.UniqueInventoryCount() + 1 > Global.DataBase.inventoryCapacity)
            return;
        Global.PlayerInfo.m_money -= Global.DataBase.getWhitcherPrice(item.Id);
        int result = Global.DataBase.whitcherSell(item.Id);
      
        Global.DataBase.addItemToInventory(item.Id, item.Quality);
        if(8 == item.Id)
        {
            Global.PlayerInfo.DailyTaskCompleted(5);
        }
        Global.Hud.RefillInventory();
        if (0 == result)
            button.interactable = false;
        count.text = result.ToString();

    }
}
