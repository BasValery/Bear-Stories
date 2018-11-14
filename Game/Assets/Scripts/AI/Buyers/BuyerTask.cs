using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;
using UnityEngine;

public enum BuyerState
{
    AFC = 0, GoingPlacingOrder, ReturningPlacingOrder,
    InQueue, Waiting, GoingTakingOff, ReturnTakingOff,
    Finished
}

public class BuyerTask : ICloneable
{
    #region Fields
    protected string story;
    protected string defenition;
    protected string title;
    protected int itemId;
    protected int reward;
    protected int respect;
    protected double maxWaitingTime;
    protected double executionTime;
    protected double waited;
    protected double targetReachedTime;
    protected double manufacturingTime;
    protected BuyerState state = BuyerState.AFC;
    #endregion //Fields

    #region Events
    public event EventHandler Expired;
    public event EventHandler TakeOffTime;
    #endregion //Events

    #region Constructors
    public BuyerTask(string story, string defenition, string title, int itemId, int reward,
        int respect, double maxWaitingTime, double executionTime, double manufacturingTime)
    {
        Story = story;
        Defenition = defenition;
        Title = title;
        ItemId = itemId;
        Reward = reward;
        Respect = respect;
        MaximumWaitingTime = maxWaitingTime;
        ExecutionTime = executionTime;
        ManufacturingTime = manufacturingTime;
    }

    private BuyerTask()
    {
    }
    
    /*
    public NPCTask(JsonData jsonData)
    {
        try
        {
            Story = (string)jsonData["Story"];
            Defenition = (string)jsonData["Sefenition"];
            Title = (string)jsonData["Title"];
            Reward = (int)jsonData["Reward"];
            Respect = (int)jsonData["Respect"];
            Waited = (double)jsonData["Waited"];
            MaximumWaitingTime = (double)jsonData["MaximumWaitingTime"];
            TargetReachedTime = (double)jsonData["TargetReachedTime"];
        }
        catch
        {
            UnityEngine.Debug.LogWarning("Failed to load NPC task");
        }
    }
    */
    #endregion //Constructors

    #region Properties
    public string Story
    {
        get { return story; }
        private set { story = value; }
    }

    public string Title
    {
        get { return title; }
        private set { title = value; }
    }

    public string Defenition
    {
        get { return defenition; }
        private set { defenition = value; }
    }

    public int Reward
    {
        get { return reward; }
        private set { reward = value; }
    }

    public int Respect
    {
        get { return respect; }
        private set { respect = value; }
    }

    public int ItemId
    {
        get { return itemId; }
        private set { itemId = value; }
    }

    public Item Item
    {
        get { return Global.DataBase.getById(itemId); }
    }

    public double MaximumWaitingTime
    {
        get { return maxWaitingTime; }
        set { maxWaitingTime = value; }
    }

    public double Waited
    {
        get { return waited; }
        private set { waited = value; }
    }

    public double TargetReachedTime
    {
        get { return targetReachedTime; }
        private set { targetReachedTime = value; }
    }

    public double ExecutionTime
    {
        get { return executionTime; }
        private set { executionTime = value; }
    }

    public double ManufacturingTime
    {
        get { return manufacturingTime; }
        private set { manufacturingTime = value; }
    }

    public BuyerState State
    {
        get { return state; }
        set { state = value; }
    }
    #endregion //Properties

    #region Methods
    
    public void Over()
    {
        waited = 0;
        targetReachedTime = 0;
        State = BuyerState.ReturnTakingOff;
    }

    public static JsonData ToJson(BuyerTask task)
    {
        return JsonMapper.ToJson(task);
    }
    
    public static BuyerTask FromJson(JsonData json, bool npcSave = false)
    {
        var task = new BuyerTask();
        try
        {
            task.Story = (string)json["Story"];
            task.Defenition = (string)json["Defenition"];
            task.Title = (string)json["Title"];
            task.Reward = (int)json["Reward"];
            task.Respect = (int)json["Respect"];
            task.ItemId = (int)json["ItemId"];
            task.ManufacturingTime = (double)json["ManufacturingTime"];
            task.MaximumWaitingTime = (double)json["MaximumWaitingTime"];

            if (npcSave == true)
            {
                task.Waited = (double)json["Waited"];
                task.ExecutionTime = (double)json["ExecutionTime"];
                task.TargetReachedTime = (double)json["TargetReachedTime"];
            }
        }
        catch
        {
            UnityEngine.Debug.LogWarning("Failed to load NPC task");
            return null;
        }
        return task;
    }
    public void Done()
    {
        if (TakeOffTime != null)
        {
            TakeOffTime.Invoke(this, new EventArgs());
        }
    }
    public void Step(float time)
    {
        switch(state)
        {
            case BuyerState.InQueue:
                Waited += time;
                if (Waited > maxWaitingTime)
                {
                    if (Expired != null)
                    {
                        Expired.Invoke(this, new EventArgs());
                    }
                }
                break;
            case BuyerState.Waiting:
                if (targetReachedTime + Global.Time.CalculateTime(manufacturingTime) < Global.Time.TimeElapsed)
                {
                    if (TakeOffTime != null)
                    {
                        TakeOffTime.Invoke(this, new EventArgs());
                    }
                }

                break;
        }
    }

    public void TargetReached()
    {
        if (targetReachedTime == 0)
        {
            targetReachedTime = Global.Time.TimeElapsed;
        }
    }

    public object Clone()
    {
        return new BuyerTask(
            story,
            defenition,
            title,
            itemId,
            reward,
            respect,
            maxWaitingTime,
            executionTime,
            manufacturingTime
        );
    }


    #endregion //Methods
}
