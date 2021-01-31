using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInventory : MonoBehaviour
{
    [SerializeField]
    private GameObject objectInInventory;
    // Start is called before the first frame update
    void Start()
    {
        objectInInventory = null;
    }

    public bool AddToInventory(GameObject go){
        if(objectInInventory == null){
            objectInInventory = go;
            Image image = GameObject.FindGameObjectWithTag("InventoryUI").transform.GetChild(0).GetComponent<Image>();
            image.sprite = objectInInventory.GetComponent<SpriteRenderer>().sprite;
            //UpdateUI
            return true;
        }

        return false;        
    }

    public void RemoveInventory(){
        objectInInventory = null;
        Image image = GameObject.FindGameObjectWithTag("InventoryUI").transform.GetChild(0).GetComponent<Image>();
        image.sprite = null;
        //UpdateUI
    }

    public GameObject GetObject(){
        return objectInInventory;
    }
}
