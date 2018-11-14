using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Surface : MonoBehaviour {
    #region Fields
    public Camera m_camera;
    private MovementGuider m_player;
    private Collider2D m_collider;
    private Collider2D m_playerCollider;
    #endregion //Fields

    #region Messages
    private void Start ()
    {
	}

    private void Awake()
    {
        m_collider = GetComponent<BoxCollider2D>();

        var playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject)
        {
            m_player = playerObject.GetComponent<MovementGuider>();
            m_playerCollider = playerObject.GetComponent<Collider2D>();
        }
        else
        {
            Debug.LogWarning("Player wasn't found by Surface");
        }
    }

    private void Update ()
    {
		
	}

    private void OnMouseDown()
    {
        if (m_camera != null /*&& m_collider.bounds.Intersects(m_playerCollider.bounds)*/)
        {
            Vector3 pos = Input.mousePosition;
            pos = m_camera.ScreenToWorldPoint(pos);
            m_player.Destination = new Destination(
                pos,
                OnDestinationReached,
                OnAborted
                );
            pos.z = m_player.transform.position.z;
        }
    }
    #endregion //Messages

    #region Methods
    private void OnDestinationReached()
    {

    }

    private void OnAborted()
    {

    }
    #endregion //Methods
}
