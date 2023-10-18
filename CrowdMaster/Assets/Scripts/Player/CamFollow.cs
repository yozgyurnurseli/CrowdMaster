using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField]
    Transform player;
    Transform myTransform;

    Vector3 pos;

    void Start()
    {
        myTransform = transform;
        pos = new Vector3();
    }

    void Update()
    {
        pos = player.localPosition;
        pos.x *= 0.67f;
        myTransform.localPosition = pos;
    }
}
