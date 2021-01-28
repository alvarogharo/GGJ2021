using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingMovement : MonoBehaviour
{
    private bool moving = false;
    private Vector3 pointToGo;
    public float speed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        moving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A)){
            this.transform.position = this.transform.position - new Vector3(speed, 0f, 0f);
            moving = false;
        }else if(Input.GetKey(KeyCode.D)){
            this.transform.position = this.transform.position + new Vector3(speed, 0f, 0f);
            moving = false;
        }else if(Input.GetMouseButtonDown(0)){            
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pointToGo = new Vector3(worldPosition.x, 0, 0);
            moving = true;
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
}
