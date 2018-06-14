using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class FloatingText : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public Animator animator;

    /// <summary>
    /// 
    /// </summary>
    public Text GUI_Text;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void OnEnable()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetText(string text)
    {
        GUI_Text.text = text;
    }
}
