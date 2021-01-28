using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineController : MonoBehaviour
{
    public bool isInteractable;
    public bool hasOutline;
    public Material defaultMaterial;
    public Material outlineMaterial;
    private SpriteRenderer sprRenderer;
    // Start is called before the first frame update
    void Awake()
    {
        sprRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isInteractable)
        {
            if (!hasOutline)
            {
                sprRenderer.material = defaultMaterial;
            }
            else
            {
                sprRenderer.material = outlineMaterial;
            }
        } else
        {
            sprRenderer.material = defaultMaterial;
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
