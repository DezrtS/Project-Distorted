using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject followObject;
    private Vector3 offset;

    private void Start()
    {
        offset = followObject.transform.position - transform.position;
    }

    private void Update()
    {
        transform.position = followObject.transform.position - offset;
    }
}
