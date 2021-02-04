using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public float defaultMoveSpeed;

    private Animator cursorAnimator;
    private SpriteRenderer sprRenderer;
    private AudioSource audioSrc;
    private Vector3 mousePos;
    private float moveSpeed;
    private bool blockedClick;

    void Awake()
    {
        cursorAnimator = gameObject.GetComponent<Animator>();
        sprRenderer = gameObject.GetComponent<SpriteRenderer>();
        audioSrc = gameObject.GetComponent<AudioSource>();
        moveSpeed = defaultMoveSpeed;
        blockedClick = false;
    }

    void Update()
    {
        Cursor.visible = false;

        if (Input.GetMouseButtonDown(0) && !blockedClick)
        {
            cursorAnimator.SetTrigger("click");
            moveSpeed = 0.1f;
            blockedClick = true;
            if (!audioSrc.isPlaying)
            {
                audioSrc.Play(0);
            }
        } else {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = Vector2.Lerp(transform.position, mouseWorldPosition, moveSpeed * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        setAnimationState();
    }

    public void startClickToMoveTransition() {
        StartCoroutine(clickToMoveTransition (moveSpeed, defaultMoveSpeed, 1));
    }

    private IEnumerator clickToMoveTransition (float oldValue, float newValue, float duration) {
        for (float t = 0f; t < duration; t += Time.deltaTime) {
            moveSpeed = Mathf.Lerp(oldValue, newValue, t / duration);
            yield return null;
        }
        moveSpeed = newValue;
        blockedClick = false;
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
}
