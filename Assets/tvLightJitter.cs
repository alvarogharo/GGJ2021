using UnityEngine.Experimental.Rendering.Universal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class tvLightJitter : MonoBehaviour
{
    [Tooltip("1 - base value, 2 - increment")]
    public Vector2 outerRadius;
    public float speed = 1.0f;
    public Color lightColor;

    // Start is called before the first frame update
    void Start()
    {
        
        lightColor = gameObject.GetComponent<Light2D>().color;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Light2D>().intensity =  outerRadius.x + Mathf.PingPong(Time.time*speed, outerRadius.y);
        lightColor = newHUE();
    }


    private Color newHUE(){
        float hue;
        float sat;
        float val;

        Color.RGBToHSV(lightColor, out hue, out sat, out val); 
        //HSVToRGB()

        return Color.HSVToRGB(Random.value, sat, val);
    }
}
