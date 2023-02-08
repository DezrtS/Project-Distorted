using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    [SerializeField] private ItemManager itemManager;
    [SerializeField] private List<Entity> entities = new List<Entity>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Time.timeScale = Mathf.Min(1, Time.timeScale + 0.1f);
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        } 
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Time.timeScale = Mathf.Max(0.1f, Time.timeScale - 0.1f);
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnEnemy(Vector3.zero);
        }
    }

    public void SpawnEnemy(Vector3 position)
    {
        GameObject enemyGameObject;
        enemyGameObject = Instantiate(itemManager.basicEnemy, position, Quaternion.identity);
        enemyGameObject.GetComponent<Enemy>().SetupEntity(itemManager.enemyTypes[0]);

    }

    public void AddEntityToWorld(Entity entity)
    {
        if (!entities.Contains(entity))
        {
            entities.Add(entity);
        }
    }

    public MeleeWeapon SpawnMeleeWeapon(Entity parent, MeleeWeaponType type)
    {
        GameObject weaponGameObject;
        MeleeWeapon weapon = null;
        if (type.weaponClass == WeaponClasses.SWORD)
        {
            weaponGameObject = Instantiate(itemManager.basicSword, parent.transform.position, Quaternion.identity, parent.transform);
            weaponGameObject.GetComponent<Sword>().SetUpWeapon(type, weaponGameObject);
        } else
        {

        }
        return weapon;
    }

    public MeleeWeapon SpawnRandomMeleeWeapon(Entity parent)
    {
        GameObject weaponGameObject;
        MeleeWeapon weapon = null;
        MeleeWeaponType type = itemManager.meleeWeaponTypes[0];
        if (type.weaponClass == WeaponClasses.SWORD)
        {
            weaponGameObject = Instantiate(itemManager.basicSword, parent.transform.position, Quaternion.identity, parent.transform);
            weaponGameObject.GetComponent<Sword>().SetUpWeapon(type, weaponGameObject);
            weapon = weaponGameObject.GetComponent<MeleeWeapon>();
        }
        else
        {
            Debug.Log("Melee weapon is null");
        }
        return weapon;
    }

    public Entity GetClosestEntityOfTypes(List<EntityTypes> types, Entity originalEntity)
    {
        Vector3 position = originalEntity.transform.position;
        Entity closestEntity = null;
        float distance = int.MaxValue;
        foreach (Entity entity in entities)
        {
            if (types.Contains(entity.GetEntityType()) && entity != originalEntity)
            {
                float entityDistance = (entity.transform.position - position).magnitude;
                if (entityDistance < distance)
                {
                    distance = entityDistance;
                    closestEntity = entity;
                }
            }
        }
        return closestEntity;
    }
}
