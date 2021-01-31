using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicsController : MonoBehaviour
{
    public int currentAnimation;
    private Animator[] cinematicsAnimator;

    void Awake()
    {
        cinematicsAnimator = gameObject.GetComponentsInChildren<Animator>();
        currentAnimation = 0;
    }

    private void Update() {
        transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);
    }

    public void PlayCurrentAnimationAndUpdateState()
    {
        if (currentAnimation < cinematicsAnimator.Length)
        {
            cinematicsAnimator[currentAnimation].SetTrigger("Play");
            FindObjectOfType<IntratableFasesController>().isCinematic = true;
            currentAnimation++;
        }
    }
}
