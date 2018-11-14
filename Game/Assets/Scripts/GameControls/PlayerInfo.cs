using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PlayerTasks))]
public class PlayerInfo : MonoBehaviour {
    #region Fields
    public int m_money;
    public int m_respect;
    public int m_lives;
    public float m_speed;
    public bool m_movementLocked = false;
    public PlayerTasks m_playerTasks;
    private List<DailyTask> m_dailyTaskList = new List<DailyTask>();
    private List<DailyTask> m_completedTasks = new List<DailyTask>();

    private Dictionary<string, float> m_storage = new Dictionary<string, float>();
    private List<string> m_achivements = new List<string>();
    private List<string> m_sceneLoaded = new List<string>();

    #endregion //Fields

    #region Properties
    public Dictionary<string, float> Storage
    {
        get { return m_storage; }
    }

    public IEnumerable<DailyTask> DailyTasks
    {
        get { return m_dailyTaskList; }
    }

    public bool DailyTaskCompleted(int id)
    {
        var task = m_dailyTaskList.FirstOrDefault(x => x.Id == id);
        if (task != null)
        {
            task.Completed();
            m_dailyTaskList.Remove(task);
            m_completedTasks.Add(task);
            return true;
        }
        return false;
    }

    public bool IsTaskCompleted(DailyTask task)
    {
        return m_completedTasks.Contains(task);
    }

    public bool IsTaskCompleted(int id)
    {
        return m_completedTasks.Any(x => x.Id == id);
    }

    public bool NewDailyTask(DailyTask task)
    {
        if (m_dailyTaskList.Contains(task))
        {
            return false;
        }
        m_dailyTaskList.Add(task);
        return true;
    }

    public bool RemoveDailyTask(DailyTask task)
    {
        return m_dailyTaskList.Remove(task);
    }
        

    public List<string> SceneLoaded
    {
        get { return m_sceneLoaded; }
    }
    #endregion //Properties

    #region Messages
    // Use this for initialization
    void Start() {

    }

    private void Awake()
    {
        m_playerTasks = GetComponent<PlayerTasks>();
        m_storage.Add("LastPotion", 5);
        m_storage.Add("LastPotionTemp", 0);

        InitTasks();

       
    }

    // Update is called once per frame
    void Update() {
        m_money = m_money < 0 ? 0 : m_money;
    }
    #endregion //Messages

    #region Methods
    public bool CheckAchivement(string name)
    {
        return m_achivements.Contains(name);
    }

    public void Achivement(string name)
    {
        m_achivements.Add(name);
    }

    public bool SceneLoadedFirst(string scene)
    {
        if (m_sceneLoaded.Contains(scene))
            return false;
        m_sceneLoaded.Add(scene);
        return true;
    }

    public void InitTasks()
    {
        var taskTransferGnomePotion = new DailyTask("Отдать зелье лысому", 4, false, false, false, () =>
        {

        });
        
        var taskCookPotionForGnome = new DailyTask("Добыть зелье для лысого", 5, false, false, false, () =>
        {
            NewDailyTask(taskTransferGnomePotion);
        });
        var taskAskForHelp = new DailyTask("Попросить помощи у лесника", 3,false, false, false, () =>
        {

        });
        var taskTalkToGnome = new DailyTask("Поговорить с гномом", 2, false, false, false, () =>
        {
            NewDailyTask(taskCookPotionForGnome);
            NewDailyTask(taskAskForHelp);
        });
        var taskCompleteOrder = new DailyTask("Выполнить заказ для жителя", 1, false, false, false, () =>
        {

        });
        var taskTakeOrder = new DailyTask("Принять заказ у жителя", 6, false, false, false, () =>
        {
            NewDailyTask(taskCompleteOrder);
        });

        NewDailyTask(taskTakeOrder);
        NewDailyTask(taskTalkToGnome);
    }
    #endregion
}
