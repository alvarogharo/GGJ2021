using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.1f;
    [SerializeField]
    private GameObject pointTrailPrefab;
    [SerializeField]
    private bool moving = false;
    private Vector3 pointToGo;
    [SerializeField]
    private InteractableObjectController interactableObj;
    [SerializeField]
    private bool controlEnabled;
    private bool firstMovement = true;
    private bool secondMovement = true;

    // Start is called before the first frame update
    void Start()
    {
        moving = false;
        controlEnabled = false;
        firstMovement = true;
        secondMovement = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(controlEnabled){
            if(Input.GetMouseButtonDown(0)){   
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
                worldPosition.z = -7;
                RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector3.forward, Mathf.Infinity, 1 << LayerMask.NameToLayer("InteractableObj"));

                if(hit.collider != null){
                    GameObject go = hit.collider.gameObject;
                    if(!go.GetComponent<InteractableObjectController>().isInteractable){
                        interactableObj = null;
                    }
                }else{
                    interactableObj = null;
                }
                Instantiate(pointTrailPrefab, worldPosition, Quaternion.identity);   

                if(firstMovement){
                    //#3 - El primer click no se mueve pero dice una frase
                    FindObjectOfType<DialogueManager>().StartDialogue(0, 1);
                    firstMovement = false;
                }     
            }
        }

        if(moving){
            float distance = Vector3.Distance(this.transform.position, pointToGo);
            float multiplier = pointToGo.x > this.transform.position.x ? 1 : -1;
            if(distance > 0.1f){
                 this.transform.position = this.transform.position + new Vector3(speed * multiplier, 0f, 0f);
            }else{
                if(interactableObj != null){
                    interactableObj.Interact();
                    interactableObj = null;
                }
                moving = false;

                if(!firstMovement && secondMovement){
                    //#4 - Segundo clic frase cuando va allí
                    FindObjectOfType<DialogueManager>().StartDialogue(0, 2);
                    secondMovement = false;
                }
            }
        }
    }

    public void EnableControl(){controlEnabled = true;}
     public void DisableControl(bool stopNow = false){
        controlEnabled = false;

        if(stopNow){
            moving = false;
        }
    }
    public void SetPointToGo(Vector3 point){
        if(controlEnabled){
            pointToGo = new Vector3(point.x, this.transform.position.y, this.transform.position.z);
            moving = true;
        }
    }

    public void SetObject(InteractableObjectController obj){
        interactableObj = obj;
    }
}
