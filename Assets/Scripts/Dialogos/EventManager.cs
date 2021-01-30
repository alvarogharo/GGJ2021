using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public void TriggerEvent(string code){
        switch(code){
            case "activateMovement":
                //#2 - Click en la super - Después de su frase se mantiene la fijación de la cámara y ya no es interactuable
                FindObjectOfType<CameraMovement>().DisableControl();
                FindObjectOfType<CharacterMovement>().GetComponent<InteractableObjectController>().isInteractable = false;
                break;
            case "enableFirstPhase":
                //#5 - Al llegar al punto al segundo click activa la fase 1
                FindObjectOfType<IntratableFasesController>().EnableFase(0);
                break;
            case "activateFridgeTV":
                //#8 - Después de 3 click en el traje - Activamos la tele y la nevera
                FindObjectOfType<IntratableFasesController>().AddInteractableObjectByName("TV");
                FindObjectOfType<IntratableFasesController>().AddInteractableObjectByName("Fridge");
                break;
            case "tvOffwindowOn":
                //#9 - Después del click en la tele - Activamos ventana
                //GameObject.Find("TV").GetComponent<Animator>().SetTrigger("SwitchOff");
                FindObjectOfType<IntratableFasesController>().AddInteractableObjectByName("Window");
                break;
            case "activateClothes":
                //#10 - Después del click en la tele - Activamos ropa
                FindObjectOfType<IntratableFasesController>().AddInteractableObjectByName("1DirtyClothes");
                FindObjectOfType<IntratableFasesController>().AddInteractableObjectByName("2DirtyClothes");
                FindObjectOfType<IntratableFasesController>().AddInteractableObjectByName("3DirtyClothes");
                break;
            case "exclamationShow":
                //provisional
                //FindObjectOfType<CharacterMovement>().ShowExclamation();
                break;
            case "hardShake":
                //provisional
                FindObjectOfType<CameraShakeController>().ShakeDefault();
                break;
            case "addStoreKey":
                //#17 - Se añaden las llaves al inventario
                GameObject srKey = GameObject.Find("StoreRoomKey");
                FindObjectOfType<CharacterInventory>().AddToInventory(srKey);
                break;
            case "activateStoreroom":
                //#15 - Activa la despensa para usar la llave        
                FindObjectOfType<IntratableFasesController>().AddInteractableObjectByName("Storeroom");
                break;
            case "keysInventory":
                //#17 - Se añaden las llaves al inventario
                GameObject key = GameObject.Find("HouseKeys");
                FindObjectOfType<CharacterInventory>().AddToInventory(key);
                break;
            case "activateDoor":
                //#18 - Activamos la puerta
                FindObjectOfType<IntratableFasesController>().AddInteractableObjectByName("Door");
                break;
            case "openDoor":
                //#20 - Animación de abrir la puerta
               //TO DO
               break;
        }
    }
}
