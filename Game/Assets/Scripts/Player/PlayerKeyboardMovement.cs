using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerKeybordMovement : MonoBehaviour {

    #region Fields
    public Vector3 jump;
    public float m_jumpForce = 500.0f;
    private Rigidbody2D m_rigidBody;
    private Vector3 m_movement;
    private Animator m_animator;
    #endregion //Fields

    #region Methods
    #endregion //Methods

    #region Messages
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        m_rigidBody.gravityScale = 0;
        jump = new Vector2(0.0f, 12.0f);
    }


    void FixedUpdate()
    {
        transform.Translate(m_movement);
    }

    //private void OnTriEnter2D(Collision2D collision)
    //{
    //    var outline = collision.gameObject.GetComponent<SpriteOutline>();
    //    if (outline != null)
    //    {
    //        outline.outlineSize = 4;
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    var outline = collision.gameObject.GetComponent<SpriteOutline>();
    //    if (outline != null)
    //    {
    //        outline.outlineSize = 0;
    //    }
    //}

    void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            if (m_animator.GetBool("talk") == false)
            {
                m_animator.SetBool("talk", true);
            }
            else
            {
                m_animator.SetBool("talk", false);
            }
        }



        Vector3 scale = m_rigidBody.transform.localScale;

        var x = Input.GetAxis("Horizontal") * UnityEngine.Time.deltaTime * 7.0f;
        var y = Input.GetAxis("Vertical") * UnityEngine.Time.deltaTime * 5.0f;
        
       
        if (x > 0)
        {
            m_rigidBody.transform.localScale = scale;
            if (scale.x > 0)
            {
                scale.x *= -1;
            }
            if (m_animator.GetInteger("state") != 1)
            {
                m_animator.SetInteger("state", 1);
            }
            transform.localScale = scale;
        }
        else if (x < 0)
        {
            if (scale.x < 0)
            {
                scale.x *= -1;
            }
            if (m_animator.GetInteger("state") != 1)
            {
                m_animator.SetInteger("state", 1);
            }
            transform.localScale = scale;
        }
        else if (y > 0)
        {
            if (m_animator.GetInteger("state") != 1)
            {
                m_animator.SetInteger("state", 1);
            }
            //transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - 0.005f);
        }
        else if (y < 0)
        {
            if (m_animator.GetInteger("state") != 1)
            {
                m_animator.SetInteger("state", 1);
            }
            //transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + 0.005f);
        }
        else
        {
            m_animator.SetInteger("state", 3);
        }

       
        
        m_movement = new Vector3(x, y);
    }
    #endregion //Messages

}
