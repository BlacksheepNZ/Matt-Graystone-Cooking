using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardFireworks : MonoBehaviour {

    private static RewardFireworks instance;
    public static RewardFireworks Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<RewardFireworks>();
            }

            return RewardFireworks.instance;
        }
    }

    public ParticleSystem particle;

    public void Play()
    {
        SoundManager.Instance.Play_FireWorks();
        StartCoroutine(OnPlay());
    }

    public bool playing = false;
    public IEnumerator OnPlay()
    {
        playing = true;

        while (playing)
        {
            particle.Emit(1);
            playing = false;

            yield return new WaitForSeconds(1);
        }

        particle.Stop(true);
        particle.Clear(true);
    }
}
