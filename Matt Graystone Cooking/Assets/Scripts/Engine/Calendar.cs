using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class Calendar : MonoBehaviour
{
    /// <summary>
    /// Instantiate class object
    /// </summary>
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

    /// <summary>
    /// GUI
    /// </summary>
    public Text StarDate;

    /// <summary>
    /// Time in seconds for 1 day
    /// </summary>
    [HideInInspector]
    public float DayRate;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    private Stopwatch watch;

    /// <summary>
    /// Current date time
    /// </summary>
    [HideInInspector]
    public DateTime Date = new DateTime(2100, 1, 1);

    /// <summary>
    /// Add a day to the current date
    /// </summary>
    public void AddDay()
    {
        Date = Date.AddDays(1);
    }

    /// <summary>
    /// Reads out the current datetime.
    /// </summary>
    public override string ToString()
    {
        // for formating see https://msdn.microsoft.com/de-de/library/zdtaw1bw(v=vs.110).aspx
        return Date.ToString("yyy.MM.dd");
    }

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        watch = new Stopwatch();
        watch.Start();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if(watch.Elapsed.Seconds > DayRate)
        {
            AddDay();
            watch.Reset();
            watch.Start();
        }

        StarDate.text = ToString();
    }
}
