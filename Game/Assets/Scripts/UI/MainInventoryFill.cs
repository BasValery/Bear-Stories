using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainInventoryFill : MonoBehaviour {

    public GameObject EmptyPrefab;
    public GameObject ItemPrefab;

    private List<Item> items;
    private int inventoryCount;
    // Use this for initialization
    void Start () {
        inventoryCount = Global.DataBase.InventoryCount();
        Refilled();
     }
	
	// Update is called once per frame
	void Update () {
        if(inventoryCount != Global.DataBase.InventoryCount())
        {
            Refilled();
            inventoryCount = Global.DataBase.InventoryCount();
        }
		
	}
    public void Refilled()
    {
        for(int i = 0; i < transform.childCount;i++)
        {

            Destroy(transform.GetChild(i).gameObject);
        }

        items = Global.DataBase.getUniqueInventory();

        for (int i = 0; i < items.Count; i++)
        {
            GameObject copy = Instantiate(ItemPrefab);
            copy.transform.GetChild(0).GetComponent<Image>().sprite = items[i].getSprite();

            copy.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text 
            = Global.DataBase.getCountById(items[i].Id).ToString();
            copy.transform.Find("Text").GetComponent<Text>().text = items[i].Title;
            copy.transform.SetParent(transform);
            copy.transform.localScale = new Vector3(1, 1, 1);
            copy.GetComponent<ItemDrag>().item = items[i];
       
        }
        for (int i = 0; i < Global.DataBase.inventoryCapacity - items.Count; i++)
        {
            GameObject copy = Instantiate(EmptyPrefab);
            copy.transform.SetParent(transform);
            copy.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
