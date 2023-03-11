using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    private Rigidbody2D rig;
    private GameObject meleeWeaponPivot;
    private GameObject meleeWeaponImage;
    private MeleeWeaponType type;
    private float speed;
    private bool canBlock;
    private bool canParry;

    private bool isMoving;
    private bool isParrying;



    private void Start()
    {
        rig = GetComponentInParent<Rigidbody2D>();
        rig.centerOfMass = Vector2.zero;
        meleeWeaponPivot = gameObject;
        meleeWeaponImage = transform.Find("Image").gameObject;
    }

    public override void Aim()
    {
        if (HasTargetedPosition())
        {
            Vector3 targetPosition = GetTargetedPosition();
            float rotationTowardsTarget = Mathf.Atan2((rig.transform.position.y - targetPosition.y), (rig.transform.position.x - targetPosition.x)) * Mathf.Rad2Deg + 180;

            float rotationOfHand = meleeWeaponPivot.transform.rotation.eulerAngles.z;

            float angleDifference = 0;
            int direction = 1;

            if (rotationOfHand < rotationTowardsTarget && 360 - rotationTowardsTarget + rotationOfHand < rotationTowardsTarget - rotationOfHand)
            {
                angleDifference = 360 - rotationTowardsTarget + rotationOfHand;
                direction = -1;
            }
            else if (rotationOfHand > rotationTowardsTarget && 360 - rotationOfHand + rotationTowardsTarget < rotationOfHand - rotationTowardsTarget)
            {
                angleDifference = 360 - rotationOfHand + rotationTowardsTarget;
                direction = 1;
            }
            else if (rotationOfHand > rotationTowardsTarget)
            {
                angleDifference = rotationOfHand - rotationTowardsTarget;
                direction = -1;
            }
            else if (rotationOfHand < rotationTowardsTarget)
            {
                angleDifference = rotationTowardsTarget - rotationOfHand;
                direction = 1;
            }


            if (angleDifference > 0 && !isMoving)
            {
                isMoving = true;
                rig.angularVelocity = angleDifference * direction * speed;

                if (angleDifference > 20)
                {
                    if (direction > 0)
                    {
                        meleeWeaponImage.transform.localEulerAngles = new Vector3(0, 0, 0);
                    }
                    else if (direction < 0)
                    {
                        meleeWeaponImage.transform.localEulerAngles = new Vector3(180, 0, 0);
                    }
                }

                StartCoroutine(StopSwing());
            }
        }
    }
    

    public Rigidbody2D GetRigidbody()
    {
        return rig;
    }

    public void SetUpWeapon(MeleeWeaponType meleeWeaponType, GameObject meleeWeapon)
    {
        type = meleeWeaponType;
        base.SetUpWeapon(type.weaponClass, type.damage, type.knockback);
        speed = type.speed;
        canBlock = type.canBlock;
        canParry = type.canParry;
    }

    IEnumerator StopSwing()
    {
        yield return new WaitForSeconds(1 / speed);
        isMoving = false;
        rig.angularVelocity = 0;
    }

    IEnumerator DisableSwing()
    {
        isMoving = true;
        isParrying = true;
        rig.angularVelocity = 720;
        yield return new WaitForSeconds(0.25f);
        isMoving = false;
        isParrying = false;
        rig.angularVelocity = 0;
    }

    public void Parry()
    {
        StopAllCoroutines();
        StartCoroutine(DisableSwing());
    }
}
