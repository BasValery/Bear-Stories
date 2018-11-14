using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PlayerTasks : MonoBehaviour
{
    #region Fields
    private List<PlayerTask> m_currentTasks = new List<PlayerTask>();
    private List<PlayerTask> m_archiveSucceed = new List<PlayerTask>();
    private List<PlayerTask> m_archiveFailed = new List<PlayerTask>();
    #endregion //Fields

    #region Properties
    public IEnumerable<PlayerTask> CompletedTasksToday
    {
        get
        {
            var result = new List<PlayerTask>();
            for (int i = m_archiveSucceed.Count - 1; i >= 0; i--)
            {
                var task = m_archiveSucceed[i];
                if (Global.Time.GetDay(task.ExpirationTime) == Global.Time.CurrentDay)
                {
                    result.Add(task);
                }
            }
            return result;
        }
    }

    public IEnumerable<PlayerTask> FailedTasksToday
    {
        get
        {
            var result = new List<PlayerTask>();
            for (int i = m_archiveFailed.Count - 1; i >= 0; i--)
            {
                var task = m_archiveFailed[i];
                if (Global.Time.GetDay(task.ExpirationTime) == Global.Time.CurrentDay)
                {
                    result.Add(task);
                }
            }
            return result;
        }
    }

    public IEnumerable<PlayerTask> CurrentTasks
    {
        get
        {
            return m_currentTasks;
        }
    }

    public int CompletedTasksCount
    {
        get { return m_archiveFailed.Count + m_archiveSucceed.Count; }
    }

    public int TotalTasksCount
    {
        get { return m_currentTasks.Count + m_archiveFailed.Count + m_archiveSucceed.Count; }
    }

    public int CurrentTasksCount
    {
        get
        {
            return m_currentTasks.Count;
        }
    }
    #endregion //Properties

    #region Messages
    private void Awake()
    {

    }
    #endregion //Messages

    #region Methods
    public bool TaskSucceeded(BuyerTask task)
    {
        PlayerTask playerTask = m_currentTasks.FirstOrDefault(x => x.ItemId == task.ItemId);
        if (playerTask == null)
        {
            return false;
        }
        m_currentTasks.Remove(playerTask);
        m_archiveSucceed.Add(playerTask);
        return true;

    }

    public bool TaskFailed(BuyerTask task)
    {
        PlayerTask playerTask = m_currentTasks.FirstOrDefault(x => x.ItemId == task.ItemId);
        if (playerTask == null)
        {
            return false;
        }
        m_currentTasks.Remove(playerTask);
        m_archiveFailed.Add(playerTask);
        return true;
    }

    public void AddTask(BuyerTask task)
    {
        m_currentTasks.Add(
            new PlayerTask(
                task.Story,
                task.Defenition,
                task.Title,
                task.TargetReachedTime + task.ManufacturingTime,
                task.ItemId,
                task.Reward,
                task.Respect
            )
        );
    }


    public int FailedTasksCount
    {
        get { return m_archiveFailed.Count; }
    }


    public int SucceedTasksCount
    {
        get { return m_archiveSucceed.Count; }
    }
    #endregion //Methods
}
