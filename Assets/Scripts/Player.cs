using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Creature, IDataPersistence
{
    private Rigidbody2D rig;

    [SerializeField] private CreatureType playerType;

    [SerializeField] private bool overrideCreatureType = false;

    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;

    [SerializeField] private float jumpPower;

    [SerializeField] private float drag;

    private bool isGrounded = false;
    private bool canJump = true;

    Explosive explosive;

    void Start()
    {
        explosive = new Explosive(this, 1);
        rig = GetComponent<Rigidbody2D>();
        if (!overrideCreatureType)
        {
            maxSpeed = playerType.maxSpeed;
            acceleration = playerType.acceleration;
            jumpPower = playerType.jumpPower;
            drag = playerType.drag;
        }
        //SetupEntity();
    }

    public void LoadData(GameData data)
    {
        SetupEntity(data);
        transform.position = data.playerPosition;
    }

    public void SaveData(ref GameData data)
    {
        data.playerHealth = GetCurrentHealth();
        data.playerPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropHeldItem();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            PickupItem();
            if (HasItem())
            {
                if (GetHeldItem().GetEntitySubClass() == EntitySubClass.WEAPON)
                {
                    GetHeldItem().GetComponent<Weapon>().OnlyTargetPosition(true);
                }
            }
                
        }
        else if (Input.GetKeyDown(KeyCode.R) && !HasItem())
        {
            SetHeldItem(GameManager.instance.SpawnRandomMeleeWeapon(this));
            GetHeldItem().OnlyTargetPosition(true);
        }

        if (rig.velocity.x > drag)
        {
            rig.AddForce(new Vector2(-drag, 0), ForceMode2D.Impulse);
        }
        else if (rig.velocity.x < -drag)
        {
            rig.AddForce(new Vector2(drag, 0), ForceMode2D.Impulse);
        }
        else
        {
            rig.AddForce(new Vector2(-rig.velocity.x, 0), ForceMode2D.Impulse);
        }
        if (HasItem())
        {
            GetHeldItem().SetTargetedPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }



    IEnumerator ExtendJump()
    {
        yield return new WaitForSeconds(0.15f);
        canJump = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Blade")
        {
            //explosive.Activate();
        }

        if (collision.gameObject.tag == "Ground")
        {
            StopAllCoroutines();
            isGrounded = true;
            canJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
            if (canJump)
            {
                StartCoroutine(ExtendJump());
            }
        }
    }

    public void SetupEntity(GameData data)
    {
        base.SetupEntity(EntityClass.CREATURE, EntitySubClass.PLAYER, data.playerHealth, 0);
        //weapon = GameController.instance.SpawnRandomMeleeWeapon(this);
        //weapon.OnlyTargetPosition(true);
    }
}
