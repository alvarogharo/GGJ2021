using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class OutlineController : MonoBehaviour
{
    public bool isInteractable;
    public bool hasOutline;
    private Light2D outlineLight;
    // Start is called before the first frame update
    void Awake()
    {
        outlineLight = gameObject.GetComponentInChildren<Light2D>();
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
}
