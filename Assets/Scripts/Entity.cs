using UnityEngine;

public enum EntityTypes
{
    PLAYER,
    ENEMY,
    ALLY,
    OBJECT,
    DEBUG
}

public class Entity : MonoBehaviour
{
    private EntityTypes entityType;
    private float maxHealth;
    private float health;
    [Range(0, 1)]
    private float damageReduction;

    public void SetupEntity(EntityTypes entityType, float maxHealth, float damageReduction)
    {
        this.entityType = entityType;
        this.maxHealth = maxHealth;
        health = maxHealth;
        this.damageReduction = damageReduction;
        GameController.instance.AddEntityToWorld(this);
    }

    void Update()
    {
        
    }

    public EntityTypes GetEntityType()
    {
        return entityType;
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
        damageReduction = 1 - Mathf.Clamp(damageReduction, 0, 1);
        this.damageReduction = damageReduction;
    }

    public void AddOrRemoveHealth(float amount)
    {
        if (amount > 0)
        {
            health = Mathf.Min(health + amount, maxHealth);
        } else
        {
            amount = amount * damageReduction;
            health = Mathf.Max(health + amount, 0);
        }

        if (health == 0)
        {
            Kill();
            Debug.Log(name + " has died");
        }
    }

    public void Kill()
    {

    }
}
