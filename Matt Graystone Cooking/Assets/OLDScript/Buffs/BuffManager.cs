using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour {

    private static BuffManager instance;
    public static BuffManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<BuffManager>();
            }

            return BuffManager.instance;
        }
    }

    public GameObject Parent;
    public GameObject BuffPrefab;

    public void AddBuff(BuffEffect buffEffect, float timeToComplete)
    {
        GameObject buffObject = Instantiate(BuffPrefab);
        buffObject.transform.SetParent(Parent.transform);
        buffObject.transform.localPosition = Vector3.zero;
        buffObject.transform.localScale = Vector3.one;

        Buffs buff = buffObject.GetComponent<Buffs>();
        buff.TimeToComplete = timeToComplete;
        buff.BuffEffect = buffEffect;
        StartCoroutine(buff.Begin());
    }
}
