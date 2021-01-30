using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractWithObject : InteractableObjectController
{
    [SerializeField]
    private GameObject[] neededObjects;
    // Start is called before the first frame update
    private List<GameObject> objectList;
    void Start()
    {
        objectList.AddRange(neededObjects);
    }

    public override void Interact(){
        GameObject go = FindObjectOfType<CharacterInventory>().GetObject();
        if(objectList.Contains(go)){
            objectList.Remove(go);
        }
        FindObjectOfType<CharacterInventory>().RemoveInventory();

        isInteractable = false;

        if(objectList.Count == 0){
            //#11 - la lavadora tiene toda la ropa y lanza el trigger de la conversación
            //#16 - la despensa usa la llave
            //#19 - Abrimos la puerta con la llave de casa
            int index = FindIndexOfPhase(phaseActions, phaseController.currentFase);
            phaseActions[index].UseItem();
        }
        
        
    }
}
