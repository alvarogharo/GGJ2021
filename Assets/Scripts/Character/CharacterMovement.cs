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
    private bool moveNow = true;
    private Vector3 pointToGo;
    [SerializeField]
    private InteractableObjectController interactableObj;
    [SerializeField]
    private bool controlEnabled;
    private bool firstMovement = true;
    private bool secondMovement = true;

    private float objectOffset = 0.75f;
    private float moveOffset = 0.1f;

    //References to components
    private Animator anim;

    private void Awake() {
        anim = GetComponent<Animator>();
        anim.SetBool("Sitted", true);
    }
    // Start is called before the first frame update
    void Start()
    {
        moving = false;
        controlEnabled = false;
        firstMovement = true;
        secondMovement = true;
        StartCoroutine(ScratchLoop());
    }

    // Update is called once per frame
    void Update()
    {
        if(controlEnabled && moveNow){
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
                    anim.SetBool("Sitted", false);
                    firstMovement = false;
                }     
            }
        }

        if(moving){
            float distance = Vector3.Distance(this.transform.position, pointToGo);
            float multiplier = pointToGo.x > this.transform.position.x ? 1 : -1;
            float offset = interactableObj != null ? objectOffset : moveOffset;
            if(distance > offset){
                 this.transform.position = this.transform.position + new Vector3(speed * multiplier, 0, 0);
            }else{
                if(interactableObj != null){
                    interactableObj.Interact();
                    interactableObj = null;
                }
                moving = false;
                anim.SetBool("Walking", false);

                if(!firstMovement && secondMovement){
                    //#4 - Segundo clic frase cuando va allí
                    FindObjectOfType<DialogueManager>().StartDialogue(0, 2);
                    secondMovement = false;
                }
            }
        }

        moveNow = true;
    }

    IEnumerator ScratchLoop(){
        while(true){
            float seconds = Random.Range(10f, 20f);
            yield return new WaitForSeconds(seconds);
            anim.SetTrigger("Scratch");
        }
    }

    public void EnableControl(){controlEnabled = true; moveNow = false;}
     public void DisableControl(bool stopNow = false){
        controlEnabled = false;

        if(stopNow){
            moving = false;
            anim.SetBool("Walking", false);
        }
    }
    public void SetPointToGo(Vector3 point){
        if(controlEnabled){
            pointToGo = new Vector3(point.x, this.transform.position.y, this.transform.position.z);
            GetComponent<SpriteRenderer>().flipX = transform.position.x < pointToGo.x;
            moving = true;
            anim.SetBool("Walking", true);
        }
    }

    public void SetObject(InteractableObjectController obj){
        interactableObj = obj;
    }
}
