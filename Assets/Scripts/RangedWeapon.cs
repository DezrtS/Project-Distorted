using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    private GameObject rangedWeaponPivot;
    //private GameObject rangedWeaponImage;
    private RangedWeaponType type;
    private float range;
    private bool arc;



    private void Start()
    {
        rangedWeaponPivot = gameObject;
    }

    public override void Aim()
    {
        if (HasTargetedPosition())
        {
            Vector3 targetPosition = GetTargetedPosition();
            float rotationTowardsTarget = Mathf.Atan2((transform.position.y - targetPosition.y), (transform.position.x - targetPosition.x)) * Mathf.Rad2Deg + 180;
            rangedWeaponPivot.transform.eulerAngles = new Vector3(0, 0, rotationTowardsTarget);
        }
    }

    public void SetUpWeapon(RangedWeaponType rangedWeaponType, GameObject rangedWeapon)
    {
        type = rangedWeaponType;
        base.SetUpWeapon(type.weaponClass, type.damage, type.knockback);
        range = type.range;
        arc = type.arc;
    }
}
