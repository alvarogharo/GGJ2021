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

        isInteractable = false;

        if(objectList.Count == 0){
            int index = FindIndexOfPhase(phaseActions, phaseController.currentFase);
            phaseActions[index].UseItem();
        }
        
        
    }
}
