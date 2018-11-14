using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class BuyersController : MonoBehaviour
{
    #region Fields
    public Vector3 destPos;

    private Stack<Buyer> m_freeNPC = new Stack<Buyer>();
    private List<Buyer> m_busyNPC = new List<Buyer>();
    private Queue<BuyerTask> m_taskQueue = new Queue<BuyerTask>();
    private List<Buyer> m_queue = new List<Buyer>();
    #endregion //Fields

    #region Properties
    public int FreeQuanity
    {
        get { return m_freeNPC.Count; }
    }
    #endregion //Properties

    #region Messages
    private void Awake()
    {
        destPos  = new Vector3(17.1f, -11.74f, 26.56727f);
        m_freeNPC = new Stack<Buyer>(GetComponentsInChildren<Buyer>());
        foreach (Buyer npc in m_freeNPC)
        {
            npc.Enqueued += Npc_Enqueued;
            npc.Dequeued += Npc_Dequeued;
            npc.Busy += Npc_Busy;
            npc.Free += Npc_Free;
        }
    }

    private void Start()
    {
        Global.StoryLine.m_npcSystem = this;
    }

    private void Update()
    {
        
    }
    #endregion //Messages

    #region Methods
    public void NewOrder(BuyerTask task)
    {
        if (m_freeNPC.Count > 0)
        {
            var npc = m_freeNPC.Pop();
            var pos = destPos;
            pos.x += m_busyNPC.Count;
            npc.SetTask(task, pos, m_queue.Count != 0);
            m_busyNPC.Add(npc);
        }
        else
        {
            m_taskQueue.Enqueue(task);
        }
    }

    private void Npc_Enqueued(object sender, BuyerEventArgs e)
    {
        m_queue.Add(e.NPC);
        //int index = m_queue.FindIndex(x => x == e.NPC);
        List<Buyer> placingOrder = m_busyNPC.Where(x => x.State == BuyerState.GoingPlacingOrder).ToList();
        var pos = e.NPC.TargetPos;
        pos.x += 2;
        for (int i = 0; i < placingOrder.Count; i++)
        {
            var npc = placingOrder[i];
            npc.TaregtPosChanged(pos, true);
        }
    }

    private void Npc_Dequeued(object sender, BuyerEventArgs e)
    {
        int index = m_queue.FindIndex(x => x == e.NPC);
        m_queue.FindIndex(x => x == e.NPC);
        Vector3 pos;
        if (index + 1 < m_queue.Count)
        {
            m_queue[index + 1].TaregtPosChanged(destPos, false);
            for (int i = index + 2; i < m_queue.Count; i++)
            {
                var npc = m_queue[i];
                pos = m_queue[i].TargetPos;
                pos.x -= 2;
                npc.TaregtPosChanged(pos, true);
            }
        }

        m_queue.Remove(e.NPC);

        List<Buyer> placingOrder = m_busyNPC.Where(x => x.State == BuyerState.GoingPlacingOrder).ToList();
        placingOrder.RemoveAll(x => m_queue.Contains(x));
        if (placingOrder.Count > 0)
        {
            pos = placingOrder.Last().TargetPos;
            pos.x -= 2;
            for (int i = 0; i < placingOrder.Count; i++)
            {
                var npc = placingOrder[i];
                npc.TaregtPosChanged(pos, m_queue.Count != 0);
            }
        }
    }

    private void Npc_Free(object sender, BuyerEventArgs e)
    {
        m_busyNPC.Remove(e.NPC);
        m_freeNPC.Push(e.NPC);
    }

    private void Npc_Busy(object sender, BuyerEventArgs e)
    {
    }
    #endregion //Methods
}
