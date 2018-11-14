using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyGoldTaskSetter : MonoBehaviour {

    public int GoldGoal { get; private set; }
    private DailyTask m_DailyTask;
    private int day = 0;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       

    }
    public void CheckGoldTask()
    {
        if (m_DailyTask != null)
        {
            m_DailyTask.Complete = Global.PlayerInfo.m_money >= GoldGoal;
           
        }
    }
    public void SetGoldFirst(DailyTask playerDailyTask)
    {
        GoldGoal = (day + 1) * 20;
        playerDailyTask.Title = "Заработать " + GoldGoal.ToString();
        m_DailyTask = playerDailyTask;
    }

    public void UpdateTask()
    {
        ++day;
        GoldGoal = (day + 1) * 20;
        m_DailyTask.Title = "Заработать " + GoldGoal.ToString();
        m_DailyTask.Complete = false;
    }
}
