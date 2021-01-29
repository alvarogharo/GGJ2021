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

    public void ShakeDefault()
    {
        CameraShaker.Instance.ShakeOnce(defaultMagnitude, defaultRoughness, defaultFadeInTime, defaultFadeOutTime);
    }

    public void Shake(float magnitude, float roughness, float fadeInTime, float fadeOutTime)
    {
        CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);
    }
}
