using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDrive : MonoBehaviour
{

    public float Speed;
    private bool free = true;
    public bool Free { get { return free; } }
    private GameArrowDirection arrowDirection;
    public GameArrowDirection ArrowDirection
    {
        get { return arrowDirection; }
        set
        {
            
            arrowDirection = value;
            switch (value)
            {
                case GameArrowDirection.UP:
                    Rect.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                case GameArrowDirection.RIGHT:
                    Rect.rotation = Quaternion.Euler(0, 0, -90);
                    break;
                case GameArrowDirection.LEFT:
                    Rect.rotation = Quaternion.Euler(0, 0, 90);
                    break;
                case GameArrowDirection.DOWN:
                    Rect.rotation = Quaternion.Euler(0, 0, 180);
                    break;
            }
        }
    }

    private RectTransform Rect;
    private int XRange;
    private void Awake()
    {
        Rect = GetComponent<RectTransform>();
    }
    void Start()
    {

        XRange = (int)(transform.parent.GetComponent<RectTransform>().rect.width / 2 + this.GetComponent<RectTransform>().rect.width / 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Free)
        {
            Rect.transform.Translate(-Speed * Time.deltaTime, 0, 0, Space.World);
            if (Rect.localPosition.x < -XRange)
                free = true;
        }
    }

    public void Move()
    {
        free = false;
    }
}

