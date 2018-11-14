using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameControl : MonoBehaviour
{

    // Use this for initialization

    #region PublicValues
    public RectTransform WaterLevel;
    public GameObject Sign;
    public GameObject Arrow;
    public GameObject ingredientPrefab;
    public GameObject contentPanel;
    public GameObject boilingObject;
//    public DataBase dataBase;
    public float Coefficient;
    public Item boilingItem { get; set; }
    #endregion

    #region Properties
    public float temp;
    public float Temp
    {
        get
        {
            return temp;
        }
        private set
        {

            if (value > 250)
                value = 250;
            if (value < 0)
                value = 0;
            Sign.transform.Translate(0, (value - temp)/75, 0);
            Sign.GetComponentInChildren<Text>().text = ((Mathf.RoundToInt(value * 10) / 10f)).ToString();
            WaterLevel.sizeDelta = new Vector2(WaterLevel.sizeDelta.x, value*1.5f + 50);
            temp = value;
        }
    }

    private float heating;
    public float Heating
    {
        get { return heating; }
        set
        {
            if (value > 360 || value < 0)
                return;
            Arrow.transform.SetPositionAndRotation(Arrow.transform.position, Quaternion.Euler(0, 0, -value));
            heating = value;
        }
    }
    #endregion

    #region PrivateValues
    private List<GameObject> Slots = new List<GameObject>();
    private float endingTemp;
    #endregion


    void Start()
    {
       
            FillInventory();
        Temp = 0;
        Heating = 0;

    }

    // Update is called once per frame
    void Update()
    {

        Temp += (Heating * Coefficient * UnityEngine.Time.deltaTime) - (15 * UnityEngine.Time.deltaTime);
        Heating -= 20 * UnityEngine.Time.deltaTime;
    }

    void FillInventory()
    {
        var list = Global.DataBase.getInventory();
        foreach (Item value in list)
        {
            if (value.Boilable)
            {
                GameObject newSlot = Instantiate(ingredientPrefab);
                Slots.Add(newSlot);
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

    public void fixTemp()
    {
        endingTemp = Temp;
        Destroy(boilingObject);
        boilingObject = null;
        Global.PlayerInfo.Storage["LastPotion"] = boilingItem.Id;
        Global.PlayerInfo.Storage["LastPotionTemp"] = endingTemp;
        
    }
    
    public void finish()
    {


    }
 
}

