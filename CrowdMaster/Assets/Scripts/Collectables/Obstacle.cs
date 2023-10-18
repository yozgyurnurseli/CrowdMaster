using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.forward * 7.5f);
    }
}
