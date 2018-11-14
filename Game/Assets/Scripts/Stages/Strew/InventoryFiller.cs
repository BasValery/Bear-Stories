using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryFiller : MonoBehaviour {

    public GameObject contentPanel;
    //public DataBase dataBase;
    public GameObject ingredientPrefab;
    // Use this for initialization
    void Start () {
        FillInventory();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FillInventory()
    {
        var list = Global.DataBase.getInventory();
        foreach (Item value in list)
        {
            if (value.Strewable)
            {
                GameObject newSlot = Instantiate(ingredientPrefab);

                newSlot.transform.SetParent(contentPanel.transform, false);
                var img = newSlot.transform.Find("Image");
                img.GetComponent<Image>().sprite = value.getSprite();
                img.GetComponent<Image>().preserveAspect = true;
                img.localScale = new Vector3(1f, 1f, 1);
                newSlot.transform.Find("Text").GetComponent<Text>().text = value.Title;
                newSlot.transform.Find("Count").GetComponent<Text>().text = Global.DataBase.getCountById(value.Id).ToString();
                newSlot.GetComponent<BoilngInventoryDrag>().item = value;
            }
        }
    }
}
