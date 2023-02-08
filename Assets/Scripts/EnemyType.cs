using UnityEngine;

[CreateAssetMenu]
public class EnemyType : ScriptableObject
{
    public string enemyName;
    public string enemyDiscription;
    public float maxHealth;
    [Range(0, 1)]
    public float damageReduction;
    public WeaponType weapon;
}
