using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum MovementSpecialState
{
    None, Flying, Bouncing, Falling
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(OrderLayerByY))]
[RequireComponent(typeof(NotAffectedByGravity))]
[RequireComponent(typeof(Animator))]
public class MovementGuider : MonoBehaviour
{
    #region Fields
    public string m_state = "state";
    public float m_speedMultiplier = 1;
    private bool m_locked = false;
    private MovementSpecialState m_specialState = MovementSpecialState.None;
    private bool m_freezed = false;
    private float m_speed;
    private Collider2D m_collider;

    protected Destination m_destination;
    protected Animator m_animator;

    #endregion //Fields


    #region Events
    public event EventHandler DestinationChanged;
    public event EventHandler MovementFreezed;
    public event EventHandler<DestinationReachedEventArgs> DestinationReached;
    #endregion //Events

    #region Properties
    public Animator Animator
    {
        get { return m_animator; }
    }

    public bool Locked
    {
        get { return m_locked; }
        set { m_locked = value; }
    }

    public MovementSpecialState SpecialState
    {
        get { return m_specialState; }
        set
        {
            m_specialState = value;
            if (m_destination != null)
            {
                ApplyMovementAnimation();
            }
        }
    }

    public Bounds Bounds
    {
        get { return m_collider.bounds; }
    }
    
    public Vector3 Bottom
    {
        get
        {
            return new Vector3(
                transform.position.x,
                m_collider.bounds.min.y,
                transform.position.z
                );
        }
    }

    public bool Freezed
    {
        get { return m_freezed; }
        set
        {
            m_freezed = value; 
            if (MovementFreezed != null)
            {
                MovementFreezed(this, new EventArgs());
            }
        }
    }

    public Destination Destination
    {
        get { return m_destination; }
        set
        {
            /*
            if (m_freezed)
            {
                return;
            }
            */
            if (m_locked)
            {
                return;
            }

            float width = GetComponent<BoxCollider2D>().size.x;

            if (m_destination != null)
            {
                m_destination.Abort();
            }
            if (DestinationChanged != null)
            {
                DestinationChanged.Invoke(this, new EventArgs());
            }
            m_destination = value;
            if (m_destination == null)
            {
                m_animator.SetInteger(m_state, (int)NPCSTate.Stop);
                return;
            }
            else
            {
                m_destination.Position = Align(m_destination.Position);
            }

            //Mathf.Clamp();

            Vector3 scale = transform.localScale;

            if (m_destination.Position.x > transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x);
            }
            else if (m_destination.Position.x < transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x) * -1f;
            }
            transform.localScale = scale;

            ApplyMovementAnimation();
        }
    }

    #endregion //Properties

    #region Messages
    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_collider = GetComponent<Collider2D>();
        if (m_speed == 0)
        {
            m_speed = Global.PlayerInfo.m_speed;
        }
    }

    private void OnEnable()
    {
        Global.Time.TimeFreezed += Time_TimeFreezed;
    }

    private void OnDisable()
    {
        Global.Time.TimeFreezed -= Time_TimeFreezed;
    }

    private void Start()
    {
        if (Global.Time.IsFreezed)
        {
            m_freezed = true;
            m_animator.speed = 0;
        }
    }

    private void Update()
    {
        if (!m_freezed && !Global.Time.IsFreezed && m_destination != null)
        {
            Move();
            CheckDestinationReach();
        }
    }
    #endregion //Messages

    #region Methods
    private void Move()
    {
        if (m_destination != null)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                m_destination.Position,
                m_speed * m_speedMultiplier * Time.deltaTime
                );
        }
    }
    
    /*
    private void Scale(Vector3 from, Vector3 to)
    {
        float diff = from.y - to.y;

        if (diff > 0)
        {
            transform.localScale = new Vector3(
                transform.localScale.x + 0.001f,
                transform.localScale.y + 0.001f,
                transform.localScale.z
                );
        }
        else if (diff < 0)
        {
            transform.localScale = new Vector3(
            transform.localScale.x - 0.001f,
            transform.localScale.y - 0.001f,
            transform.localScale.z
            );
        }
    }
    */

    private void CheckDestinationReach()
    {
        if (m_destination != null)
        {
            if (transform.position == m_destination.Position)
            {
                var previousDestinaton = m_destination;

                if (previousDestinaton == m_destination)
                {
                    m_animator.SetInteger(m_state, (int)NPCSTate.Stop);
                }

                if (m_destination.AutoInvoke == true)
                {
                    m_destination.Invoke();
                }

                if (m_destination == previousDestinaton)
                {
                    m_destination = null;
                }
            }
        }
    }

    private float GetDirection(float value)
    {
        return value > 0 ? 1 : -1;
    }

    private Vector3 Align(Vector3 position)
    {
        Bounds bounds = m_collider.bounds;
        Vector3 offset = m_collider.offset;
        float y = m_destination.Position.y + bounds.extents.y;
        
        //if higher
        if (offset.y >= 0)
        {
            y -= Math.Abs(offset.y);
        }
        else
        {
            y += Math.Abs(offset.y);
        }
        //float y = m_destination.Position.y;
        return new Vector3(
                /*Mathf.Clamp(m_destination.Position.x, CityControl.instance.m_xFrom, CityControl.instance.m_xTo)*/m_destination.Position.x,
                /*transform.position.y,*/y,
                transform.position.z
                );
    }


    private void Time_TimeFreezed(object sender, TimeFreezedEventArgs e)
    {
        Freezed = e.Freezed;
        if (e.Freezed)
        {
            m_animator.speed = 0;
        }
        else
        {
            m_animator.speed = 1;
        }
    }

    private void ApplyMovementAnimation()
    {
        int anim = (int)NPCSTate.Walk;
        switch (m_specialState)
        {
            case MovementSpecialState.Bouncing:
                anim = (int)NPCSTate.Jump;
                break;
            case MovementSpecialState.Flying:
                anim = (int)NPCSTate.Fly;
                break;
            case MovementSpecialState.Falling:
                anim = (int)NPCSTate.Falling;
                break;
        }
        
        m_animator.SetInteger(m_state, anim);
    }
    #endregion //Methods
}

