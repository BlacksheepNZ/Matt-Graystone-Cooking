using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public enum Currency
{
    S,
    K,
    M,
    B,
    T,
    AA,
    BB,
    CC,
    DD,
    EE,
    FF,
    GG,
    HH,
    II,
    JJ,
    KK,
    LL,
}

/// <summary>
/// 
/// </summary>
public class CurrencyManager : MonoBehaviour
{
	/// <summary>
    /// Instantiate class object
    /// </summary>
    public static CurrencyManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CurrencyManager>();
            }

            return CurrencyManager.instance;
        }
    }
    private static CurrencyManager instance;

    public Button button;
    public InputField input;
    public Text text;

    private List<Tuple<Currency, float>> CurrencyValue = new List<Tuple<Currency, float>>();

    private List<Currency> CurrencyTier = Enum.GetValues(typeof(Currency))
                                        .Cast<Currency>()
                                        .ToList(); 
                                        
    // Use this for initialization
    void Start ()
    {
        for (int i = 0; i < CurrencyTier.Count(); i++)
        {
            CurrencyValue.Add(new Tuple<Currency, float>(CurrencyTier[i], 0));
    	}
    }
	
	// check to see input value
	//each tier is caped at 1000 
	//if input is greater reduce input to %for that tier
	//t1 = 0 to 999
	//t2 = 1000 '3
	//t3 = 1000 '6
    private void AddGold(Currency currentToGet, float amount)
    {
        int current = (int)currencyToGet;
        float x = amount;
        
        //add 1 tier higher
        if(x > 999 && x < 999999)
        {
            x = x / 1000;
            current += 1;
        }//add 2 tier higher
        else if (x > 1000000 && x < 999999999)
        {
           x = x / 1000000;
           current += 2;
        }//add 3 tier higher
		else if(x > 1000000000 && x < 999999999999)
		{
			x = x / 1000000000;
			current += 3;
		}
		//do nothing
        else { }
        
        Add(current, x);
    }

    // Update is called once per frame
    // Add +1 to next tier when we have reached 1000
    // Reset to 0
    void Update ()
    {
        for (int i = 0; i < CurrencyValue.Count(); i++)
        {
            if (CurrencyValue[i].Item2 >= 1000)
            {
                CurrencyValue[i + 1].Item2 += 1;

                float excess = CurrencyValue[i].Item2 - 1000;
                if (excess > 0)
                {
                    CurrencyValue[i].Item2 = 0;
                    CurrencyValue[i].Item2 += excess;
                }
                else
                {
                    CurrencyValue[i].Item2 = 0;
                }
            }
        }

        text.text = GetValue(Currency.S).ToString() + "s \n" +
                    GetValue(Currency.K).ToString() + "k \n" +
                    GetValue(Currency.M).ToString() + "m";
    }

    public void Add(int index, float amount)
    {
                CurrencyValue[index].Item2 += amount;
    }

    public void Remove(int index, float amount)
    {
                CurrencyValue[index].Item2 -= amount;
    }

    public float GetValue(Currency currency)
    {
        for (int i = 0; i < CurrencyValue.Count(); i++)
        {
            if (CurrencyValue[i].Item1 == currency)
            {
            	if(i == 0)
            	{
            		return CurrencyValue[i].Item2;
            	}
            	else
            	{
                	return CurrencyValue[i].Item2 * (i * 3);
                }
            }
        }
        return 0;
    }
    
    public Tuple<Currency, float> GetHighestValue()
    {
        int index = 0;
        
        for(int i = 0; i < CurrencyValue.Count(); i++)
        {
            //loop through each tier and if we have more than 1 set as new highest
            if(CurrencyValue[i].Item2 > 0)
            {
            	index = i
            }
        }
        
        return CurrencyValue[index];	
    }
}
