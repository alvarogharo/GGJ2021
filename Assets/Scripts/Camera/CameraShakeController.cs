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
    private List<Animator> lightsAnimator = new List<Animator>();
    private float fadeOffsetPercentage = 0.25f;

    private void Start()
    {
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

    public void Shake(float magnitude, float roughness, float fadeInTime, float fadeOutTime)
    {
        SetCameraController(true);
        SetLigthsSwing(true);
        CameraShaker.Instance.RestPositionOffset = Camera.main.transform.position;
        CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);
        StartCoroutine(StopLightSwingAndCameraController(fadeOutTime - (fadeOutTime * fadeOffsetPercentage)));
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

    IEnumerator StopLightSwingAndCameraController(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SetLigthsSwing(false);
        SetCameraController(false);
    }
}
