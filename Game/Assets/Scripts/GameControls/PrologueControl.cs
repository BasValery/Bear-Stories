using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PrologueControl : MonoBehaviour
{
    #region Fields
    public MovementGuider m_postmanMovementGuider;
    public Vector3 m_endPoint;
    public Vector2 m_letterPosition;
    public Animator m_letterObject;


    public void Update()
    {
       
    }

    #endregion //Fields

    #region Messages
    private void Awake()
    {



    }
    public void Start()
    {
        Global.Hud.hudDisable();
        StartCoroutine(StartWaiting());
    }
    #endregion //Messages

    #region Methods

    private IEnumerator StartWaiting()
    {
        yield return new WaitForSeconds(3);
        m_postmanMovementGuider.Destination = new Destination(
        new Vector3(m_letterPosition.x, m_letterPosition.y, transform.position.z),
        LetterReached,
        null);
    }

    private void LetterReached()
    {
        m_letterObject.SetTrigger("Fall");
        m_postmanMovementGuider.Destination = new Destination(m_endPoint, endPositionReached);
    }
    private void endPositionReached()
    {
        //Global.Hud.hudEnable();
        Global.Load.FadeScene("Game");
    }
    private void InitialPosReached()
    {

    }
    #endregion //Methods
}
