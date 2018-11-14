using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonFiller : MonoBehaviour {

    // Use this for initialization

    public GameObject ContentPanel;
    public GameObject ItemPrefab;
    public GameObject NoSlotPrefab;

    void Start () {
        Fill();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Fill()
    {
        var persons = Global.DataBase.getPersons();
        int count = 0;
        foreach (Persons person in persons)
        {
            if (person.IsOpen)
            {
                var copy = Instantiate(ItemPrefab);
                copy.GetComponent<SetImgAndText>().Set(person.Title, person.getSprite());
                copy.GetComponent<SetInfoPanelPerson>().Book = Global.Hud.m_personsBook.GetComponent<GetInfoPanel>();
                copy.GetComponent<CurrentPerson>().person = person;
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
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        var persons = Global.DataBase.getPersons();
        int count = 0;
        foreach (Persons person in persons)
        {

            if (person.IsOpen)
            {
                var copy = Instantiate(ItemPrefab);
                copy.GetComponent<SetImgAndText>().Set(person.Title, person.getSprite());
                copy.GetComponent<SetInfoPanel>().Book = Global.Hud.m_personsBook.GetComponent<GetInfoPanel>();
                copy.GetComponent<CurrentPerson>().person = person;
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
