using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public float cursorChangeOffSet;
    public float moveSpeed;

    private Animator cursorAnimator;
    private SpriteRenderer sprRenderer;
    private AudioSource audioSrc;
    private Vector3 mousePos;
    private Vector3 prevMousePos;

    void Awake()
    {
        cursorAnimator = gameObject.GetComponent<Animator>();
        sprRenderer = gameObject.GetComponent<SpriteRenderer>();
        audioSrc = gameObject.GetComponent<AudioSource>();
        Cursor.visible = false;
    }

    void Update()
    {
        Cursor.visible = false;

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, -7);

        transform.position = Vector2.Lerp(transform.position, mouseWorldPosition, moveSpeed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            cursorAnimator.SetTrigger("click");
            if (!audioSrc.isPlaying)
            {
                audioSrc.Play(0);
            }
        }
    }

    void FixedUpdate()
    {
        setAnimationState();
    }

    private void setAnimationState() {
        if(Input.GetAxis("Mouse X") < 0) {
            cursorAnimator.SetBool("isGoingLeft", true);
        }
        if(Input.GetAxis("Mouse X") > 0) {
            cursorAnimator.SetBool("isGoingLeft", false);
        }
    }

    public void ShowCursor()
    {
        sprRenderer.enabled = true;
    }

    public void HideCursor()
    {
        sprRenderer.enabled = false;
    }

    public void ClickAnimationEnded() {
        Debug.Log("Terminó");
    }

}
