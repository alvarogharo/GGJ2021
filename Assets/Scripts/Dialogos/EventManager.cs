using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public void TriggerEvent(string code){
        switch(code){
            case "activateMovement":
                FindObjectOfType<CharacterMovement>().EnableControl();
                break;
            case "enableFirstPhase":
                FindObjectOfType<IntratableFasesController>().EnableFase(0);
                break;
            case "activateFridgeTV":
                FindObjectOfType<IntratableFasesController>().AddInteractableObjectByName("TV");
                FindObjectOfType<IntratableFasesController>().AddInteractableObjectByName("Fridge");
                break;
            case "tvOffwindowOn":
                GameObject.Find("TV").GetComponent<Animator>().SetTrigger("SwitchOff");
                FindObjectOfType<IntratableFasesController>().AddInteractableObjectByName("Window");
                break;
            case "activateClothes":
                FindObjectOfType<IntratableFasesController>().AddInteractableObjectByName("Cloth1");
                FindObjectOfType<IntratableFasesController>().AddInteractableObjectByName("Cloth2");
                FindObjectOfType<IntratableFasesController>().AddInteractableObjectByName("Cloth3");
                break;
            case "exclamationShow":
                //provisional
                //FindObjectOfType<CharacterMovement>().ShowExclamation();
                break;
            case "hardShake":
                //provisional
                FindObjectOfType<CameraShakeController>().ShakeDefault();
                break;
            case "activateStoreroom":
                FindObjectOfType<IntratableFasesController>().AddInteractableObjectByName("Storeroom");
                break;
            case "keysInventory":
                GameObject key = GameObject.Find("Key");
                //TO DO
                //FindObjectOfType<CharacterInventory>().AddToInventory(key);
                break;
            case "activateDoor":
                FindObjectOfType<IntratableFasesController>().AddInteractableObjectByName("Door");
                break;
            case "openDoor":
               //TO DO
               break;
        }
    }
}
