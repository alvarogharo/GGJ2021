using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBarsController : MonoBehaviour
{
    private Animator blackBarsAnimator;
    // Start is called before the first frame update
    void Start()
    {
        blackBarsAnimator = GetComponent<Animator>();
    }

    public void ShowBlackBars()
    {
        SetBlackBarsState(true);
    }

    public void HideBlackBars()
    {
        SetBlackBarsState(false);
    }

    private void SetBlackBarsState(bool areBarsIn)
    {
        blackBarsAnimator.SetBool("areBarsIn", areBarsIn);
    }
}
