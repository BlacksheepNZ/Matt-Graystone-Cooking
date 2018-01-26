using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clouds : MonoBehaviour
{
    public Image Cloud;
    public float Speed = 50.0f;

    // Use this for initialization
    void Start()
    {
        float randomRange = Random.Range(0.5f, 1f);
        Vector3 scale = new Vector3(randomRange, randomRange, 0);
        transform.localScale = scale;
        Cloud.transform.position = Vector3.zero;
        Cloud.transform.localPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posX = Cloud.transform.position;
        posX.x += Random.Range(Speed / 10, Speed) * (Time.time / (24 * 60 * 60));

        RectTransform rt = Cloud.transform.parent.GetComponent<RectTransform>();

        if (Cloud.transform.localPosition.x > rt.sizeDelta.x)
        {
            Destroy(GameObject.Find(name));
        }

        Cloud.transform.position = posX;
    }
}
