using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resize : MonoBehaviour {

    private SpriteRenderer spriteRendere;
	// Use this for initialization
	void Start () {
        spriteRendere = GetComponent<SpriteRenderer>();
        sizeOut();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void sizeOut()
    {
        transform.localScale = new Vector3 (1 / spriteRendere.bounds.size.y, 1 / spriteRendere.bounds.size.y, 1);

    }
}
