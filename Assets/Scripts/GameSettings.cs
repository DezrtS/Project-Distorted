using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings instance;
    [SerializeField] private float pixelsPerUnit;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public float GetPixelsPerUnitCount()
    {
        return pixelsPerUnit;
    }
}
