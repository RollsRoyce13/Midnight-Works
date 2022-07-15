using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoinBehavior : MonoBehaviour
{
    private float rotationSpeed = 200.0f;

    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
