using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraController : MonoBehaviour
{

    #region Fields
    public Camera m_camera;
    public float m_scaleSpeed = 0.2f;
    public float m_maximumExtent = 11f;
    public float m_minimumExtent = 6f;
    
    private float m_fromX;
    private float m_toX;

    private GameObject m_trackObject;
    private GameObject m_background;
    private GameObject m_player;
    private Vector3 m_offset;
    private PlayerInfo m_playerInfo;
    #endregion //Fields

    #region Events
    public UnityEvent m_cameraScaled;
    public UnityEvent m_cameraMoved;
    #endregion //Events

    #region Methods
    public float XFrom
    {
        get { return m_fromX; }
    }

    public float xTo
    {
        get { return m_toX; }
    }
    #endregion //Methods

    #region Messages
    // Use this for initialization
    void Start()
    {
        var cityControl = CityControl.instance;
    }

    private void Awake()
    {
        m_playerInfo = Global.PlayerInfo;
        m_player = GameObject.FindGameObjectWithTag("Player").gameObject;
        m_background = FindObjectOfType<DayNightSycle>().gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (m_trackObject == null)
        {
            UpdateCamera();
        }
        else
        {
            Track();
        }
    }

    private void LateUpdate()
    {
    }
#endregion //Messages

#region Methods
    public void Lock(GameObject gameObject, float size)
    {
        m_trackObject = gameObject;
        var delta = size - m_camera.orthographicSize;
        m_camera.transform.Translate(0, delta, 0);
        m_camera.orthographicSize = size;
    }

    public void Lock(GameObject gameObject)
    {
        m_trackObject = gameObject;
    }

    public void Free()
    {
        m_trackObject = null;
    }

    public void Free(float x)
    {
        m_trackObject = null;
        m_camera.transform.position = new Vector3(
            x,
            m_camera.transform.position.y,
            m_camera.transform.position.z
            );
    }

    public void SetLimits(float xFrom, float xTo)
    {
        m_fromX = xFrom;
        m_toX = xTo;
    }

    public Vector2 CalculateCameraExtents()
    {
        Vector2 vec = new Vector2();
        vec.y = m_camera.orthographicSize;
        vec.x = vec.y * ((float)Screen.width / Screen.height);
        return vec;
    }

    public static Vector2 CalculateCameraExtents(float size)
    {
        Vector2 vec = new Vector2(size * ((float)Screen.width / Screen.height), size);
        return vec;
    }

    public static float CalcualteXExtent(float yExtent)
    {
        return yExtent * ((float)Screen.width / Screen.height);
    }

    public Rect CameraBounds()
    {
        return CameraBounds(m_camera.transform.position, m_camera.orthographicSize);
    }

    public static Rect CameraBounds(Vector3 cameraPos, float orthographicSize)
    {
        Vector2 extents = CalculateCameraExtents(orthographicSize);
        return new Rect(
            cameraPos.x - extents.x,
            cameraPos.y - extents.y,
            extents.x * 2.0f,
            extents.y * 2.0f
            );
    }

    private float GetSign(float value)
    {
        return value < 0 ? -1 : 1;
    }


    private void UpdateCamera()
    {
        float playerWidth = m_player.GetComponent<BoxCollider2D>().size.x;

        ScaleCamera();
        //ClampPlayerPosition();
        MoveAndClampCamera(playerWidth);
        UpdateBackgroundPosition(playerWidth);
    }

    private void Track()
    {
        m_camera.transform.position = new Vector3(
            m_trackObject.transform.position.x,
            m_camera.transform.position.y,
            m_camera.transform.position.z
            );
    }

    private void UpdateBackgroundPosition(float playerWidth)
    {
        if (m_background != null)
        {
            float x = transform.position.x;


            float ex = CalcualteXExtent(m_maximumExtent) - playerWidth / 2f;
            x = Mathf.Clamp(x, m_fromX + ex, m_toX - ex);
            m_background.transform.position = new Vector3(x, m_background.transform.position.y);


            m_playerInfo.Storage["CamOrthSize"] = m_camera.orthographicSize;
            m_playerInfo.Storage["PlayerX"] = m_player.transform.position.x;
            m_playerInfo.Storage["PlayerY"] = m_player.transform.position.y;

        }
        else
        {
            Debug.LogWarning("Background is not set");
        }
    }

    private void ScaleCamera()
    {
        if (m_player == null)
        {
            Debug.LogWarning("player is not set");
        }

        Vector3 dest;

#if (UNITY_STANDALONE)
        var mousePos = Input.mousePosition; 
        mousePos = m_camera.ScreenToWorldPoint(mousePos);
        dest = new Vector3(mousePos.x, m_camera.transform.position.y, m_camera.transform.position.z);
        //maximize
        if (Input.mouseScrollDelta.y < 0)
        {
            if (m_camera.orthographicSize < m_maximumExtent)
            {
                m_camera.orthographicSize += m_scaleSpeed;
                m_offset.y += m_scaleSpeed;

                m_camera.transform.position = Vector3.MoveTowards(m_camera.transform.position, dest, -m_scaleSpeed);

                //m_guider.transform.Translate(-(0), -(m_scaleSpeed), 0);
                m_cameraScaled.Invoke();

                m_camera.transform.Translate(0, m_scaleSpeed, 0);
            }
        }
        //minimize
        else if (Input.mouseScrollDelta.y > 0)
        {
            if (m_camera.orthographicSize > m_minimumExtent)
            {
                m_camera.orthographicSize -= m_scaleSpeed;
                m_offset.y -= m_scaleSpeed;

                m_camera.transform.position = Vector3.MoveTowards(m_camera.transform.position, dest, m_scaleSpeed);

                //m_guider.transform.Translate((0), (m_scaleSpeed), 0);
                m_cameraScaled.Invoke();

                //Not rigid camera player y follow
                m_camera.transform.Translate(0, -m_scaleSpeed, 0);
            }
        }
#elif (UNITY_ANDROID || UNITY_IOS)
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);
            
            var touchZeroPos = m_camera.ScreenToWorldPoint(touchZero.position);
            var touchOnePos = m_camera.ScreenToWorldPoint(touchOne.position);

            dest = new Vector3(
                (touchZeroPos.x + touchOnePos.x) / 2f,
                /*touchZeroPos.x,*/
                m_camera.transform.position.y,
                m_camera.transform.position.z
            );

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;


            // ... change the orthographic size based on the change in distance between the touches.

            // minimize
            if (deltaMagnitudeDiff < 0)
            {
                if (m_camera.orthographicSize > m_minimumExtent)
                {
                    m_camera.orthographicSize -= m_scaleSpeed;
                    m_offset.y -= m_scaleSpeed;

                    m_camera.transform.position = Vector3.MoveTowards(m_camera.transform.position, dest, m_scaleSpeed);

                    m_cameraScaled.Invoke();

                    m_camera.transform.Translate(0, -m_scaleSpeed, 0);
                }
            }
            //maximize
            else if (deltaMagnitudeDiff > 0)
            {
                if (m_camera.orthographicSize < m_maximumExtent)
                {
                    m_camera.orthographicSize += m_scaleSpeed;
                    m_offset.y += m_scaleSpeed;

                    m_camera.transform.position = Vector3.MoveTowards(m_camera.transform.position, dest, -m_scaleSpeed);
                    
                    m_cameraScaled.Invoke();

                    m_camera.transform.Translate(0, m_scaleSpeed, 0);
                }
            }
        }
		
#endif
    }


    private void ClampPlayerPosition()
    {
        m_player.transform.position = new Vector3(
            Mathf.Clamp(m_player.transform.position.x,
                m_fromX,
                m_toX
                ),
            m_player.transform.position.y,
            m_player.transform.position.z
            );
    }

    private void MoveAndClampCamera(float playerWidth)
    {
        Vector3 cameraNewPos;
        Rect cameraBounds = CameraBounds();
        cameraNewPos = MoveCamera(m_camera.transform.position);


        cameraNewPos = ClampCamera(cameraNewPos, playerWidth);

        //moving camera 
        transform.position = cameraNewPos;
    }

    private Vector3 ClampCamera(Vector3 currentPos, float playerWidth)
    {
        Rect cameraBounds = CameraBounds();
        return new Vector3(
            Mathf.Clamp(currentPos.x,
                m_fromX + (cameraBounds.width / 2.0f - playerWidth / 2.0f),
                m_toX - (cameraBounds.width / 2.0f - playerWidth / 2.0f)
                ),
            currentPos.y,
            currentPos.z
            );
    }

    private Vector3 MoveCamera(Vector3 currentPosition)
    {
#if (UNITY_STANDALONE)
        Vector2 extents = CalculateCameraExtents();
        var mousePos = Input.mousePosition;
        mousePos = m_camera.ScreenToWorldPoint(mousePos);
        float delta = mousePos.x - m_camera.transform.position.x;

        if (Input.GetMouseButton(1) && Mathf.Abs(delta) > (extents.x * 0.7f))
        {
            currentPosition = new Vector3(
              m_camera.transform.position.x + (delta / 100),
              transform.position.y,
              mousePos.z
              );
            m_cameraMoved.Invoke();
        }
#elif (UNITY_ANDROID || UNITY_IOS)
        if (Input.touchCount == 1)
        {
            Touch touch0 = Input.GetTouch(0);
            var touch0Pos = m_camera.ScreenToWorldPoint(touch0.position);
            var touch1Pos = m_camera.ScreenToWorldPoint(touch0.position - touch0.deltaPosition);
                currentPosition.x -= touch0Pos.x - touch1Pos.x;
        }
#endif
        return currentPosition;
    }
    #endregion //Methods
}
