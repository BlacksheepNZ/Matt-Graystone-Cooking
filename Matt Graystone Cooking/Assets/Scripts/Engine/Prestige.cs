using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prestige : MonoBehaviour {


    private static Prestige instance;
    public static Prestige Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Prestige>();
            }

            return Prestige.instance;
        }
    }

    public float RemainingPoints = 0;
    public float TotalPoints = 0;
    public float LifetimCurrency = 0;

    IEnumerator GetPoints()
    {
        while (true)
        {
            TotalPoints = (int)Mathf.Sqrt(LifetimCurrency / 1000000000000000) * 150;

            yield return new WaitForSeconds(1);
        }
    }

    void Start () {
        StartCoroutine(GetPoints());
    }
	
	public void AddPoints(float Points)
    {
        TotalPoints += Points;
    }

    public void RemovePoints()
    {
        RemainingPoints -= TotalPoints;
    }
}
