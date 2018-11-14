using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour {

    // Use this for initialization

#region PublicValues
    public RectTransform WaterLevel;
    public GameObject Sign;
    public GameObject Arrow;
    public GameObject ingredientPrefab;
    public GameObject contentPanel;
    public GameObject boilingObject;
    public DataBase dataBase;
    public float Coefficient;
    public Item boilingItem { get; set; }
#endregion

    public float temp;
    public float Temp {
        get {
            return temp;
        }
        private set {

            if (value > 250)
                value = 250;
                if ( value < 0)
                value = 0;
            Sign.transform.Translate(0, value - temp, 0);
            Sign.GetComponentInChildren<Text>().text =((Mathf.RoundToInt(value * 10) / 10f)).ToString();
            WaterLevel.sizeDelta = new Vector2(WaterLevel.sizeDelta.x, value + 50);
            temp = value;
        }
    }

    private float heating;
    public float Heating { get { return heating; }
        set {
            if (value > 360 || value < 0)
                return;
            Arrow.transform.SetPositionAndRotation(Arrow.transform.position, Quaternion.Euler(0, 0, -value));
            heating = value;
        }
    }

 
    private List<GameObject> Slots = new List<GameObject>();

    void Start () {
      
        FillInventory();
        Temp = 0;
        Heating = 0;
       
	}
	
	// Update is called once per frame
	void Update () {

        Temp += (Heating * Coefficient * UnityEngine.Time.deltaTime) - (15 * UnityEngine.Time.deltaTime);
        Heating -= 20 * UnityEngine.Time.deltaTime;
	}

    void FillInventory()
    {
        var list = dataBase.getInventory();
        foreach (Item value in list)
        {
            if (value.Boilable)
            {
                GameObject newSlot = Instantiate(ingredientPrefab);
                Slots.Add(newSlot);
                newSlot.transform.parent = contentPanel.transform;
                newSlot.GetComponent<Image>().sprite = value.getSprite();
                newSlot.transform.Find("Text").GetComponent<Text>().text = value.Title;
                newSlot.GetComponent<BoilngInventoryDrag>().item = value;
            }
        }
    }
    /*
  public bool isChosen()
  {
      return liquid != null;
  }*/
    /*
    private Liquid liquid;
    public Liquid LiquidBoiling
    {
        set
        {
            liquid = value;
        }
    }*/
}
/*
public class Liquid
{
    public Sprite SpriteSmall { get; private set; }
    public Sprite SpriteBig { get; private set; }
    public string Name { get; private set; }

    public Liquid(Sprite spriteSmall, Sprite spriteBig, string name)
    {
        this.SpriteBig = spriteBig;
        this.SpriteSmall = spriteSmall;
        this.Name = name;
    }
}
*/
