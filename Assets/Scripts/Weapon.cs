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

public enum WeaponStates
{
    DROPPED,
    HELD,
    THROWN,
    CAUGHT,
    UNHOLDABLE,
    UNDROPABLE
}

public class Weapon : MonoBehaviour
{
    private WeaponClasses weaponClass;
    private WeaponStates weaponState;
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

    public void Drop()
    {
        transform.parent = null;
        gameObject.layer = 0;
        weaponState = WeaponStates.DROPPED; 
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.layer = 0;
        }
        transform.AddComponent<Rigidbody2D>().AddForce(transform.right * 25, ForceMode2D.Impulse);
    }

    public void PickUp(Entity entity)
    {
        Destroy(GetComponent<Rigidbody2D>());
        gameObject.layer = 8;
        weaponState = WeaponStates.HELD;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.layer = 8;
        }
        transform.parent = entity.transform;
        transform.position = entity.transform.position;
    }

    public WeaponStates GetWeaponState()
    {
        return weaponState;
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
        weaponState = WeaponStates.HELD;
        this.damage = damage;
        this.knockback = knockback;
        GameController.instance.AddWeaponToWorld(this);
    }
}
