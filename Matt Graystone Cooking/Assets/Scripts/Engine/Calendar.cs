using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Calendar : MonoBehaviour {

    private static Calendar instance;
    public static Calendar Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Calendar>();
            }

            return global::Calendar.instance;
        }
    }

    public Text StarDate;

    public float DateRate;
    private Stopwatch watch;

    public DateTime Date = new DateTime(2100, 1, 1);

    public void AddDay()
    {
        Date = Date.AddDays(1);
    }

    public override string ToString()
    {
        // for formating see https://msdn.microsoft.com/de-de/library/zdtaw1bw(v=vs.110).aspx
        return Date.ToString("yyy.MM.dd");
    }

    // Use this for initialization
    void Start () {
        watch = new Stopwatch();
        watch.Start();
    }
	
	// Update is called once per frame
	void Update () {

        if(watch.Elapsed.Seconds > DateRate)
        {
            AddDay();
            watch.Reset();
            watch.Start();
        }

        StarDate.text = ToString();
    }
}
