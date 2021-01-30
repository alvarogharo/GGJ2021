using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.1f;
    [SerializeField]
    private GameObject pointTrailPrefab;
    private bool moving = false;
    private Vector3 pointToGo;
    private InteractableObjectController interactableObj;
    private bool controlEnabled;

    // Start is called before the first frame update
    void Start()
    {
        moving = false;
        controlEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(controlEnabled){
            if(Input.GetKey(KeyCode.A)){
                this.transform.position = this.transform.position - new Vector3(speed, 0f, 0f);
                moving = false;
            }else if(Input.GetKey(KeyCode.D)){
                this.transform.position = this.transform.position + new Vector3(speed, 0f, 0f);
                moving = false;
            }else if(Input.GetMouseButtonDown(0)){   
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
        pointToGo = new Vector3(point.x, this.transform.position.y, this.transform.position.z);
        moving = true;
    }

    public void SetObject(InteractableObjectController obj){
        interactableObj = obj;
    }
}
