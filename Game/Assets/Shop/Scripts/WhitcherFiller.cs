using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhitcherFiller : MonoBehaviour {

    public List<GameObject> prefabsList;
    public GameObject gridPanel;
    private List<Item> itemList;
    private int index = 0;

	// Use this for initialization
	void Start () {
        itemList = Global.DataBase.getUniqueWhitcher();
       
        foreach (Item item in itemList)
        {
            var copy = Instantiate(prefabsList[index]);
            ++index;
            if (index >= prefabsList.Count)
                index = 0;
            copy.GetComponent<SlotFiller>().SetAll(item.getSprite(),
                item.Title,
                Global.DataBase.getWhitcherItemCount(item.Id).ToString(),
                Global.DataBase.getWhitcherPrice(item.Id).ToString());

            copy.transform.SetParent(gridPanel.transform, false);
            copy.GetComponent<Sell>().item = item;
        }
	}
	
	// Update is called once per frame
	void Update () {
        
            if(itemList != null)
            if (itemList.Count != Global.DataBase.getUniqueWhitcher().Count)
                Refill();
      
      
    }

    void Refill()
    {
        for (int i = 0; i < gridPanel.transform.childCount; i++)
        {
            Destroy(gridPanel.transform.GetChild(i).gameObject);
        }
        itemList = Global.DataBase.getUniqueWhitcher();

        foreach (Item item in itemList)
        {
            var copy = Instantiate(prefabsList[index]);
            ++index;
            if (index >= prefabsList.Count)
                index = 0;
            copy.GetComponent<SlotFiller>().SetAll(item.getSprite(),
                item.Title,
                Global.DataBase.getWhitcherItemCount(item.Id).ToString(),
                Global.DataBase.getWhitcherPrice(item.Id).ToString());

            copy.transform.SetParent(gridPanel.transform, false);
            copy.GetComponent<Sell>().item = item;
        }
    }

  
}
