using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntratableFasesController : MonoBehaviour
{
    public string[] interactableObjets;
    public int currentFase = 0;
    public int[] numberOfKeyObjectPerPhase;
    private List<InteractableObjectController> currentFaseControllers;
    private bool[] keyObjectFaseController;
    // Start is called before the first frame update
    private void Awake()
    {
        keyObjectFaseController = new bool[numberOfKeyObjectPerPhase[currentFase]];
        for(int i = 0; i<keyObjectFaseController.Length; i++){
            keyObjectFaseController[i] = false;
        }
        currentFaseControllers = GetOutlineControllersByFase(currentFase);
        //EnableFase(currentFase);
    }

    private void Update() {
        for(int i = 0; i<keyObjectFaseController.Length; i++){
            if(!keyObjectFaseController[i]){
                return;
            }
        }

        EnableFase(currentFase+1);
    }
    public void EnableFase(int fase)
    {
        SwitchInteractibleFromControllers(currentFaseControllers, false);
        currentFaseControllers = GetOutlineControllersByFase(fase);
        SwitchInteractibleFromControllers(currentFaseControllers, true);
        keyObjectFaseController = new bool[numberOfKeyObjectPerPhase[currentFase]];
        for(int i = 0; i<keyObjectFaseController.Length; i++){
            keyObjectFaseController[i] = false;
        }
        currentFase = fase;
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
}
