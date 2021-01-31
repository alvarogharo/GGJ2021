using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntratableFasesController : MonoBehaviour
{
    public string[] interactableObjets;
    public int currentFase = 0;
    public int[] numberOfKeyObjectPerPhase;
    public bool isCinematic;
    private List<InteractableObjectController> currentFaseControllers;
    private bool[] keyObjectFaseController;
    private DialogueManager dm;
    
    // Start is called before the first frame update
    private void Awake()
    {
        dm = FindObjectOfType<DialogueManager>();
        keyObjectFaseController = new bool[numberOfKeyObjectPerPhase[currentFase]];
        for(int i = 0; i<keyObjectFaseController.Length; i++){
            keyObjectFaseController[i] = false;
        }
        currentFaseControllers = GetOutlineControllersByFase(currentFase);
        //EnableFase(currentFase);
    }

    private void Start() {
        isCinematic = false;
        //#0 - Primera animación
        FindObjectOfType<CinematicsController>().PlayCurrentAnimationAndUpdateState();
    }

    private void Update() {

        if(IsPhaseCompleted() && !dm.IsInDialogue() && !isCinematic){ //Falta if cinematic on
            FinishFase(currentFase);
        }
    }
    
    public void EnableFase(int fase)
    {
        SwitchInteractibleFromControllers(currentFaseControllers, false);
        currentFaseControllers = GetOutlineControllersByFase(fase);
        SwitchInteractibleFromControllers(currentFaseControllers, true);
        keyObjectFaseController = new bool[numberOfKeyObjectPerPhase[fase]];
        for(int i = 0; i<keyObjectFaseController.Length; i++){
            keyObjectFaseController[i] = false;
        }
        Debug.Log(keyObjectFaseController.ToString());
        currentFase = fase;
    }

    public void FinishFase(int fase){
        dm.DisableControl();
        CharacterMovement cm = FindObjectOfType<CharacterMovement>();
        if(cm.gameObject.activeSelf){
            cm.DisableControl();
        }
        FindObjectOfType<CameraMovement>().DisableControl();
        FindObjectOfType<CursorController>().HideCursor();
        
        //Ocultar ratón
        //Deshabilitar cámara
        switch(fase){
            case 0:
                //#6 - Se acaba la primera fase y se pone la animación            
                //Show cthulu animation 1
                FindObjectOfType<CinematicsController>().PlayCurrentAnimationAndUpdateState();
                break;
            case 1:
                //#12 - Finaliza la segunda fase y ponemos segunda animación
                //Show cthulu animation 2
                FindObjectOfType<CinematicsController>().PlayCurrentAnimationAndUpdateState();
                break;
            case 2:
                //#21 - Activamos última animación
                //Show cthulu animation 3
                FindObjectOfType<CinematicsController>().PlayCurrentAnimationAndUpdateState();
                break;
        }
    }

    private List<InteractableObjectController> GetOutlineControllersByFase(int fase)
    {
        string faseNames = interactableObjets[fase];
        string[] faseNamesSplit = faseNames.Split(',');
        List<InteractableObjectController> outControllers = new List<InteractableObjectController>();
        foreach(string name in faseNamesSplit)
        {
            outControllers.Add(GameObject.Find(name).GetComponent<InteractableObjectController>());
        }
        return outControllers;
    }

    public void AddInteractableObjectByName(string name){
        InteractableObjectController obj = GameObject.Find(name).GetComponent<InteractableObjectController>();
        obj.isInteractable = true;
        currentFaseControllers.Add(obj);
    }
    public void RemoveInteractableObjectByName(string name){
        foreach(InteractableObjectController obj in currentFaseControllers){
            if(obj.name.Equals(name)){
                currentFaseControllers.Remove(obj);
            }
        }
    }

    private void SwitchInteractibleFromControllers(List<InteractableObjectController> outControllers, bool newState)
    {
        foreach (InteractableObjectController controller in outControllers)
        {
            controller.isInteractable = newState;
        }
    }

    public void UseKeyObject(int index){
        keyObjectFaseController[index] = true;
    }

    private bool IsPhaseCompleted(){
        for(int i = 0; i<keyObjectFaseController.Length; i++){
            if(!keyObjectFaseController[i]){
                return false;
            }
        }
        return true;
    }
}
