using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CajonController : MonoBehaviour
{
    [SerializeField]
    private PhaseAction[] phaseActions;
    private int maxTimes;
    // Start is called before the first frame update
    void Start()
    {
        maxTimes = FindObjectsOfType<Cajon>().Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Used(){
        //#14 - Al llegar al último cajón añade llave de despensa
        phaseActions[0].UseItem();
    }
}
