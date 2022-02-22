using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeInSOund : MonoBehaviour
{
    //fade bgm imto the game

    public AudioSource audioSource;
    public float time;
    public float targetVolume;

    void Start()
    {
        StartCoroutine(FadeAudioSource.StartFade(audioSource, time, targetVolume));
    }

  
}
