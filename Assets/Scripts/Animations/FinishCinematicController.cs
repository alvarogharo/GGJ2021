using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCinematicController : MonoBehaviour
{
    public void FinishCinematic(int index){
        switch(index){
            case 0:
                FindObjectOfType<CursorController>().ShowCursor();
                FindObjectOfType<CharacterMovement>().EnableControl();
                //Character interactable
                break;
            case 1:
                FindObjectOfType<CursorController>().ShowCursor();
                FindObjectOfType<CharacterMovement>().EnableControl();
                FindObjectOfType<CameraMovement>().EnableControl();
                FindObjectOfType<IntratableFasesController>().EnableFase(1);
                //Camera Shake intervalos
                break;
            case 2:
                FindObjectOfType<CursorController>().ShowCursor();
                FindObjectOfType<CharacterMovement>().EnableControl();
                FindObjectOfType<CameraMovement>().EnableControl();
                FindObjectOfType<IntratableFasesController>().EnableFase(2);
                break;
            case 3:
                //End of the game
                break;
        }
    }
}
