using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    private Entity parent;
    private int maxLevel = 3;
    private int level;
    private float power = 20;
    private float range = 10;

    public Explosive(Entity parent, int level)
    {
        this.parent = parent;
        this.level = level;
    }

    public bool Upgrade()
    {
        int nextLevel = level + 1;
        level = Mathf.Min(nextLevel, maxLevel);
        return !(nextLevel > maxLevel);
    }

    public void Activate()
    {
        List<Entity> allEntities = GameManager.instance.GetAllEntitiesOfClass(EntityClass.CREATURE);
        foreach (Entity entity in allEntities)
        {
            if ((entity.transform.position - parent.transform.position).magnitude < range)
            {
                entity.GetComponent<Rigidbody2D>().AddForce((entity.transform.position - parent.transform.position).normalized * power, ForceMode2D.Impulse);
            }
        }
    }
}
