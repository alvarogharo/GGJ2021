using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntratableFasesController : MonoBehaviour
{
    public string[] interactableObjets;
    public int currentFase = 0;
    private List<OutlineController> currentFaseControllers;
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
    }

    private List<OutlineController> GetOutlineControllersByFase(int fase)
    {
        string faseNames = interactableObjets[fase];
        string[] faseNamesSplit = faseNames.Split(',');
        List<OutlineController> outControllers = new List<OutlineController>();
        foreach(string name in faseNamesSplit)
        {
            outControllers.Add(GameObject.Find(name).GetComponent<OutlineController>());
        }
        return outControllers;
    }

    private void SwitchInteractibleFromControllers(List<OutlineController> outControllers, bool newState)
    {
        foreach (OutlineController controller in outControllers)
        {
            controller.isInteractable = newState;
        }
    }
}
