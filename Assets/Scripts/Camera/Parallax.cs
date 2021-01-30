using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class Parallax : MonoBehaviour{

    public Camera camera;

    private float smooth = 0f;

    public Vector3 realPosition;
    public Vector3 correctedPosition;


    private float A;
    private float B;
    private float C;
    private float D;
    
    void Start(){
        realPosition = transform.position;
        A = 0; B = 0; C = 0; D = 0;
    }

    void Update(){
        float correctedX = calculateCorrectedX();
        correctedPosition = new Vector3(correctedX, transform.position.y, transform.position.z);
        transform.position = correctedPosition;
    }

    public float calculateCorrectedX(){

        /*

                                [object]
                                  /|
                                /  |
                             /     |
                          / |      |
                       /    | (A)  | B
         [camera]() /_ _ _ _| _ _ _|        [z axis]
                        C
                            D

        */

        int sign = 1;

        C = Mathf.Abs(camera.transform.position.z);
        D = C + Mathf.Abs(transform.position.z);
        B = realPosition.x - camera.transform.position.x;
        sign = B > 0 ? 1 : -1; 
        B = Mathf.Abs(B);

        A = (B*C)/D;

        A = A + ( (A - B) * smooth );

        A = A * sign;

        return A;
    }


}