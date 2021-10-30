using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(ConstantForce2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour, HittableInterface
{
    Rigidbody2D rb;
    float speed;

    bool returned = false;

    HitType hitType;
    public Vector2 bossPosition;

    int playerCollidersHit = 0; //Guarrada maestra, el player tiene 2 colliders si le golpeamos tenemos que atravesar los 2.
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    public void ChangeDirection(Vector2 dir)
    {
        dir.Normalize();
        SetDirectionAndSpeed(dir, speed);
    }


    public void SetDirectionAndSpeed(Vector2 dir, float speed)
    {
        dir.Normalize();
        this.speed = speed;
        rb.velocity = dir * speed;
    }

    public void Returned()
    {
        returned = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Boss boss = collision.GetComponent<Boss>();
        if (boss && returned)
        {
            boss.Hit(hitType);
            DestroyByImpact();
        }
        else
        {
            PlayerCombat player = collision.GetComponent<PlayerCombat>();
            if(player)
            {
                player.Hit(hitType);
                playerCollidersHit++;
                if(playerCollidersHit == 2)
                    DestroyByImpact();
            }
        }
        //Lo mismo para el player
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerCombat player = collision.GetComponent<PlayerCombat>();
        if (player)
        {
            playerCollidersHit--;
        }
    }

    public void Hit(HitType hitType)
    {
        this.hitType = hitType;
        ChangeDirection(bossPosition - (Vector2)transform.position);
        //Debug.DrawLine((Vector2)transform.position, (Vector2)transform.position + (), Color.yellow, 10);
    }

    void DestroyByImpact()
    {
        Destroy(gameObject);
    }
}
