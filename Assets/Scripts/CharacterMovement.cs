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
                Instantiate(pointTrailPrefab, worldPosition, Quaternion.identity);        
                /*Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pointToGo = new Vector3(worldPosition.x, 0, 0);
                moving = true;*/
            }
        }

        if(moving){
            float distance = Vector3.Distance(this.transform.position, pointToGo);
            float multiplier = pointToGo.x > this.transform.position.x ? 1 : -1;
            if(distance > 0.1f){
                 this.transform.position = this.transform.position + new Vector3(speed * multiplier, 0f, 0f);
            }else{
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
}
