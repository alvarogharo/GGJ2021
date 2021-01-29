using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class InteractableObjectController : MonoBehaviour
{
    public bool isInteractable;
    public bool hasOutline;
    private Light2D outlineLight;
    private AudioSource audioSrc;
    // Start is called before the first frame update
    void Awake()
    {
        outlineLight = gameObject.GetComponentInChildren<Light2D>();
        audioSrc = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isInteractable)
        {
            outlineLight.enabled = hasOutline;
        }
    }

    private void OnMouseEnter()
    {
        hasOutline = true;
    }

    private void OnMouseExit()
    {
        hasOutline = false;
    }

    private void OnMouseDown()
    {
        if (isInteractable && audioSrc.clip && !audioSrc.isPlaying)
        {
            audioSrc.Play(0);
        }
    }
}
