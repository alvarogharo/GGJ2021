using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothInteraction : InteractableObjectController
{
    [SerializeField]
    private InteractWithObject washingMachine;

    public override void Interact(){
        if(FindObjectOfType<CharacterInventory>().AddToInventory(this.gameObject)){
            washingMachine.isInteractable = true;
            //Lo llevamos arriba porque no se puede borrar si no perdería la referencia
            this.transform.position = new Vector3(0,100,0);
        }
        
    }
}
