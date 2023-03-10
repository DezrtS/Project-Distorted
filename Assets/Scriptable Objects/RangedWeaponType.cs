using UnityEngine;

[CreateAssetMenu]
public class RangedWeaponType : WeaponType
{
    public float range;
    [Range(0, 1)]
    public float accuracy;
    public bool arc;
}
