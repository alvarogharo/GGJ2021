using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    private GameObject objectInInventory;
    // Start is called before the first frame update
    void Start()
    {
        objectInInventory = null;
    }

    public bool AddToInventory(GameObject go){
        if(objectInInventory == null){
            objectInInventory = go;
            //UpdateUI
            return true;
        }

        return false;        
    }

    public void RemoveInventory(){
        objectInInventory = null;
        //UpdateUI
    }

    public GameObject GetObject(){
        return objectInInventory;
    }
}
