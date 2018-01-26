using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buffs : MonoBehaviour {

    public BuffEffect BuffEffect;
    public Image Image;
    public float TimeToComplete;

    public IEnumerator Begin()
    {
        float current = 0;
        float speed = (Time.fixedDeltaTime / TimeToComplete);

        while (current < 1)
        {
            current += speed;
            Image.fillAmount = current;

            yield return null;
        }

        current = 0;
        Destroy(this.gameObject);
    }
}
