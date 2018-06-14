using UnityEngine;

/// <summary>
/// 
/// </summary>
public class ResearchData : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public Research Research;

    /// <summary>
    /// 
    /// </summary>
    public void Update()
    {
        StartCoroutine(Research.GUI());
    }
}
