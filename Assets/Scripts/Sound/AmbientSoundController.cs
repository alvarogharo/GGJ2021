using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundController : MonoBehaviour
{
    public readonly int MUSIC_INDEX = 0;
    public readonly int RAIN_INDEX = 1;

    public AudioSource[] audioSources;
    // Start is called before the first frame update
    void Start()
    {
        audioSources = gameObject.GetComponents<AudioSource>();
    }

    public void StopAmbient(int clipIndex)
    {
        audioSources[clipIndex].Stop();
    }

    public void StartAmbient(int clipIndex)
    {
        audioSources[clipIndex].Play();
    }
}
