using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

interface ChairState
{
    void Occupied();
    void Vacant();
}

[System.Serializable]
public class Chair : MonoBehaviour, ChairState
{
    private bool empty = true; 
    public bool Empty
    {
        get { return empty; }
    }

    public void Occupied()
    {
        empty = false;
    }

    public void Vacant()
    {
        empty = true;
    }
}
