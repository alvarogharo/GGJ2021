using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    [Tooltip ("Speed the camera will move")]
    private float speed = 0.05f;

    [SerializeField]
    [Tooltip ("The left limit in the X Axis")]
    private float leftLimit;
    [SerializeField]
    [Tooltip ("The right limit in the X Axis")]
    private float rightLimit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.mousePosition.x < 100 && this.transform.position.x > leftLimit) {
            this.transform.position = this.transform.position - new Vector3(speed, 0f, 0f);
            if(this.transform.position.x < leftLimit){
                this.transform.position = new Vector3 (leftLimit, this.transform.position.y, this.transform.position.z);
            }
        }
        if ( Input.mousePosition.x >Screen.width -100 && this.transform.position.x < rightLimit) {
            this.transform.position = this.transform.position + new Vector3(speed, 0f, 0f);
            if(this.transform.position.x > rightLimit){
                this.transform.position = new Vector3 (rightLimit, this.transform.position.y, this.transform.position.z);
            }
        }
    }
}
