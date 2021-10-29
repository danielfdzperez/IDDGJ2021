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
        }

        //Lo mismo para el player
    }

    public void Hit(HitType hitType)
    {
        this.hitType = hitType;
        ChangeDirection(bossPosition - (Vector2)transform.position);
        //Debug.DrawLine((Vector2)transform.position, (Vector2)transform.position + (), Color.yellow, 10);
    }
}
