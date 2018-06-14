using System.Collections;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class Prestige : MonoBehaviour
{
    /// <summary>
    /// Instantiate class object
    /// </summary>
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
    private static Prestige instance;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public float RemainingPoints = 0;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public float TotalPoints { get { return totalPoints; } }
    private float totalPoints = 0;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public float LifetimCurrency = 0;

    /// <summary>
    /// 
    /// </summary>
    private IEnumerator GetPoints()
    {
        while (true)
        {
            totalPoints = (int)Mathf.Sqrt(LifetimCurrency / 1000000000000000) * 150;

            yield return new WaitForSeconds(1);
        }
    }

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start() {
        StartCoroutine(GetPoints());
    }
	
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Points"></param>
	public void AddPoints(float Points)
    {
        totalPoints += Points;
    }

    /// <summary>
    /// 
    /// </summary>
    public void RemovePoints()
    {
        RemainingPoints -= TotalPoints;
    }
}
