using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningSprite : MonoBehaviour
{

    public float rotationSpeed = 40f;

    //used for spinning chain dmamager sprite
    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
