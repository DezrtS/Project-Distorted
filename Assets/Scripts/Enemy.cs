using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Creature
{
    private EnemyType type;
    private WeaponType weapon;
    private MeleeWeapon meleeWeapon;

    private void Start()
    {
        meleeWeapon.SetTarget(GameManager.instance.GetClosestEntityOfSubClass(EntityClass.CREATURE, EntitySubClass.PLAYER, this));
    }

    void Update()
    {

    }


    public void SetupEntity(EnemyType enemyType)
    {
        type = enemyType;
        base.SetupEntity(EntityClass.CREATURE, EntitySubClass.ENEMY, type.maxHealth, type.damageReduction);
        weapon = type.weapon;
        meleeWeapon = GameManager.instance.SpawnRandomMeleeWeapon(this);
    }
}
