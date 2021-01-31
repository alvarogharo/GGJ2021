using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public float cursorChangeOffSet;

    private Animator cursorAnimator;
    private SpriteRenderer sprRenderer;
    private AudioSource audioSrc;
    private Vector3 mousePos;
    private Vector3 prevMousePos;

    // Start is called before the first frame update
    void Awake()
    {
        cursorAnimator = gameObject.GetComponent<Animator>();
        sprRenderer = gameObject.GetComponent<SpriteRenderer>();
        audioSrc = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        mousePos = Input.mousePosition;
        Vector3 mouseMotion = prevMousePos - mousePos;

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(worldPosition.x, worldPosition.y, -7);

        if (mouseMotion.x > cursorChangeOffSet)
        {
            cursorAnimator.SetBool("isGoingLeft", true);
        } else if (mouseMotion.y < -cursorChangeOffSet)
        {
            cursorAnimator.SetBool("isGoingLeft", false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            cursorAnimator.SetTrigger("click");
            if (!audioSrc.isPlaying)
            {
                audioSrc.Play(0);
            }
        }

        prevMousePos = mousePos;
    }

    public void ShowCursor()
    {
        sprRenderer.enabled = true;
    }

    public void HideCursor()
    {
        sprRenderer.enabled = false;
    }

}
