using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTasks : MonoBehaviour
{

    public List<GameObject> prefabs;
    public GameObject Grid;
    private int index = 0;
    private int TasksCount = 0;
    private float delay = 1;
    private float delayBuffer;
    // Use this for initialization
    void Start()
    {
        delayBuffer = 0;
    
    }


    // Update is called once per frame
    void Update()
    {
        if (TasksCount != Global.PlayerInfo.m_playerTasks.CurrentTasksCount)
        {
            ReFill();
            delayBuffer = delay;
        }
        else
        {
            delayBuffer -= Time.deltaTime;
            if (delayBuffer <= 0)
            {
                for (int i = 0; i < Grid.transform.childCount; i++)
                {
                    string TimeLeftStr;
                    double TimeLeft = Grid.transform.GetChild(i).GetComponent<TaskStartTime>().FinishTime - Global.Time.TimeElapsed;
                    if (TimeLeft < 0 || (int)((TimeLeft / Global.Time.m_dayDuration) * 24) == 0)
                        TimeLeftStr = "Уже!";
                    else if (TimeLeft >= Global.Time.m_dayDuration)
                    {
                        TimeLeftStr = ((int)(TimeLeft / Global.Time.m_dayDuration)).ToString() + " Дней";
                    }
                    else
                    {
                        TimeLeftStr = ((int)((TimeLeft / Global.Time.m_dayDuration) * 24)).ToString() + " Чаc.";

                    }
                    var TaskUI = Grid.transform.GetChild(i).GetComponent<TaskTitleAnddTimeSetter>();
                    TaskUI.SetTitleAndTask(TaskUI.title.text, TimeLeftStr);
                }
                delayBuffer = delay;
            }
        }
    }
    public void ReFill()
    {
     

        if (TasksCount > Global.PlayerInfo.m_playerTasks.CurrentTasksCount)
        {
            for (int i = 0; i < Grid.transform.childCount; i++)
            {
                bool Contain = false;
                foreach (PlayerTask task in Global.PlayerInfo.m_playerTasks.CurrentTasks)
                {
                    if (task.Title == Grid.transform.GetChild(i).GetComponent<TaskTitleAnddTimeSetter>().title.text)
                        Contain = true;
                }
                if (!Contain)
                {
                    Destroy(Grid.transform.GetChild(i).gameObject);
                }
            }
            TasksCount = Global.PlayerInfo.m_playerTasks.CurrentTasksCount;
            return;
        }
        else
        {
            foreach (PlayerTask task in Global.PlayerInfo.m_playerTasks.CurrentTasks)
            {
                bool Contain = false;
                for (int i = 0; i < Grid.transform.childCount; i++)
                {
                    if (task.Title == Grid.transform.GetChild(i).GetComponent<TaskTitleAnddTimeSetter>().title.text)
                    {
                        Contain = true;
                        break;
                    }
                }
                if (!Contain)
                {
                    var copy = Instantiate(prefabs[index]);
                    ++index;
                    index = index < prefabs.Count ? index : 0;

                    string TimeLeftStr;

                    if (task.ExpirationTime < 0.042)
                        TimeLeftStr = "Уже!";
                    else if (task.ExpirationTime >= 1)
                    {
                        TimeLeftStr = ((int)(task.ExpirationTime)).ToString() + " Дней";
                    }
                    else
                    {
                        TimeLeftStr = ((int)(task.ExpirationTime * 24)).ToString() + " Чаc.";

                    }
                    copy.GetComponent<TaskTitleAnddTimeSetter>().SetTitleAndTask(task.Title, TimeLeftStr);
                    copy.GetComponent<TaskStartTime>().StartTime = Global.Time.TimeElapsed;
                    copy.GetComponent<TaskStartTime>().TaskDuration = task.ExpirationTime * Global.Time.m_dayDuration;

                    copy.GetComponent<CreateTolTip>().task = task;
                    copy.transform.SetParent(Grid.transform, false);
                    copy.transform.localScale = new Vector3(1, 1, 1);
                }
            }

        }
        TasksCount = Global.PlayerInfo.m_playerTasks.CurrentTasksCount;
    }

    public void FillTasks()
    {
        for (int i = 0; i < Grid.transform.childCount; i++)
        {
            Destroy(Grid.transform.GetChild(i).gameObject);
        }

        foreach (PlayerTask task in Global.PlayerInfo.m_playerTasks.CurrentTasks)
        {
            var copy = Instantiate(prefabs[index]);
            ++index;
            index = index < prefabs.Count ? index : 0;

            string TimeLeftStr;

            if (task.ExpirationTime < 0 || (int)((task.ExpirationTime / Global.Time.m_dayDuration) * 24) == 0)
                TimeLeftStr = "Уже!";
            else if (task.ExpirationTime >= Global.Time.m_dayDuration)
            {
                TimeLeftStr = ((int)(task.ExpirationTime / Global.Time.m_dayDuration)).ToString() + " Дней";
            }
            else
            {
                TimeLeftStr = ((int)((task.ExpirationTime / Global.Time.m_dayDuration) * 24)).ToString() + " Чаc.";

            }
            copy.GetComponent<TaskTitleAnddTimeSetter>().SetTitleAndTask(task.Title, TimeLeftStr);
            copy.GetComponent<TaskStartTime>().StartTime = Global.Time.TimeElapsed;
            copy.GetComponent<TaskStartTime>().TaskDuration = task.ExpirationTime;


            copy.GetComponent<CreateTolTip>().task = task;
            copy.transform.SetParent(Grid.transform, false);
            copy.transform.localScale = new Vector3(1, 1, 1);
        }
        TasksCount = Global.PlayerInfo.m_playerTasks.CurrentTasksCount;
    }
}
