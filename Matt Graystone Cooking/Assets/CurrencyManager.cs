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

        button.onClick.AddListener(() => OnClick());

    }

    public Currency currentCurreny = Currency.S;

    private void OnClick()
    {
        float x = float.Parse(input.text);
        Add(currentCurreny, x);
    }

    // Update is called once per frame
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

    public void Add(Currency currency, float amount)
    {
        for (int i = 0; i < CurrencyValue.Count(); i++)
        {
            if(CurrencyValue[i].Item1 == currency)
            {
                CurrencyValue[i].Item2 += amount;
            }
        }
    }

    public void Remove(Currency currency, float amount)
    {
        for (int i = 0; i < CurrencyValue.Count(); i++)
        {
            if (CurrencyValue[i].Item1 == currency)
            {
                CurrencyValue[i].Item2 -= amount;
                break;
            }
        }
    }

    public float GetValue(Currency currency)
    {
        for (int i = 0; i < CurrencyValue.Count(); i++)
        {
            if (CurrencyValue[i].Item1 == currency)
            {
                return CurrencyValue[i].Item2;
            }
        }

        return 0;
    }
}
