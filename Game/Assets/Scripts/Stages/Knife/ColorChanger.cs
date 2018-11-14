using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour {

    public SpriteRenderer targetSprite;
    private SpriteRenderer SelfSprite; 
	// Use this for initialization
	void Start () {
        SelfSprite = GetComponent<SpriteRenderer>();
        
    }
	
	// Update is called once per frame
	void Update () {
        Color targetColor = targetSprite.sprite.texture.GetPixel((int)targetSprite.sprite.textureRect.width / 2, (int)targetSprite.sprite.textureRect.height / 2);
        targetColor.a = 150;
        SelfSprite.color = targetColor;
	}

    void Awake()
    {

    }
}
