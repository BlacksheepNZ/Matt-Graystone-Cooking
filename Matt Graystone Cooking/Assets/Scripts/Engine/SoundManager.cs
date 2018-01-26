using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
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

    public AudioSource AmbientSound;
    public AudioSource Click;
    public AudioSource CashRegister;
    public AudioSource FireWorks;

    public bool Muted = false;

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

    public void Play_AmbientSound()
    {
        if(!Muted)
            AmbientSound.Play();
    }
    public void Play_Click()
    {
        if (!Muted)
            Click.Play();
    }
    public void Play_CashRegister()
    {
        if (!Muted)
            CashRegister.Play();
    }
    public void Play_FireWorks()
    {
        if (!Muted)
            FireWorks.Play();
    }
}
