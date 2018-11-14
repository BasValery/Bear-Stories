using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FillHud : MonoBehaviour {


   // public DataBase dataBase;
    public GameObject ingredientPrefab;
    public GameObject contentPanel;
    private List<GameObject> Slots = new List<GameObject>();

    // Use this for initialization
    void Start () {
        FillInventory();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FillInventory()
    {
        var list = Global.DataBase.getUniqueInventory();
        foreach (Item value in list)
        {
            if (value.Cuttable)
            {
                GameObject newSlot = Instantiate(ingredientPrefab);
                Slots.Add(newSlot);
                //newSlot.transform.parent = contentPanel.transform;
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
