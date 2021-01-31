using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Standard parameters")]
    [SerializeField]
    [Tooltip ("Speed the camera will move")]
    private float speed = 0.05f;

    [SerializeField]
    [Tooltip ("The left limit in the X Axis")]
    private float leftLimit;
    [SerializeField]
    [Tooltip ("The right limit in the X Axis")]
    private float rightLimit;
    private bool controlEnabled;

    private Transform playerTransform;

    [Header("Dialogue parameters")]
    [SerializeField]
    private float marginFromCenter = 3;
    [SerializeField]
    private float speedRelocate = 1.5f;

    private void Awake() {
        playerTransform = FindObjectOfType<CharacterMovement>().transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        controlEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(controlEnabled){
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
        }else{
            bool placed = false;
            if(playerTransform.position.x > transform.position.x - marginFromCenter && playerTransform.position.x < transform.position.x + marginFromCenter){
                placed = true;
            }
            if(!placed){
                transform.position = Vector3.Lerp (transform.position, new Vector3 (playerTransform.position.x, transform.position.y, transform.position.z), Time.deltaTime * speedRelocate);
            }
        }
    }

    public void EnableControl(){controlEnabled = true;}
    public void DisableControl(){controlEnabled = false;}
}
