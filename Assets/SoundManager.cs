using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instacne;

    public AudioSource TranitionEffect;
    public AudioSource PopEffect;
    private void Awake()
    {
        if(Instacne==null)
        {
            Instacne = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void PlayTransitionEffect()
    {
        TranitionEffect.Play();
    }
    public void PlayPopEffect()
    {
        PopEffect.Play();
    }
}
