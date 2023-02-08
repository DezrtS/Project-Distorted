using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Enemy : Entity
{
    private EnemyType type;
    private WeaponType weapon;
    private MeleeWeapon meleeWeapon;

    private void Start()
    {
        meleeWeapon.SetTarget(GameController.instance.GetClosestEntityOfTypes(new List<EntityTypes>() { EntityTypes.PLAYER }, this));
    }

    void Update()
    {
        
    }

    
    public void SetupEntity(EnemyType enemyType)
    {
        type = enemyType;
        base.SetupEntity(EntityTypes.ENEMY, type.maxHealth, type.damageReduction);
        weapon = type.weapon;
        meleeWeapon = GameController.instance.SpawnRandomMeleeWeapon(this);
    }
}
