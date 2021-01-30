using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingMouse : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
         Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
         this.transform.position = new Vector3(worldPosition.x, worldPosition.y, -7);
    }
}
