using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionFiller : MonoBehaviour {

    public GameObject ContentPanel;
    public GameObject ItemPrefab;
    public GameObject NoSlotPrefab;
    // Use this for initialization
    void Start()
    {
        Fill();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Fill()
    {
        var items = Global.DataBase.ItemsList;
        int count = 0;
        foreach (Item item in items)
        {
            if (item.Potion)
            {
                var copy = Instantiate(ItemPrefab);
                copy.GetComponent<SetImgAndText>().Set(item.Title, item.getSprite());
                copy.GetComponent<SetInfoPanel>().Book = Global.Hud.m_potionBook.GetComponent<GetInfoPanel>();
                copy.GetComponent<CurrentItem>().Item = item;
                copy.transform.SetParent(ContentPanel.transform, false);
                ++count;
            }
        }
        for (int i = 0; i < 20 - count; i++)
        {
            var copy = Instantiate(NoSlotPrefab);
          
            copy.transform.SetParent(ContentPanel.transform, false);
        }
    }
}
