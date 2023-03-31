using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject followObject;
    private Vector3 offset;

    public void LoadData(GameData data)
    {
        offset = data.cameraOffset;
    }

    public void SaveData(ref GameData data)
    {
        data.cameraOffset = offset;
    }


    private void Start()
    {
        //offset = followObject.transform.position - transform.position;
    }

    private void Update()
    {
        transform.position = followObject.transform.position - offset;
    }
}
