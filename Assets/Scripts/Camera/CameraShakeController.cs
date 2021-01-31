using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class CameraShakeController : MonoBehaviour
{
    public float defaultMagnitude;
    public float defaultRoughness;
    public float defaultFadeInTime;
    public float defaultFadeOutTime;
    private CameraMovement cameraMovement;
    private CameraShaker cameraShaker;
    private CinematicsSoundController soundController;
    private List<Animator> lightsAnimator = new List<Animator>();
    private float fadeOffsetPercentage = 0.25f;
    private Coroutine lightCoroutine;
    private Coroutine hardCoroutine;

    private void Start()
    {
        soundController = gameObject.GetComponent<CinematicsSoundController>();
        GameObject camera = Camera.main.gameObject;
        cameraMovement = camera.GetComponent<CameraMovement>();
        cameraShaker = camera.GetComponent<CameraShaker>();
        GameObject[] lights = GameObject.FindGameObjectsWithTag("CeilingLight");
        foreach (GameObject light in lights)
        {
            lightsAnimator.Add(light.GetComponent<Animator>());
        }
    }

    public void ShakeDefault()
    {
        Shake(defaultMagnitude, defaultRoughness, defaultFadeInTime, defaultFadeOutTime);
    }

    public void Shake(float magnitude, float roughness, float fadeInTime, float fadeOutTime, int intensity = 0)
    {
        SetCameraController(true);
        SetLigthsSwing(true);
        soundController.StartSound(intensity);
        CameraShaker.Instance.RestPositionOffset = Camera.main.transform.position;
        CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);
        StartCoroutine(StopLightSwingAndCameraController(fadeOutTime - (fadeOutTime * fadeOffsetPercentage), intensity));
    }

    public void StartLightConstantVibration()
    {
        lightCoroutine = StartCoroutine(StartLightConstant(30, 0.4f, 0.6f, 0.1f, 5f, 0));
    }

    public void StartHardConstantVibration()
    {
        hardCoroutine = StartCoroutine(StartLightConstant(30, 0.8f, 0.8f, 0.1f, 5f, 1));
    }

    public void StopVibrations()
    {
        StopAllCoroutines();
    }

    private void SetLigthsSwing(bool state)
    {
        foreach(Animator animator in lightsAnimator)
        {
            animator.SetBool("IsSwinging", state);
        }
    }

    private void SetCameraController(bool isShaking)
    {
        if (cameraMovement && cameraShaker)
        {
            cameraShaker.enabled = isShaking;
            cameraMovement.enabled = !isShaking;
        }
    }

    IEnumerator StopLightSwingAndCameraController(float seconds, int intensity)
    {
        yield return new WaitForSeconds(seconds);
        SetLigthsSwing(false);
        SetCameraController(false);
        soundController.StopSound(intensity);
    }

    IEnumerator StartLightConstant(float interval, float magnitude, float roughness, float fadeInTime, float fadeOutTime, int intensity)
    {
        while (true)
        {
            Shake(magnitude, roughness, fadeInTime, fadeOutTime, intensity);
            yield return new WaitForSeconds(interval);
        }
    }
}
