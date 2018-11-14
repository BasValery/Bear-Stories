using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SlotFiller : MonoBehaviour {

    public Image image;
    public Text description;
    public Text count;
    public Text price;
    public Button button;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetAll(Sprite sprite, string description, string count, string price)
    {
        image.sprite = sprite;
        this.description.text = description;
        this.count.text = count;
        this.price.text = price;

        if (Convert.ToInt32(count) <= 0)
            button.interactable = false;
    }
}
