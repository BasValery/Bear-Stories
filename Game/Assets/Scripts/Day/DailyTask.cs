using System;
using System.Collections;
using System.Collections.Generic;

public class DailyTask {

    public Action m_completedAction;
    public bool Complete { get; set; }
    public bool Important { get; set; }
    public int Id { get; set; }
    public string Title { get;  set; }
    public bool DeleteOnComplete { get; private set; }
    public bool MoneyTask { get; private set; }

    public DailyTask(string title, int id, bool important,bool moneyTask, bool delete = false, Action completed = null)
    {
        Complete = false;
        Title = title;
        Id = id;
        DeleteOnComplete = delete;
        m_completedAction = completed;
        MoneyTask = moneyTask;
        this.Important = important;
    }
   
    public void Completed()
    {
        if (Complete == false)
        {
            Complete = true;
            if (m_completedAction != null)
            {
                m_completedAction.Invoke();
            }
        }
    }
}
