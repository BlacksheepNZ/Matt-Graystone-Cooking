using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Gui_Anim_Component
{
    [HideInInspector]
    private GameObject Game_Object;
    public bool Enabled;
    /// <summary>
    /// 0 slow, 10 instant
    /// </summary>
    [Range(0, 1)]
    public float Speed;

    public void SetGameObject(GameObject gameObject)
    {
        Game_Object = gameObject;
    }

    /// <summary>
    /// Fade Object in need to set onclick GameObject SetActive(True)
    /// </summary>
    public IEnumerator FadeIn(bool rayCast)
    {
        //Game_Object.transform.localPosition = Move_To_Position;
        float t = 0;

        if (Enabled == true)
        {
            while (t < 1)
            {
                //RaycastTarget(rayCast);

                t += Time.deltaTime * Speed;

                //fix
                if (Game_Object == null)
                    break;

                Game_Object.GetComponent<CanvasGroup>().alpha = t;

                yield return null;
            }

            //fix
            if (Game_Object != null)
                Game_Object.GetComponent<CanvasGroup>().alpha = 1;
        }
    }
    public IEnumerator FadeOut(bool rayCast)
    {
        float t = 1;

        if (Enabled == true)
        {
            while (t > 0)
            {
                //RaycastTarget(rayCast);

                t -= Time.fixedDeltaTime * Speed;
                Game_Object.GetComponent<CanvasGroup>().alpha = t;

                yield return null;
            }
        }

        //Game_Object.transform.localPosition = Move_To_Position;
        Game_Object.GetComponent<CanvasGroup>().alpha = 0;
        Game_Object.SetActive(false);

    }

    private void RaycastTarget(bool enable)
    {
        if (enable == true)
        {
            Game_Object.GetComponent<Image>().raycastTarget = true;
        }
        else
        {
            Game_Object.GetComponent<Image>().raycastTarget = false;
        }
    }
}