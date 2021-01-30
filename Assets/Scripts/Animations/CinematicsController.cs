using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicsController : MonoBehaviour
{
    public int currentAnimation;
    private Animator[] cinematicsAnimator;

    void Start()
    {
        cinematicsAnimator = gameObject.GetComponentsInChildren<Animator>();
        currentAnimation = 0;
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
