using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject positionPin;
    // Update is called once per frame

    private void Start() {
        Destroy(this.gameObject, 3);
    }
    void Update()
    {
        this.transform.position = this.transform.position - new Vector3 (0, 0.04f, 0);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Floor")){
            if(positionPin){
                Instantiate(positionPin, this.transform.position, Quaternion.identity);
            }
            CharacterMovement cm = FindObjectOfType<CharacterMovement>();
            cm.SetPointToGo(this.transform.position);
            Destroy(this.gameObject);
        }
    }
}
