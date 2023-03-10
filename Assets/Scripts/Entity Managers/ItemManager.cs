using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject basicSword;
    public GameObject basicAxe;
    public GameObject basicSpear;

    public List<MeleeWeaponType> meleeWeaponTypes = new List<MeleeWeaponType>();
    public List<RangedWeaponType> rangeWeaponTypes = new List<RangedWeaponType>();
}
