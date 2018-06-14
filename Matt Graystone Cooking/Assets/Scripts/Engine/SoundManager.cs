using UnityEngine;

/// <summary>
/// 
/// </summary>
public class SoundManager : MonoBehaviour
{
    /// <summary>
    /// Instantiate class object
    /// </summary>
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<SoundManager>();
            }

            return SoundManager.instance;
        }
    }
    private static SoundManager instance;

    /// <summary>
    /// 
    /// </summary>
    public AudioSource AmbientSound;

    /// <summary>
    /// 
    /// </summary>
    public AudioSource Click;

    /// <summary>
    /// 
    /// </summary>
    public AudioSource CashRegister;

    /// <summary>
    /// 
    /// </summary>
    public AudioSource FireWorks;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public bool Muted = false;

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    public void Update()
    {
        if(Muted == true)
        {
            AmbientSound.Stop();
            Click.Stop();
            CashRegister.Stop();
            FireWorks.Stop();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void Play_AmbientSound()
    {
        if(!Muted)
            AmbientSound.Play();
    }

    /// <summary>
    /// 
    /// </summary>
    public void Play_Click()
    {
        if (!Muted)
            Click.Play();
    }

    /// <summary>
    /// 
    /// </summary>
    public void Play_CashRegister()
    {
        if (!Muted)
            CashRegister.Play();
    }

    /// <summary>
    /// 
    /// </summary>
    public void Play_FireWorks()
    {
        if (!Muted)
            FireWorks.Play();
    }
}
