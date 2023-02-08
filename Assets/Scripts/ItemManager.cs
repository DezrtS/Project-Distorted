using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject basicSword;
    public GameObject basicAxe;
    public GameObject basicSpear;

    public GameObject basicEnemy;

    public List<EnemyType> enemyTypes = new List<EnemyType>();
    public List<MeleeWeaponType> meleeWeaponTypes = new List<MeleeWeaponType>();
    public List<RangeWeaponType> rangeWeaponTypes = new List<RangeWeaponType>();


}
