using UnityEngine;

public class CurrencyConverter : MonoBehaviour
{

    private static CurrencyConverter instance;
    public static CurrencyConverter Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CurrencyConverter>();
            }

            return CurrencyConverter.instance;
        }
    }

    void Awake()
    {
        CreateInstance();
    }

    void CreateInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    string format = "0.##";
    string dollar = "$";

    public string GetCurrencyIntoString(float valueToConvert)
    {
        valueToConvert = Mathf.Clamp(valueToConvert, 0.0f, float.MaxValue);

        string converted;
        if (valueToConvert >= 1000000000000000000000000000000000000f)
            converted = dollar + (valueToConvert / 1000000000000000000000000000000000000f).ToString(format) + "U";
        else if(valueToConvert >= 1000000000000000000000000000000000f)
            converted = dollar + (valueToConvert / 1000000000000000000000000000000000f).ToString(format) + "D";
        else if(valueToConvert >= 1000000000000000000000000000000f)
            converted = dollar + (valueToConvert / 1000000000000000000000000000000f).ToString(format) + "N";
        else if(valueToConvert >= 1000000000000000000000000000f)
            converted = dollar + (valueToConvert / 1000000000000000000000000000f).ToString(format) + "O";
        else if(valueToConvert >= 1000000000000000000000000f)
            converted = dollar + (valueToConvert / 1000000000000000000000000f).ToString(format) + "S";
        else if(valueToConvert >= 1000000000000000000000f)
            converted = dollar + (valueToConvert / 1000000000000000000000f).ToString(format) + "s";
        else if(valueToConvert >= 1000000000000000000f)
            converted = dollar + (valueToConvert / 1000000000000000000f).ToString(format) + "Q";
        else if(valueToConvert >= 1000000000000000f)
            converted = dollar + (valueToConvert / 1000000000000000f).ToString(format) + "q";
        else if(valueToConvert >= 1000000000000f)
            converted = dollar + (valueToConvert / 1000000000000f).ToString(format) + "T";
        else if(valueToConvert >= 1000000000f)
            converted = dollar + (valueToConvert / 1000000000f).ToString(format) + "B";
        else if(valueToConvert >= 1000000f)
            converted = dollar + (valueToConvert / 1000000f).ToString(format) + "M";
        else if (valueToConvert >= 1000f)
            converted = dollar + (valueToConvert / 1000f).ToString(format) + "K";
        else
            converted = dollar + valueToConvert.ToString("0");

        return converted;
    }
    public string GetCurrencyIntoStringNoSign(float valueToConvert)
    {
        valueToConvert = Mathf.Clamp(valueToConvert, 0.0f, float.MaxValue);

        string converted;
        if (valueToConvert >= 1000000000000000000000000000000000000f)
            converted = (valueToConvert / 1000000000000000000000000000000000000f).ToString(format) + "U";
        else if (valueToConvert >= 1000000000000000000000000000000000f)
            converted = (valueToConvert / 1000000000000000000000000000000000f).ToString(format) + "D";
        else if (valueToConvert >= 1000000000000000000000000000000f)
            converted = (valueToConvert / 1000000000000000000000000000000f).ToString(format) + "N";
        else if (valueToConvert >= 1000000000000000000000000000f)
            converted = (valueToConvert / 1000000000000000000000000000f).ToString(format) + "O";
        else if (valueToConvert >= 1000000000000000000000000f)
            converted = (valueToConvert / 1000000000000000000000000f).ToString(format) + "S";
        else if (valueToConvert >= 1000000000000000000000f)
            converted = (valueToConvert / 1000000000000000000000f).ToString(format) + "s";
        else if (valueToConvert >= 1000000000000000000f)
            converted = (valueToConvert / 1000000000000000000f).ToString(format) + "Q";
        else if (valueToConvert >= 1000000000000000f)
            converted = (valueToConvert / 1000000000000000f).ToString(format) + "q";
        else if (valueToConvert >= 1000000000000f)
            converted = (valueToConvert / 1000000000000f).ToString(format) + "T";
        else if (valueToConvert >= 1000000000f)
            converted = (valueToConvert / 1000000000f).ToString(format) + "B";
        else if (valueToConvert >= 1000000f)
            converted = (valueToConvert / 1000000f).ToString(format) + "M";
        else if (valueToConvert >= 1000f)
            converted = (valueToConvert / 1000f).ToString(format) + "K";
        else
            converted =  valueToConvert.ToString("0");

        return converted;
    }
}

