using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicsSoundController : MonoBehaviour
{
    public AudioClip[] audioClips;
    public float[] volumes;
    public bool[] doesLoops;
    public List<AudioSource> audioSources;
    public AmbientSoundController ambientController;
    // Start is called before the first frame update
    void Start()
    {
        ambientController = GameObject.Find("AmbientSoundController").GetComponent<AmbientSoundController>();
        for (int i = 0; i < audioClips.Length; i++)
        {
            AudioSource currentAudioSource = gameObject.AddComponent<AudioSource>();
            currentAudioSource.clip = audioClips[i];
            currentAudioSource.volume = volumes[i];
            currentAudioSource.playOnAwake = false;
            if (doesLoops.Length > 0)
            {
                currentAudioSource.loop = doesLoops[i];
            }
            audioSources.Add(currentAudioSource);
        }
    }

    public void StopSound(int clipIndex)
    {
        audioSources[clipIndex].Stop();
    }

    public void StartSound(int clipIndex)
    {
        audioSources[clipIndex].Play();
    }
    public void StopRain()
    {
        ambientController.StopAmbient(ambientController.RAIN_INDEX);
    }

    public void StopMusic()
    {
        ambientController.StopAmbient(ambientController.MUSIC_INDEX);
    }

    public void StartRain()
    {
        ambientController.StartAmbient(ambientController.RAIN_INDEX);
    }

    public void StartMusic()
    {
        ambientController.StartAmbient(ambientController.MUSIC_INDEX);
    }
}
