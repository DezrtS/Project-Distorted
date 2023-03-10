using UnityEngine;

public class CreatureType : EntityType
{
    public string creatureName;
    public string creatureDescription;

    public float maxHealth;
    [Range(0, 1)]
    public float damageReduction;

    public float maxSpeed;
    public float acceleration;
    public float drag;
    public float jumpPower;
}
