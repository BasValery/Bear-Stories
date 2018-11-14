using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckDailyTasks : MonoBehaviour {

    public GameObject GridPanel;
    public GameObject TaskPrefab;
    public bool TaskChanger = false;
	// Use this for initialization
	void Start () {
        Check();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Return) )
            gameObject.SetActive(false);
        if (TaskChanger)
        {
            Check();
            TaskChanger = false;
        }
	}
    public void Refill()
    {
        for (int i = 0; i < GridPanel.transform.childCount; i++)
        {
            Destroy(GridPanel.transform.GetChild(i).gameObject);
        }

        foreach (DailyTask task in Global.PlayerInfo.DailyTasks)
        {
            var copy = Instantiate(TaskPrefab);
            copy.GetComponent<Text>().text = task.Title;
            copy.transform.SetParent(GridPanel.transform, false);
            if (!task.Complete)
            {
                copy.transform.Find("DoneStick").gameObject.SetActive(false);
            }
            if(!task.MoneyTask)
            {
                copy.transform.Find("Money").gameObject.SetActive(false);
            }
            if (!task.Important)
            {
                copy.transform.Find("MainGoal").gameObject.SetActive(false);
            
            }else
                copy.transform.SetAsFirstSibling();
        }
    }
    public void Check()
    {
        for(int i = 0; i < GridPanel.transform.childCount; i++)
        {
            Destroy(GridPanel.transform.GetChild(i).gameObject);
        }

        foreach (DailyTask task in Global.PlayerInfo.DailyTasks)
        {
            var copy = Instantiate(TaskPrefab);
            copy.GetComponent<Text>().text = task.Title;
            copy.transform.SetParent(GridPanel.transform, false);
            if (!task.Complete)
            {
                copy.transform.Find("DoneStick").gameObject.SetActive(false);
            }
            if (!task.MoneyTask)
            {
                copy.transform.Find("Money").gameObject.SetActive(false);
            }
            if (!task.Important)
            {
                copy.transform.Find("MainGoal").gameObject.SetActive(false);

            }
            else
                copy.transform.SetAsFirstSibling();
            if (task.DeleteOnComplete)
            {
                Global.PlayerInfo.RemoveDailyTask(task);
            }
        }


    }
}
