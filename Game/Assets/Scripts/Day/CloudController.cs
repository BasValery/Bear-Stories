using Assets.Scripts.Sky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour {

    #region Fields
    public float m_xMax;
    public float m_xMin;
    public float m_xSpeed;
    protected bool m_launched = true;
    #endregion //Fields

    #region Properties
    public bool Launched
    {
        get { return m_launched; }
    }

    public Vector3 CurrentPos
    {
        get { return transform.position; }
    }
    #endregion //Properties

    #region Messages
    // Use this for initialization
    void Start () {

	}

    void Awake()
    {

    }

    void OnValidate()
    {
        if (m_xSpeed < 0)
        {
            Debug.LogWarning("Speed is less than zero");
            m_xSpeed = Mathf.Abs(m_xSpeed);
        }
    }

    // Update is called once per frame
    void Update () {
        if (m_launched == true)
        {

            transform.Translate(Mathf.Abs(m_xSpeed) * UnityEngine.Time.deltaTime, 0, 0);
            if (transform.position.x > m_xMax)
            {
                transform.position = new Vector3(m_xMin,
                    transform.position.y,
                    transform.position.z
                    );
            }
        }
	}
    #endregion //Messages

    #region Methods
    public void Launch()
    {
        m_launched = true;
    }

    public void Stop()
    {
        m_launched = false;
    }

    public void Restart()
    {
        transform.position = new Vector3(
            m_xMin,
            transform.position.y
        );
    }
    #endregion //Methods
}
