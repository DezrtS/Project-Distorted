using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [SerializeField] private CreatureManager creatureManager;
    [SerializeField] private ItemManager itemManager;
    [SerializeField] private ObjectManager objectManager;

    [SerializeField] private GameObject keybindsUI;

    [SerializeField] private List<Entity> entities = new List<Entity>();

    public List<Entity> Entities
    {
        get { return entities; }
        private set { entities = value; }
    }

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
        if (Input.GetKeyDown(KeyCode.T))
        {
            SpawnEnemy(Vector3.zero);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            keybindsUI.SetActive(!keybindsUI.activeInHierarchy);
        }
    }

    public void SpawnEnemy(Vector3 position)
    {
        GameObject enemyGameObject;
        enemyGameObject = Instantiate(creatureManager.basicEnemy, position, Quaternion.identity);
        enemyGameObject.GetComponent<Enemy>().SetupEntity(creatureManager.enemyTypes[0]);

    }

    public void AddEntityToWorld(Entity entity)
    {
        if (!entities.Contains(entity))
        {
            entities.Add(entity);
        }
    }

    /*
    public MeleeWeapon SpawnMeleeWeapon(Entity parent, MeleeWeaponType type)
    {
        GameObject weaponGameObject;
        MeleeWeapon weapon = null;
        if (type.weaponClass == WeaponClasses.SWORD)
        {
            weaponGameObject = Instantiate(itemManager.basicSword, parent.transform.position, Quaternion.identity, parent.transform);
            weaponGameObject.GetComponent<MeleeWeapon>().SetUpWeapon(type, weaponGameObject);
        }
        else
        {

        }
        return weapon;
    }
    */

    public MeleeWeapon SpawnRandomMeleeWeapon(Entity parent)
    {
        GameObject weaponGameObject;
        MeleeWeapon weapon = null;
        MeleeWeaponType type = itemManager.meleeWeaponTypes[0];
        if (type.weaponClass == WeaponClass.SWORD)
        {
            weaponGameObject = Instantiate(itemManager.basicSword, parent.transform.position, Quaternion.identity, parent.transform);
            weaponGameObject.GetComponent<MeleeWeapon>().SetUpWeapon(type, weaponGameObject);
            weapon = weaponGameObject.GetComponent<MeleeWeapon>();
        }
        else
        {
            Debug.Log("Melee weapon is null");
        }
        return weapon;
    }

    public List<Entity> GetAllEntitiesOfClass(EntityClass entityClass)
    {
        List<Entity> listOfEntities = new List<Entity>();
        foreach (Entity entity in entities)
        {
            if (entity.GetEntityClass() == entityClass)
            {
                listOfEntities.Add(entity);
            }
        }
        return listOfEntities;
    }

    public Entity GetClosestEntityOfClass(EntityClass entityClass, Entity originalEntity)
    {
        Vector3 position = originalEntity.transform.position;
        Entity closestEntity = null;
        float distance = int.MaxValue;
        foreach (Entity entity in entities)
        {
            if (entity.GetEntityClass() == entityClass && entity != originalEntity)
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

    public Entity GetClosestEntityOfSubClass(EntityClass entityClass, EntitySubClass entitySubClass, Entity originalEntity)
    {
        List<Entity> listOfEntities = GetAllEntitiesOfClass(entityClass);
        Vector3 position = originalEntity.transform.position;
        Entity closestEntity = null;
        float distance = int.MaxValue;
        foreach (Entity entity in listOfEntities)
        {
            if (entity.GetEntitySubClass() == entitySubClass && entity != originalEntity)
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

    public Item GetClosestItemOfStates(EntitySubClass entitySubClass, List<ItemState> states, Entity originalEntity)
    {
        List<Entity> items = GetAllEntitiesOfClass(EntityClass.ITEM);
        Vector3 position = originalEntity.transform.position;
        Item closestItem = null;
        float distance = int.MaxValue;
        foreach (Item item in items)
        {
            if (item.GetEntitySubClass() == entitySubClass && states.Contains(item.GetItemState()))
            {
                float entityDistance = (item.transform.position - position).magnitude;
                if (entityDistance < distance)
                {
                    distance = entityDistance;
                    closestItem = item;
                }
            }
        }
        return closestItem;
    }
}
