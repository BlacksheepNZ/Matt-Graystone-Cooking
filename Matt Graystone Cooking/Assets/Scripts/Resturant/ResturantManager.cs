using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResturantManager : MonoBehaviour {

    public Text Resturant_Count_Text;

    public int Current_Guest_Count;
    public int Max_Guest_Count;

    public int StartingCash;
    public bool Resturant_Open;

    public float GuestArivalRate;

    public void OpenResturant()
    {
        if(Resturant_Open == false)
        {
            Game.Instance.AddGold(StartingCash);
            StartCoroutine(UpdateResturant());
            Resturant_Open = true;
        }
    }

    public float CurrentTime = 0;
    public ProgressionBar ProgressionBar;
    private bool TimerStarted;

    public IEnumerator UpdateResturant()
    {
        if (Current_Guest_Count < Max_Guest_Count)
        {
            TimerStarted = true;

            float speed = (Time.fixedDeltaTime / GuestArivalRate);

            while (ProgressionBar.Value < 1)
            {
                ProgressionBar.Value += speed;
                CurrentTime = ProgressionBar.Value;

                yield return null;
            }

            ProgressionBar.Value = 0;
            ResetTimer();
        }
    }

    void ResetTimer()
    {
        AddPeople();
        AddGuest();
        TimerStarted = false;
        StartCoroutine(UpdateResturant());
    }

    public void AddGuest()
    {
        Current_Guest_Count++;
    }

    public void RemoveGuest(int count)
    {
        Current_Guest_Count -= count;
    }

    public int GuestCount()
    {
        return Current_Guest_Count;
    }

    // Use this for initialization
    void Start () {
        OpenResturant();
    }

    // Update is called once per frame
    void Update()
    {
        if (Current_Guest_Count < Max_Guest_Count && TimerStarted == false)
        {
            StartCoroutine(UpdateResturant());
        }

        Resturant_Count_Text.text = Current_Guest_Count + " / " + Max_Guest_Count;

        for (int x = 0; x < PersonObject.Count; x++)
        {
            if(Person[x].Finished == true)
            {
                Destroy(PersonObject[x]);
            }
        }
    }

    //public void FeedPeople()
    //{
    //    for (int x = 0; x < Person.Count; x++)
    //    {
    //        if(Person[x].At_Table == true && Person[x].HasBeenFed == false)
    //        {
    //            for (int i = 0; i < Inventory.Instance.Consumable_Slot_Count; i++)
    //            {
    //                //return slot with item
    //                int value = Inventory.Instance.GetSlotWithItem(ItemType.Consumable);

    //                if (value != -1)
    //                {
    //                    Inventory.Instance.SellItem(value, Person[x].ConsomeCount);
    //                    Person[x].Feed();
    //                    break;
    //                }
    //            }
    //        }
    //    }
    //}

    public void AddPeople()
    {
        for (int x = 0; x < GuestCount(); x++)
        {
            int indexOfEmptyChair = -1;
            for (int i = 0; i < ChairsInResturant.Count; i++)
            {
                if (ChairsInResturant[i].Empty == true)
                {
                    indexOfEmptyChair = i;
                    break;
                }
                else
                {
                    indexOfEmptyChair = -1;
                }
            }

            if (GuestCount() >= 0 && indexOfEmptyChair != -1)
            {
                Chair currentChair = ChairsInResturant[indexOfEmptyChair];
                AddPerson(10, ChairsInResturant[indexOfEmptyChair]);
                currentChair.Occupied();
                RemoveGuest(1);
            }
        }

        //for (int i = 0; i < Inventory.Instance.Consumable_Slot_Count; i++)
        //{
        //    //return slot with item
        //    int x = Inventory.Instance.GetSlotWithItem(ItemType.Consumable);

        //    if (x != -1)
        //    {
        //        Inventory.Instance.SellItem(x, GuestCount());
        //        RemoveGuest(GuestCount());
        //        break;
        //    }
        //}
    }

    public List<Chair> ChairsInResturant;

    public GameObject PersonParent;
    public GameObject PersonPrefab;

    public List<Person> Person = new List<Person>();
    public List<GameObject> PersonObject = new List<GameObject>();

    public void AddPerson(float time, Chair TargetChair)
    {
        GameObject person = Instantiate(PersonPrefab);
        person.transform.SetParent(PersonParent.transform);
        person.transform.localScale = new Vector3(1, 1, 1);
        person.transform.localPosition = new Vector3(0, 0, 0);

        Person PersonData = person.GetComponent<PersonData>().Person;

        PersonData.SetTargets(PersonParent, TargetChair);

        PersonData.personObject = person;
        PersonData.TimeToDestination = time;

        StartCoroutine(PersonData.Update());

        Person.Add(PersonData);
        PersonObject.Add(person);
    }
}
