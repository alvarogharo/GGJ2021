using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntratableFasesController : MonoBehaviour
{
    public string[] interactableObjets;
    public int currentFase = 0;
    private List<InteractableObjectController> currentFaseControllers;
    // Start is called before the first frame update
    private void Awake()
    {
        currentFaseControllers = GetOutlineControllersByFase(currentFase);
        EnableFase(currentFase);
    }
    public void EnableFase(int fase)
    {
        SwitchInteractibleFromControllers(currentFaseControllers, false);
        currentFaseControllers = GetOutlineControllersByFase(fase);
        SwitchInteractibleFromControllers(currentFaseControllers, true);
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

    private void SwitchInteractibleFromControllers(List<InteractableObjectController> outControllers, bool newState)
    {
        foreach (InteractableObjectController controller in outControllers)
        {
            controller.isInteractable = newState;
        }
    }
}
