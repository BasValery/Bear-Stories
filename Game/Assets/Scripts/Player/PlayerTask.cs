using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

public class PlayerTask
{
    #region Fields
    protected string story;
    protected string defenition;
    protected string title;
    protected double expirationTime;
    protected double completionTime;
    protected int itemId;
    protected int reward;
    protected int respect;
    #endregion //Fields

    #region Constructor
    public PlayerTask(string story, string defenition, string title,
        double expirationTime, int itemId, int reward, int respect)
    {
        Story = story;
        Defenition = defenition;
        Title = title;
        ExpirationTime = expirationTime;
        ItemId = itemId;
        Respect = respect;
    }

    private PlayerTask()
    {
    }

    /*
    public PlayerTask(JsonData json)
    {
        try
        {
            Story = (string)json["Story"];
            Defenition = (string)json["Defenition"];
            Title = (string)json["Title"];
            ExpirationTime = (double)json["ExpirationTime"];
            ItemId = (int)json["ItemId"];
            Reward = (int)json["Reward"];
            Respect = (int)json["Respect"];
        }
        catch
        {
            UnityEngine.Debug.Log("Failed to load player task");
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

    public string Defenition
    {
        get { return defenition; }
        private set { defenition = value; }
    }

    public string Title
    {
        get { return title; }
        private set { title = value; }
    }

    public double ExpirationTime
    {
        get { return expirationTime; }
        set { expirationTime = value; }
    }

    public int Reward
    {
        get { return reward; }
        private set { reward = value; }
    }

    public int Respect
    {
        get { return respect; }
        set { respect = value; }
    }

    public Item Item
    {
        get { return Global.DataBase.getById(itemId); }
    }

    public int ItemId
    {
        get { return itemId; }
        private set { itemId = value; }
    }

    public double CompletionTime
    {
        get { return completionTime; }
        set { completionTime = value; }
    }

    public bool IsExpired
    {
        get { return Global.Time.TimeElapsed < expirationTime; }
    }
    #endregion //Properties


    #region Methods
    public static JsonData ToJson(PlayerTask task)
    {
        return JsonMapper.ToJson(task);
    }

    public void Complete()
    {
        CompletionTime = Global.Time.TimeElapsed;
    }

    public static PlayerTask Fromjson(JsonData json)
    {
        var task = new PlayerTask();
        try
        {
            task.Story = (string)json["Story"];
            task.Defenition = (string)json["Defenition"];
            task.Title = (string)json["Title"];
            task.ExpirationTime = (double)json["ExpirationTime"];
            task.ItemId = (int)json["ItemId"];
            task.Reward = (int)json["Reward"];
            task.Respect = (int)json["Respect"];
            task.CompletionTime = (double)json["CompletionTime"];
        }
        catch
        {
            UnityEngine.Debug.Log("Failed to load player task");
            return null;
        }
        return task;
    }
    #endregion //Methods
}
