using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchData : MonoBehaviour {

    public Research Research;

    public void Update()
    {
        StartCoroutine(Research.GUI());
    }
}
