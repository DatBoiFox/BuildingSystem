using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera camara;

    private void Start()
    {
        camara = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(camara.transform.position);
    }
}
