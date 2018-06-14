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
    public Vector3 Move_To_Position;
    public float Speed;
    public float Delay;

    public void SetGameObject(GameObject gameObject)
    {
        Game_Object = gameObject;
    }

    public void MoveInstant(bool rayCast)
    {
        Game_Object.transform.localPosition = Move_To_Position;
    }

    public IEnumerator MoveOverSeconds(bool rayCast)
    {
        if (Enabled == true)
        {
            RaycastTarget(rayCast);

            yield return new WaitForSeconds(Delay);

            float elapsedTime = 0;
            Vector3 startingPos = Game_Object.transform.localPosition;
            while (elapsedTime < Speed)
            {
                Game_Object.transform.localPosition = Vector3.Lerp(startingPos, Move_To_Position, (elapsedTime / Speed));
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            Game_Object.transform.localPosition = Move_To_Position;
        }
    }

    public IEnumerator Move(bool rayCast)
    {
        if (Enabled == true)
        {
            RaycastTarget(rayCast);

            Game_Object.transform.localPosition = Move_To_Position;
            yield return null;
        }
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
                Game_Object.GetComponent<CanvasGroup>().alpha = t;

                yield return null;
            }

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