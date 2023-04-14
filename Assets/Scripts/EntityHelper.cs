using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHelper : MonoBehaviour
{
    public static void DamageEntity<TItem, TEntity>(TItem item, TEntity entity) where TItem : Item where TEntity : Entity
    {
        if (item is Weapon)
        {
            Weapon weapon = item as Weapon;
            entity.AddOrRemoveHealth(-weapon.GetDamage());
        }
    }
}
