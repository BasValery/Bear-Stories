using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForestControl : MonoBehaviour
{
    #region Fields
    public float m_xFrom = 0f;
    public float m_xTo = 50f;
    public Light m_sunLight;
    public Vector2 m_initialPlayerPos;

    private List<CloudController> m_clouds = null;
    private CameraController m_cameraController;
    private GameTime m_gameTime;
    private GameObject m_player;

    #endregion //Fields

    #region Messages
    // Use this for initialization
    private void Start()
    {
        m_cameraController = Camera.main.GetComponent<CameraController>();

        m_player = GameObject.FindGameObjectWithTag("Player");
        if (Global.Teleporter.ContainsRecord("Forest") == false)
        {
            Global.Teleporter.SaveTeleportationPoint(
                "Forest",
                m_cameraController.transform.position,
                m_player.transform.position,
                Camera.main.orthographicSize,
                m_xFrom,
                m_xTo
                );
            m_player.transform.position = m_initialPlayerPos;
            m_cameraController.SetLimits(m_xFrom, m_xTo);
        }
    }


    private void Awake()
    {
        m_clouds = GetComponentsInChildren<CloudController>().ToList();
        m_gameTime = Global.Time;

        foreach (CloudController cloud in m_clouds)
        {
            cloud.m_xMin = m_xFrom - 4;
            cloud.m_xMax = m_xTo + 4;
        }

        if (m_gameTime.IsFreezed == false)
        {
            m_clouds.ForEach(x => x.Launch());
        }
    }

    private void OnEnable()
    {
        m_gameTime.TimeFreezed += OnGameTimeFreezed;
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    private void OnDisable()
    {
        m_gameTime.TimeFreezed -= OnGameTimeFreezed;
        SceneManager.activeSceneChanged -= OnActiveSceneChanged;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Global.Time.Freeze(!Global.Time.IsFreezed);
        }
    }
    #endregion //Messages

    #region Methods
    private void OnGameTimeFreezed(object sender, TimeFreezedEventArgs e)
    {
        if (m_clouds != null)
        {
            m_clouds.ForEach(x => {
                if (e.Freezed)
                    x.Stop();
                else
                    x.Launch();
            });
        }
    }

    private void OnActiveSceneChanged(Scene arg0, Scene arg1)
    {

    }
    #endregion //Methods
}
