using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Hud : MonoBehaviour {

    #region Fields
    public Clock m_clock;
    public GameObject m_marksPanel;
    public Display m_money;
    public Display m_respect;
    public Animator m_inventoryAnimator;
    public Animator m_addToInventoryAnimator;
    public Image m_addToInventoryImage;
    public GameObject m_ingredientBook;
    public GameObject m_potionBook;
    public GameObject m_personsBook;
    public GameObject m_achiveBook;
    public GameObject m_contextMenu;
    public GameObject m_dailyTasks;
    public GameObject m_tasks;
    public GameObject m_showTasksButton;
    public GameObject m_settingsButton;
    public GameObject m_inventoryButton;
    public GameObject m_mark1;
    public GameObject m_mark2;
    public GameObject m_mark3;
    public GameObject m_mark4;
    public ArrowController m_arrowcontroller;
    public GameObject m_Map;
    public GameObject m_brick;
   
    private GameTime m_gameTime;
    private PlayerInfo m_playerInfo;
    private GuideManager m_guideManager;

    private bool m_showClock = true;
    private bool m_showMarks = true;
    private bool m_showMoney = true;
    private bool m_showRespect = true;
    #endregion //Fields

    #region Properties

    public void ReloadAchive()
    {
        m_achiveBook.GetComponent<AchiveFiller>().Refill();
    }
    public void hudDisable()
    {
        GetComponent<Canvas>().enabled = false;
    }
    public void hudEnable()
    {
        GetComponent<Canvas>().enabled = true;
    }

    public GuideManager GuideManager
    {
        get { return m_guideManager; }
    }

    public bool ShowClock
    {
        get { return m_showClock; }
        set
        {
            m_showClock = value;
            m_clock.gameObject.SetActive(m_showClock);
        }
    }

    public bool ShowBookmarks
    {
        get { return m_showMarks; }
        set
        {
            m_showMarks = value;
            if (m_marksPanel != null)
            {
                m_marksPanel.SetActive(m_showMarks);
            }
        }
    }

    public bool ShowMoney
    {
        get { return m_showMoney; }
        set
        {
            m_showMoney = value;
            if (m_showMoney)
            {
                m_money.gameObject.SetActive(m_showMoney);
            }
        }
    }

    public bool ShowRespect
    {
        get { return m_showRespect; }
        set
        {
            m_showRespect = value;
            if (m_showRespect)
            {
                m_respect.gameObject.SetActive(m_showRespect);
            }
        }
    }
    #endregion //Properties

    #region Messages
    // Use this for initialization
    private void Awake()
    {
        m_guideManager = FindObjectOfType<GuideManager>();
        m_gameTime = FindObjectOfType<GameTime>();
        m_playerInfo = Global.PlayerInfo;
        if (m_clock != null)
        {

            m_clock.m_dayDuration = m_gameTime.m_dayDuration;

            var task = new DailyTask("", 0, true, true);
            Global.PlayerInfo.NewDailyTask(task);
            Global.DailyGoldTaskSetter.SetGoldFirst(task);
            m_gameTime.TimeOfDayChanged += DailyTaskPresentor;
        }
    }

  
    void Start () {

	}

    private void OnValidate()
    {
        if (m_clock == null)
        {
            m_clock = transform.Find("clock").GetComponent<Clock>();
        }

        if (m_marksPanel == null)
        {
            m_marksPanel = transform.Find("BookmarksPanel").gameObject;
        }
    }

    // Update is called once per frame
    void Update () {
        UpdateClock();
        UpdatePlayeInfo();
	}
    #endregion //Messages

    #region Methods
    private void DailyTaskPresentor(object sender, DayChangedEventArgs e)
    {
        if (e.CurrentTimeOfDay == TimeOfDay.Night)
        {
            Global.DailyGoldTaskSetter.CheckGoldTask();
            Global.PlayerInfo.m_money -= Global.DailyGoldTaskSetter.GoldGoal;
        }
        if (e.CurrentTimeOfDay == TimeOfDay.Day)
            Global.DailyGoldTaskSetter.UpdateTask();
        m_dailyTasks.gameObject.SetActive(true);
        m_dailyTasks.GetComponent<CheckDailyTasks>().Check();
       


    }

    public void DailyTasksActivator()
    {
        if (!m_dailyTasks.activeSelf)
        {
            m_dailyTasks.SetActive(true);
            m_dailyTasks.GetComponent<CheckDailyTasks>().Refill();
        }
        else
            m_dailyTasks.SetActive(false);
    }

    public void MenuActivate()
    {
        if (!m_contextMenu.activeSelf)
            m_contextMenu.SetActive(true);
        else
            m_contextMenu.SetActive(false);
    }

    public void Mark1Click()
    {
        m_potionBook.SetActive(false);
        m_personsBook.SetActive(false);
        m_achiveBook.SetActive(false);
        if (!m_ingredientBook.activeSelf)
            m_ingredientBook.SetActive(true);
        else
            m_ingredientBook.SetActive(false);

    }
    public void Mark2Click()
    {
        m_personsBook.SetActive(false);
        m_ingredientBook.SetActive(false);
        m_achiveBook.SetActive(false);
        if (!m_potionBook.activeSelf)
            m_potionBook.SetActive(true);
        else
            m_potionBook.SetActive(false);

        //var guideManager = FindObjectOfType<GuideManager>();
        //GuideDialogue dialogue = new GuideDialogue();
        //dialogue.AddSentence("Your time has come!");
        //dialogue.AddSentence("Let's start the journey");
        //guideManager.StartDialouge(dialogue);
    }

    public void Mark3Click()
    {
        m_potionBook.SetActive(false);
        m_ingredientBook.SetActive(false);
        m_achiveBook.SetActive(false);
        if (!m_personsBook.activeSelf)
            m_personsBook.SetActive(true);
        else
            m_personsBook.SetActive(false);

    }

    public void Mark4Click()
    {
        m_potionBook.SetActive(false);
        m_ingredientBook.SetActive(false);
        m_personsBook.SetActive(false);
        if (!m_achiveBook.activeSelf)
            m_achiveBook.SetActive(true);
        else
            m_achiveBook.SetActive(false);

    }

    public void OpenMap()
    {
        m_Map.SetActive(true);
    }

    public void ShowInventory()
    {
        bool b = m_inventoryAnimator.GetBool("up");
        m_inventoryAnimator.SetBool("up", !b);
    }

    public void AddToInventoryAnim(int itemId)
    {
        m_addToInventoryImage.sprite = Global.DataBase.GetSpriteById(itemId);
        m_addToInventoryAnimator.SetTrigger("Take");
      
    }
    public void AddToInventoryAnim(Sprite sprite)
    {
        m_addToInventoryImage.sprite = sprite;
        m_addToInventoryAnimator.SetTrigger("Take");

    }
    public void ShowInventory(bool show)
    {
        m_inventoryAnimator.SetBool("up", !show);
    }

    public void RefillInventory()
    {
       GameObject.Find("ItemsPanel").GetComponent<MainInventoryFill>().Refilled();
    }

    private void UpdateClock()
    {
        if (m_clock != null && m_showClock)
        {
            m_clock.DayTimeProgress = m_gameTime.DayTimeProgress;
            m_clock.TimeOfDay = m_gameTime.TimeOfDay;
        }
    }

    private void UpdatePlayeInfo()
    {
        m_money.Value = m_playerInfo.m_money.ToString();
        m_respect.Value = m_playerInfo.m_respect.ToString();
    }

    #endregion //Methods
}
