using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetImgAndText : MonoBehaviour {

    public Text text;
    public Image image;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Set(string text, Sprite image)
    {
        this.text.text = text;
        this.image.sprite = image;
    }
}
