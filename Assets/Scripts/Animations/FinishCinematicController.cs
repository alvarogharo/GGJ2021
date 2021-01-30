using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCinematicController : MonoBehaviour
{
    public void FinishCinematic(int index){
        FindObjectOfType<IntratableFasesController>().isCinematic = false;
        switch(index){
            case 0:
                //#1 - Finaliza la primera animación - Se activa ratón e interacción con Super
                FindObjectOfType<CursorController>().ShowCursor();
                FindObjectOfType<CharacterMovement>().GetComponent<InteractableObjectController>().isInteractable = true;
                //Character interactable
                break;
            case 1:
                //#7 - Finaliza la segunda animación - Se activan todos los controles - Se activan fase 2
                FindObjectOfType<CursorController>().ShowCursor();
                FindObjectOfType<CharacterMovement>().EnableControl();
                FindObjectOfType<CameraMovement>().EnableControl();
                FindObjectOfType<IntratableFasesController>().EnableFase(1);
                //Camera Shake intervalos
                break;
            case 2:
                //#13 - Finaliza tercera animación - Se activan todos los controles - Se activa fase 3 - Se inicia el primer diálogo
                FindObjectOfType<CursorController>().ShowCursor();
                FindObjectOfType<CharacterMovement>().EnableControl();
                FindObjectOfType<CameraMovement>().EnableControl();
                FindObjectOfType<IntratableFasesController>().EnableFase(2);
                FindObjectOfType<DialogueManager>().StartDialogue(2,0);
                break;
            case 3:
                //#22 - Fin del juego - Créditos
                //End of the game
                Debug.Log("Finish");
                break;
        }
    }
}
