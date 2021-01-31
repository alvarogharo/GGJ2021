using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cajon : InteractableObjectController
{
    // Update is called once per frame
    public override void Interact(){
        FindObjectOfType<CajonController>().Used();
        isInteractable = false; 
        outlineLight.enabled = false;      
    }
}
