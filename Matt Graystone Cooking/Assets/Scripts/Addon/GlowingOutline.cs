using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlowingOutline : MonoBehaviour {

    private Outline Outline;
    private float Alpha = 1;
    public float Speed = 5;
    public float MaxGlow = 1;
    public float MinGlow = 0.1f;

	// Use this for initialization
	void Start () {
        Outline = this.gameObject.transform.GetComponent<Outline>();
        StartCoroutine(Glow());
	}

    IEnumerator Glow()
    {
        Color color = new Color(Outline.effectColor.r,
            Outline.effectColor.g,
            Outline.effectColor.b,
            Outline.effectColor.b);

        bool TransitionDown = false;
        bool TransitionUp = true;

        while (true)
        {
            Alpha = Outline.effectColor.a;
            
            if(!TransitionDown)
            {
                Alpha -= (Alpha * Time.deltaTime) * Speed;

                if (Alpha <= MinGlow)
                {
                    TransitionDown = true;
                    TransitionUp = false;
                }
            }

            if (!TransitionUp)
            {
                Alpha += (Alpha * Time.deltaTime) * Speed;

                if (Alpha >= MaxGlow)
                {
                    TransitionDown = false;
                    TransitionUp = true;
                }
            }

            Alpha = Mathf.Clamp(Alpha, MinGlow, MaxGlow);

            color = new Color(Outline.effectColor.r,
                Outline.effectColor.g,
                Outline.effectColor.b,
                Alpha);

            Outline.effectColor = color;

            yield return null;
        }
    }

	// Update is called once per frame
	void Update () {
		


	}
}
