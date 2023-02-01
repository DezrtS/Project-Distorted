using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rig;

    [SerializeField] private GameObject playerObject;

    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;

    [SerializeField] private float jumpPower;

    [SerializeField] private float drag;

    [SerializeField] private int groundLayer;
    private bool isGrounded = false;
    private bool canJump = true;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerObject.transform.rotation = Quaternion.identity;

        if (Input.GetKey(KeyCode.D))
        {
            if (rig.velocity.x + acceleration > maxSpeed)
            {
                rig.AddForce(new Vector2(maxSpeed - rig.velocity.x, 0), ForceMode2D.Impulse);
            }
            else
            {
                rig.AddForce(new Vector2(acceleration, 0), ForceMode2D.Impulse);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (Mathf.Abs(rig.velocity.x - acceleration) > maxSpeed)
            {
                rig.AddForce(new Vector2(-maxSpeed - rig.velocity.x, 0), ForceMode2D.Impulse);
            }
            else
            {
                rig.AddForce(new Vector2(-acceleration, 0), ForceMode2D.Impulse);
            }
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if ((isGrounded && canJump) || canJump)
            {
                canJump = false;
                rig.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }
        }

        if (rig.velocity.x > drag)
        {
            rig.AddForce(new Vector2(-drag, 0), ForceMode2D.Impulse);
        } else if (rig.velocity.x < -drag)
        {
            rig.AddForce(new Vector2(drag, 0), ForceMode2D.Impulse);
        } else
        {
            rig.AddForce(new Vector2(-rig.velocity.x, 0), ForceMode2D.Impulse);
        }
    }



    IEnumerator ExtendJump()
    {
        yield return new WaitForSeconds(0.15f);
        canJump = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == groundLayer)
        {
            Debug.Log("Collided");
            isGrounded = true;
            canJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == groundLayer)
        {
            isGrounded = false;
            if (canJump)
            {
                StartCoroutine(ExtendJump());
            }
        }
    }
}
