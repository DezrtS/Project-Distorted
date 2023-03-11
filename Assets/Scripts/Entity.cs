using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityClass
{
    CREATURE,
    ITEM,
    OBJECT
}

public enum EntitySubClass
{
    PLAYER,
    ENEMY,
    ALLY,
    WEAPON,
    INTERACTABLE,
    OBSTACLE
}

public class Entity : MonoBehaviour
{
    [SerializeField] private EntityClass entityClass;
    [SerializeField] private EntitySubClass entitySubClass;
    private string entityName;
    private string entityDescription;
    private float maxHealth;
    private float health;
    [Range(0, 1)]
    private float damageReduction = 0;

    public void SetupEntity(EntityClass entityClass, EntitySubClass entitySubClass, float maxHealth, float damageReduction)
    {
        this.entityClass = entityClass;
        this.entitySubClass = entitySubClass;
        this.maxHealth = maxHealth;
        health = maxHealth;
        this.damageReduction = damageReduction;
        GameManager.instance.AddEntityToWorld(this);
    }

    void Update()
    {

    }

    public EntityClass GetEntityClass()
    {
        return entityClass;
    }

    public EntitySubClass GetEntitySubClass()
    {
        return entitySubClass;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetCurrentHealth()
    {
        return health;
    }

    public float GetDamageReduction()
    {
        return damageReduction;
    }

    public void SetDamageReduction(float damageReduction)
    {
        this.damageReduction = Mathf.Clamp(damageReduction, 0, 1); ;
    }

    public void AddOrRemoveHealth(float amount)
    {
        if (amount > 0)
        {
            health = Mathf.Min(health + amount, maxHealth);
        }
        else
        {
            amount = amount * (1 - damageReduction);
            health = Mathf.Max(health + amount, 0);
        }

        if (health == 0)
        {
            Kill();
            Debug.Log(name + " has died");
        }
    }

    public virtual void OnHit()
    {

    }

    public void Kill()
    {

    }
}
