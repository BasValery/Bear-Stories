using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour {

    public GameObject InventoryPanel;
    public GameObject SlotPanel;
    public GameObject InventoryNoSlot;
    public GameObject InventorySlot;
    public GameObject InventoryItem;
   
    public List<Item> Items = new List<Item>();
    public List<GameObject> Slots = new List<GameObject>();

  
    // Use this for initialization
    void Start () {
       
        AddItem(0);
        AddItem(1);
        AddItem(2);
        AddItem(3);
        AddItem(4);

        AddEmpty();
        AddEmpty();
    }
	
    public void AddItem(int id)
    {

        Item itemToAdd = Global.DataBase.getById(id);
        if (itemToAdd.Id == -1)
               return;

        Slots.Add(Instantiate(InventorySlot));
        Items.Add(itemToAdd);
        Slots[Slots.Count - 1].transform.SetParent(SlotPanel.transform);

        GameObject objectToAdd = Instantiate(InventoryItem);
        objectToAdd.transform.SetParent(Slots[Slots.Count - 1].transform);
        objectToAdd.transform.position = Vector2.zero;
        objectToAdd.transform.localScale = new Vector3(0.9f, 0.9f, 1);
        objectToAdd.GetComponent<Image>().sprite = itemToAdd.getSprite();
        objectToAdd.transform.Find("Text").GetComponent<Text>().text = itemToAdd.Title;



    }

    public void AddEmpty()
    {
        Slots.Add(Instantiate(InventoryNoSlot));
        Items.Add(new Item());
        Slots[Slots.Count - 1].transform.SetParent(SlotPanel.transform);
    }

	// Update is called once per frame
	void Update () {
	}
}
