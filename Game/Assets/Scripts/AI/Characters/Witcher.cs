using Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MovementGuider))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
class Witcher : MonoBehaviour
{
    #region Fields
    public Vector3 m_witcherStoreLocation;
    private bool m_inside;
    private MovementGuider m_movementGuider;
    private Vector3 m_initialPosition;
    private Vector3 m_initialScale;
    private Bounds m_bounds;
    private OrderLayerByY m_orderLyaerByY;
    private List<int> m_acceptablePotions;

    //
    private DialogManager m_witcherDialogManager;
    private GameObject m_player;
    private DialogControl m_dialogControl;
    private bool m_dialogStarted = false;
    private bool m_playerReached = false;
    private bool m_shouldLeave = false;
    #endregion //Fields

    #region Properties
    public bool IsInside
    {
        get { return m_inside; }
    }
    #endregion //Properties

    #region Messages
    private void Awake()
    {
        m_acceptablePotions = new List<int>(1);
        m_movementGuider = GetComponent<MovementGuider>();
        m_orderLyaerByY = GetComponent<OrderLayerByY>();
        m_movementGuider.DestinationReached += DestinationReached;
        m_bounds = GetComponent<Collider2D>().bounds;
        m_initialPosition = transform.position;
        m_witcherDialogManager = new WitcherDialogManager(m_acceptablePotions);
        m_initialScale = transform.localScale;
        m_witcherDialogManager.DialogueIsOver += DialogueIsOver;
        SceneManager.sceneLoaded += SceneLoaded;

        GetComponent<ItemTaker>().Check = Witcher_AfterTake;
    }

    private bool Witcher_AfterTake(Item item)
    {
        if(m_acceptablePotions.Contains(item.Id))
        {
            switch (item.Id)
            {
                case 9:
                    Global.DataBase.WhitcherAddQuestItem(14, 25);
                    return true;
                    break;
            }
        }
        return false;
    }

    private void SceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        m_dialogControl = FindObjectOfType<DialogControl>();
    }

    private void Start()
    {
        m_movementGuider.Destination = new Destination(m_witcherStoreLocation, StoreReached, null);

        m_player = GameObject.FindGameObjectWithTag("Player");
        var playerMovementGuider = m_player.GetComponentInChildren<MovementGuider>();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    private void OnEnable()
    {
        Global.Time.TimeOfDayChanged += Time_TimeOfDayChanged;
    }

    private void OnDisable()
    {
        Global.Time.TimeOfDayChanged -= Time_TimeOfDayChanged;
    }

    private void OnMouseDown()
    {
        if (m_inside)
        {
            var movementGuider = m_player.GetComponent<MovementGuider>();
            var pos = m_witcherStoreLocation;
            pos.x -= 1.4f;
            pos.y -= 0.1f;
            movementGuider.Destination = new Destination(pos, PlayerWitcherReached, null);
            m_dialogStarted = true;
        }
    }


    private void Update()
    {
        if (m_playerReached)
        {
            if (Vector2.Distance(m_player.transform.position, transform.position) > 3f) //|| 
            //    Vector2.Distance(Camera.main.transform.position, m_player.transform.position) > 12f)

            {
                m_playerReached = false;
                m_dialogStarted = false;
                m_dialogControl.EndDialog();
                //                   //
                if (m_shouldLeave)
                {
                    m_movementGuider.Destination = new Destination(m_initialPosition);
                    m_orderLyaerByY.SetLayer("NPC");
                    transform.localScale = m_initialScale;
                    m_shouldLeave = false;
                }
            }
        }
    }
    #endregion //Messages

    #region Methods
    private void DialogueIsOver(object sender, EventArgs e)
    {
        if (m_shouldLeave)
        {
            m_movementGuider.Destination = new Destination(m_initialPosition);
            m_orderLyaerByY.SetLayer("NPC");
            transform.localScale = m_initialScale;
            m_shouldLeave = false;
        }
    }

    private void Time_TimeOfDayChanged(object sender, DayChangedEventArgs e)
    {
        switch (e.CurrentTimeOfDay)
        {
            case TimeOfDay.Day:
                m_movementGuider.Destination = new Destination(m_witcherStoreLocation, StoreReached, null);
                break;
            //                        //
            case TimeOfDay.Night:
                if (m_dialogStarted == false)
                {
                    transform.position = m_witcherStoreLocation;
                    transform.Translate(0, m_bounds.extents.y, 0);
                    m_movementGuider.Destination = new Destination(m_initialPosition);
                    m_orderLyaerByY.SetLayer("NPC");
                    transform.localScale = m_initialScale;
                    m_inside = false;
                }
                else
                {
                    m_shouldLeave = true;
                }
                break;

        }
    }

    private void StoreReached()
    {
        m_inside = true;
        var pos = m_witcherStoreLocation;
        pos.y += 1.85f;
        transform.localScale = new Vector3(
            transform.localScale.x * 0.9f,
            transform.localScale.y * 0.9f,
            transform.localScale.z
            );
        pos.z = transform.position.z;
        m_orderLyaerByY.SetLayer("Store");
        transform.position = pos;
    }

    private void PlayerWitcherReached()
    {
        Global.PlayerInfo.DailyTaskCompleted(3);
        m_witcherDialogManager.FirstParticipantName = "Толик";
        m_witcherDialogManager.SecondParticipantName = "Валера";
        m_witcherDialogManager.FirstHeroImageName = "Player";
        m_witcherDialogManager.SecondHeroImageName = "Witcher";
        m_dialogControl.RegisterDialogManager(m_witcherDialogManager);
        m_playerReached = true;

        var scale = m_player.transform.localScale;
        scale.x = Mathf.Abs(scale.x);
        m_player.transform.localScale = scale;

    }

    private void DestinationReached(object sender, DestinationReachedEventArgs e)
    {
        e.Destination.Invoke();
    }

    private void PlayerDestinationChanged(object sender, EventArgs e)
    {
    }


    #endregion //Methods
}
