using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float playerHealth;
    public Vector3 playerPosition;
    public Vector3 cameraOffset;

    public GameData()
    {
        this.playerPosition = Vector3.zero;
        this.cameraOffset = new Vector3(0, -5, 10);
        this.playerHealth = 100;
    }


}
