using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentDirection : MonoBehaviour
{
    public GameArrowDirection CurrentArrowDirection { get; private set; }
    public DancingControl dancingControl;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "FlamingoArrow")
        {
            CurrentArrowDirection = other.gameObject.GetComponent<ArrowDrive>().ArrowDirection;
          
            dancingControl.ArrowCome();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "FlamingoArrow")
        {
            CurrentArrowDirection = GameArrowDirection.NONE;
           
            dancingControl.ArrowLeave();
        }
    }
}
