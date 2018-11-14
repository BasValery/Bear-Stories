using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchiveFiller : MonoBehaviour {

    public GameObject ContentPanel;
    public GameObject ItemPrefab;
    public GameObject NoSlotPrefab;

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
        for (int i = 0; i < ContentPanel.transform.childCount; i++)
        {
            Destroy(ContentPanel.transform.GetChild(i).gameObject);
        }

        var achives = Global.DataBase.getAchives();
        int count = 0;
        foreach (Achive achive in achives)
        {
            if (achive.IsOpen)
            {
                var copy = Instantiate(ItemPrefab);
                copy.GetComponent<SetImgAndText>().Set(achive.Title, achive.getSprite());
                copy.GetComponent<SetInfoPanelAchive>().Book = Global.Hud.m_achiveBook.GetComponent<GetInfoPanel>();
                copy.GetComponent<CurrentAchive>().achive = achive;
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

    public void Refill()
    {
        for (int i = 0; i < ContentPanel.transform.childCount; i++)
        {
            Destroy(ContentPanel.transform.GetChild(i).gameObject);
        }

        var achives = Global.DataBase.getAchives();
        int count = 0;
        foreach (Achive achive in achives)
        {
            if (achive.IsOpen)
            {
                var copy = Instantiate(ItemPrefab);
                copy.GetComponent<SetImgAndText>().Set(achive.Title, achive.getSprite());
                copy.GetComponent<SetInfoPanelAchive>().Book = Global.Hud.m_achiveBook.GetComponent<GetInfoPanel>();
                copy.GetComponent<CurrentAchive>().achive = achive;
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
