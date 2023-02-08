using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum WeaponClasses
{
    SWORD,
    AXE,
    SPEAR,
    BOW,
    KNIFE
}

public class Weapon : MonoBehaviour
{
    private WeaponClasses weaponClass;
    private float damage;
    private float knockback;

    private bool onlyTargetPosition;
    private Entity target = null;
    private Vector3 targetedPosition = Vector3.zero;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public bool HasTargetedPosition()
    {
        if (target.IsUnityNull())
        {
            return onlyTargetPosition;
        } else
        {
            return true;
        }
    }

    public Entity GetTarget()
    {
        return target;
    }

    public Vector3 GetTargetedPosition()
    {
        if (onlyTargetPosition)
        {
            return targetedPosition;
        } else
        {
            return target.transform.position;
        }
    }

    public void SetTarget(Entity target)
    {
        this.target = target;
    }

    public void SetTargetedPosition(Vector3 targetedPosition)
    {
        this.targetedPosition = targetedPosition;
    }

    public void OnlyTargetPosition(bool onlyTargetPosition)
    {
        this.onlyTargetPosition = onlyTargetPosition;
    }

    public void SetUpWeapon(WeaponClasses weaponClass, float damage, float knockback)
    {
        this.weaponClass = weaponClass;
        this.damage = damage;
        this.knockback = knockback;
    }
}
