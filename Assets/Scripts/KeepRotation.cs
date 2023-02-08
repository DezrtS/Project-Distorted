using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepRotation : MonoBehaviour
{
    [SerializeField] bool stayUpright;
    private Quaternion startingRotation;

    private void Start()
    {
        if (stayUpright)
        {
            startingRotation = Quaternion.identity;
        } else
        {
            startingRotation = transform.rotation;
        }
    }

    private void Update()
    {
        transform.rotation = startingRotation;
    }
}
