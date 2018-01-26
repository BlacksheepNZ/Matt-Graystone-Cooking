using UnityEngine;
using UnityEngine.UI;

public class JemDrop : MonoBehaviour
{

    public Animator Animator;
    private Image image;

    private void OnEnable()
    {
        AnimatorClipInfo[] clipInfo = Animator.GetCurrentAnimatorClipInfo(0);
 
        image = Animator.GetComponent<Image>();
    }
}