using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothInteraction : InteractableObjectController
{
    [SerializeField]
    private InteractWithObject washingMachine;

    public override void Interact(){
        if(FindObjectOfType<CharacterInventory>().AddToInventory(this.gameObject)){
            FindObjectOfType<InteractWithObject>().isInteractable = true;
            Destroy(this.gameObject);
        }
        
    }
}
