using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportationPoint
{
    public Vector3 CameraPosition { get; set; }
    public Vector3 PlayerPosition { get; set; }
    public float CameraExtent { get; set; }
    public float FromX {get; set; }
    public float ToX {get; set; }
}

public class Teleporter : MonoBehaviour
{
    #region Fields
    private Dictionary<string, TeleportationPoint> m_teleportPoints;
    public string m_baseSceneName;
    private GameObject m_player;
    private MovementGuider m_playerMovementGuider;
    #endregion //Fields

    #region Properties
    #endregion //Properties

    #region Messages
    private void Awake()
    {
        m_teleportPoints = new Dictionary<string, TeleportationPoint>();
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    #endregion //Messages

    #region Methods
    public void Launch(string scene)
    {
        Teleport(scene);
    }

    //force new save point
    public void SaveTeleportationPoint(string scene, Vector3 cameraPos, Vector3 playerPos, float cameraExtent)
    {
        m_teleportPoints[scene] = new TeleportationPoint() { CameraPosition = cameraPos, PlayerPosition = playerPos, CameraExtent = cameraExtent };
    }

    public void SaveTeleportationPoint(string scene, Vector3 cameraPos, Vector3 playerPos, float cameraExtent, float fromX, float toX)
    {
         m_teleportPoints[scene] = new TeleportationPoint()
         {
            CameraPosition = cameraPos,
            PlayerPosition = playerPos,
            CameraExtent = cameraExtent,
            FromX = fromX,
            ToX = toX
        };
    }

    public void SaveTeleportationPoint(string scene, TeleportationPoint teleportationPoint)
    {
        m_teleportPoints[scene] = teleportationPoint;
    }

    public bool ContainsRecord(string scene)
    {
        return m_teleportPoints.ContainsKey(scene);
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        TeleportationPoint teleportationPoint;
        CameraController cameraContoller = Camera.main.GetComponent<CameraController>();
        if (TryInitPlayerReference())
        {
            if (m_teleportPoints.TryGetValue(arg1.name, out teleportationPoint))
            {
                m_player.transform.position = teleportationPoint.PlayerPosition;
                Camera.main.transform.position = teleportationPoint.CameraPosition;
                Camera.main.orthographicSize = teleportationPoint.CameraExtent;
                if (cameraContoller != null && teleportationPoint.FromX != teleportationPoint.ToX)
                {
                    cameraContoller.SetLimits(teleportationPoint.FromX, teleportationPoint.ToX);
                }
                else
                {
                    Debug.LogWarning("Can't store limits, camera controller is not found");
                }
            }
        }
    }

    private bool TryInitPlayerReference()
    {
        if (m_player == null)
        {
            m_player = GameObject.FindGameObjectWithTag("Player");
            if (m_player)
            {
                m_playerMovementGuider = m_player.GetComponent<MovementGuider>();
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    private void Teleport(string sceneName)
    {
        if (sceneName == Global.Load.CurrentSceneName)
        {
            sceneName = m_baseSceneName;
        }
        CameraController cameraController = Camera.main.GetComponent<CameraController>();
        if (cameraController != null)
        {
            m_teleportPoints[Global.Load.CurrentSceneName] = new TeleportationPoint()
            {
                CameraPosition = Camera.main.transform.position,
                CameraExtent = Camera.main.orthographicSize,
                PlayerPosition = m_player.transform.position,
                FromX = cameraController.XFrom,
                ToX = cameraController.xTo
            };
        }
        else
        {
            Debug.LogWarning("Can't store, camera controller is not found");
        }
        Global.Load.LoadScene(sceneName);
        m_playerMovementGuider.Destination = null;
    }
    #endregion //Methods
}