using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    private Rigidbody2D rig;

    [SerializeField] private GameObject swordPivot;
    //[SerializeField] private GameObject swordHitbox;

    [SerializeField] float rotationsPerSecond = 1;
    private bool isMoving = false;

    private Vector3 mousePosition;

    //[SerializeField] private LayerMask groundLayers;
    //private float swordReach;


    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.centerOfMass = Vector2.zero;
        mousePosition = Input.mousePosition;

        //swordReach = swordHitbox.transform.localScale.x;
    }

    void Update()
    {
        mousePosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Mathf.Abs(Camera.main.transform.position.z)));

        float rotationOfMouse = Mathf.Atan2((rig.transform.position.y - worldPosition.y), (rig.transform.position.x - worldPosition.x)) * Mathf.Rad2Deg + 180;
        
        /*
        RaycastHit2D ray = Physics2D.Linecast(transform.position, transform.position + swordPivot.transform.rotation * (Vector3.right * swordReach), groundLayers);
        if (ray)
        {
            swordHitbox.transform.localScale = new Vector3(ray.distance, swordHitbox.transform.localScale.y, swordHitbox.transform.localScale.z);
            swordHitbox.transform.localPosition = new Vector3(ray.distance / 2, 0, 0);
        } else
        {
            swordHitbox.transform.localScale = new Vector3(swordReach, swordHitbox.transform.localScale.y, swordHitbox.transform.localScale.z);
            swordHitbox.transform.localPosition = new Vector3(2, 0, 0);
        }
        */
        
        float rotationOfHand = swordPivot.transform.rotation.eulerAngles.z;

        float angleDifference = 0;
        int direction = 1;

        if (rotationOfHand < rotationOfMouse && 360 - rotationOfMouse + rotationOfHand < rotationOfMouse - rotationOfHand)
        {
            angleDifference = 360 - rotationOfMouse + rotationOfHand;
            direction = -1;
        }
        else if (rotationOfHand > rotationOfMouse && 360 - rotationOfHand + rotationOfMouse < rotationOfHand - rotationOfMouse)
        {
            angleDifference = 360 - rotationOfHand + rotationOfMouse;
            direction = 1;
        }
        else if (rotationOfHand > rotationOfMouse)
        {
            angleDifference = rotationOfHand - rotationOfMouse;
            direction = -1;
        }
        else if (rotationOfHand < rotationOfMouse)
        {
            angleDifference = rotationOfMouse - rotationOfHand;
            direction = 1;
        }


        if (angleDifference > 0 && !isMoving)
        {
            isMoving = true;
            rig.angularVelocity = angleDifference * direction * rotationsPerSecond;
            StartCoroutine(StopSwing());
        }
    }

    IEnumerator StopSwing()
    {
        yield return new WaitForSeconds(1 / rotationsPerSecond);
        isMoving = false;
        rig.angularVelocity = 0;
    }
}
