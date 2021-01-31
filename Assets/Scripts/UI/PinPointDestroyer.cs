using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinPointDestroyer : MonoBehaviour
{
    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
