using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MovementGuider))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(InfoCloudControl))]
[RequireComponent(typeof(OrderLayerByY))]
[RequireComponent(typeof(NotAffectedByGravity))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Buyer : MonoBehaviour
{

    #region Fields
    protected MovementGuider m_movementGuider;
    protected InfoCloudControl m_infoControl;
    public string m_name;
    private BuyerTask m_currentTask;
    private Vector3 m_initialPos;
    private Vector3 m_targetPos;
    private bool m_queue = false;
    private Animator m_animator;
    private AudioSource m_audioSource;
    #endregion //Fields

    #region Events
    public event EventHandler<BuyerEventArgs> Free;
    public event EventHandler<BuyerEventArgs> Busy;
    public event EventHandler<BuyerEventArgs> Enqueued;
    public event EventHandler<BuyerEventArgs> Dequeued;
    #endregion //Events

    #region Properties
    public Vector3 TargetPos
    {
        get { return m_targetPos; }
    }

    public BuyerState State
    {
        get { return m_currentTask.State; }
    }
    #endregion //Properties

    #region Messages
    // Use this for initialization
    void Start()
    {

    }


    private void Awake()
    {
        m_movementGuider = GetComponent<MovementGuider>();
        m_animator = GetComponent<Animator>();
        m_audioSource = GetComponent<AudioSource>();
        m_infoControl = GetComponent<InfoCloudControl>();
        m_movementGuider.DestinationReached += OnMovementGuiderTargetReached;
        m_initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_currentTask != null)
        {
            if(Input.GetKeyDown(KeyCode.F2))
            {
                m_currentTask.Done();
            }
            m_currentTask.Step(Time.deltaTime);
        }
    }
    #endregion //Messages

    #region Methods
    public void SetTask(BuyerTask task, Vector3 targetPos, bool queue = false)
    {
        if (task == null)
        {
            Debug.LogWarning("task is null");
        }
        m_infoControl.Show(false);
        m_targetPos = targetPos;
        m_queue = queue;
        if (m_currentTask != null)
        {
            m_currentTask.Expired -= Task_Expired;
            m_currentTask.TakeOffTime -= Task_TakeOffTime;
        }
        m_currentTask = (BuyerTask)task.Clone();
        if (Busy != null)
        {
            Busy.Invoke(this, new BuyerEventArgs(this));
        }
        m_currentTask.Expired += Task_Expired;
        m_currentTask.TakeOffTime += Task_TakeOffTime;
        if (m_movementGuider != null)
        {
            m_movementGuider.Destination = new Destination(targetPos, OnPositionReached, null);
            m_currentTask.State = BuyerState.GoingPlacingOrder;
        }
    }


    public void TaregtPosChanged(Vector3 pos, bool queue = false)
    {
        if (m_currentTask.State == BuyerState.InQueue || m_currentTask.State == BuyerState.GoingPlacingOrder)
        {
            m_targetPos = pos;
            m_queue = queue;
            m_currentTask.State = BuyerState.GoingPlacingOrder;
            m_movementGuider.Destination = new Destination(m_targetPos, OnPositionReached, null);
        }
    }

    public void Accepted()
    {
        Global.PlayerInfo.DailyTaskCompleted(6);
        if (m_currentTask != null)
        {
            m_currentTask.State = BuyerState.ReturningPlacingOrder;
            m_movementGuider.Destination = new Destination(m_initialPos, OnInitialPosReached, null);
            Global.PlayerInfo.m_playerTasks.AddTask(m_currentTask);
            m_infoControl.Show(false);
            if (Dequeued != null)
            {
                Dequeued.Invoke(this, new BuyerEventArgs(this));
            }
            m_currentTask.TargetReached();
        }

    }


    public void Declined()
    {
        if (m_currentTask != null)
        {
            m_currentTask.State = BuyerState.ReturnTakingOff;
            Global.PlayerInfo.m_respect -= m_currentTask.Respect / 2;
            m_movementGuider.Destination = new Destination(m_initialPos, OnInitialPosReached, null);
            m_infoControl.Show(false);
            m_currentTask.Over();
            if (Dequeued != null)
            {
                Dequeued.Invoke(this, new BuyerEventArgs(this));
            }
        }
    }

    private void Task_TakeOffTime(object sender, EventArgs e)
    {
        m_currentTask.State = BuyerState.GoingTakingOff;
        m_movementGuider.Destination = new Destination(m_targetPos, OnPositionReached, null);
    }

    private void OnInitialPosReached()
    {
        switch (m_currentTask.State)
        {
            case BuyerState.ReturningPlacingOrder:
                m_currentTask.State = BuyerState.Waiting;
                break;
            case BuyerState.ReturnTakingOff:
                m_currentTask.State = BuyerState.Finished;
                m_currentTask.Expired -= Task_Expired;
                m_currentTask.TakeOffTime -= Task_TakeOffTime;
                m_animator.SetBool("desperate", false);
                m_audioSource.Stop();
                m_movementGuider.m_speedMultiplier = 1f;
                if (Free != null)
                {
                    Free.Invoke(this, new BuyerEventArgs(this));
                }
                break;
        }
    }

    private void OnPositionReached()
    {
        switch (m_currentTask.State)
        {
            case BuyerState.GoingTakingOff:
                m_currentTask.State = BuyerState.ReturnTakingOff;
                m_movementGuider.Destination = new Destination(m_initialPos, OnInitialPosReached, null);
                var targetItem = Global.DataBase.getFromInventoryById(m_currentTask.ItemId);
                if (targetItem.Id == -1)
                {
                    Global.PlayerInfo.m_respect -= (int)(m_currentTask.Respect * 0.75f);
                    Global.PlayerInfo.m_playerTasks.TaskFailed(m_currentTask);
                    m_animator.SetBool("desperate", true);
                    m_movementGuider.m_speedMultiplier = 0.5f;
                    m_audioSource.Play();
                }
                else
                {
                    Global.PlayerInfo.DailyTaskCompleted(1);
                    Global.PlayerInfo.m_playerTasks.TaskSucceeded(m_currentTask);
                    Global.DataBase.deleteFromInventory(m_currentTask.ItemId);
                    Global.PlayerInfo.m_respect += m_currentTask.Respect;
                    Global.PlayerInfo.m_money += (int)((float)m_currentTask.Reward * (float)targetItem.Quality / 100f); ;
                    Global.PlayerInfo.DailyTaskCompleted(1);
                }
                break;
            case BuyerState.GoingPlacingOrder:
                m_currentTask.State = BuyerState.InQueue;
                if (m_queue == false)
                {
                    m_infoControl.DisplayText(m_currentTask.Story);
                    m_infoControl.Show(true);
                }
                if (Enqueued != null)
                {
                    Enqueued.Invoke(this, new BuyerEventArgs(this));
                }
                break;
        }
    }

    private void OnMovementGuiderTargetReached(object sender, DestinationReachedEventArgs e)
    {
        e.Destination.Invoke();
    }

    private void Task_Expired(object sender, EventArgs e)
    {
        if (m_currentTask != null)
        {
            m_currentTask.State = BuyerState.ReturnTakingOff;
            Global.PlayerInfo.m_respect -= (int)(m_currentTask.Respect * 0.75f);
            m_movementGuider.Destination = new Destination(m_initialPos, OnInitialPosReached, null);
            m_infoControl.Show(false);
            m_currentTask.Over();
            if (Dequeued != null)
            {
                Dequeued.Invoke(this, new BuyerEventArgs(this));
            }
        }
    }
    #endregion //Methods
}
