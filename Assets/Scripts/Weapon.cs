using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum WeaponClass
{
    SWORD,
    AXE,
    SPEAR,
    BOW,
    KNIFE
}

public class Weapon : Item
{
    private WeaponClass weaponClass;
    private float damage;
    private float knockback;

    public override void Drop()
    {
        transform.parent = null;
        SetTarget(null);
        OnlyTargetPosition(false);
        gameObject.layer = 0;
        SetItemState(ItemState.DROPPED);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.layer = 0;
        }
        transform.AddComponent<Rigidbody2D>().AddForce(transform.right * 25, ForceMode2D.Impulse);
    }

    public override void PickedUp(Entity entity)
    {
        Destroy(GetComponent<Rigidbody2D>());
        gameObject.layer = 8;
        SetItemState(ItemState.HELD);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.layer = 8;
        }
        transform.parent = entity.transform;
        transform.position = entity.transform.position;
    }

    public void SetUpWeapon(WeaponClass weaponClass, float damage, float knockback)
    {
        this.weaponClass = weaponClass;
        SetItemState(ItemState.HELD);
        this.damage = damage;
        this.knockback = knockback;
        GameManager.instance.AddItemToWorld(this);
    }
}
