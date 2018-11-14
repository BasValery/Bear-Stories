using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InfoPanelSetter : MonoBehaviour {

    public Text text;
    public Text title;
    public Image img;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Set (Item item)
    {
        this.img.sprite = item.getSprite();
        title.text = item.Title;
        img.preserveAspect = true;
        if (item.Ingredient)
        {
            this.text.text = Global.DataBase.GetIngredientDescriotion(item.Id);

        }
        else if(item.Potion)
        {
            text.text = Global.DataBase.GetPotionDescriotion(item.Id);
        }
           
    }

    public void Set(Achive item)
    {
        this.img.sprite = item.getSprite();
        title.text = item.Title;
        img.preserveAspect = true;
        text.text = item.Description;

    }

    public void Set(Persons item)
    {
        this.img.sprite = item.getSprite();
        title.text = item.Title;
        img.preserveAspect = true;
        text.text = item.Description;

    }
}
