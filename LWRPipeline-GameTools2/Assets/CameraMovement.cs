using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class CameraMovement : MonoBehaviour
{
    public Transform JasperTransform;

    public Vector3 CameraOffset;
    private void Start()
    {
        
    }

    private void Update()
    {
        transform.position = JasperTransform.position - CameraOffset;
        transform.LookAt(JasperTransform);
    }
}
