using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform _cam;
    private Vector3 offSet;
    void Start()
    {
        _cam = Camera.main.transform;
        offSet = _cam.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _cam.position = transform.position + offSet;
    }
}
