using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementComponent : MonoBehaviour
{
    Rigidbody2D rigidbody;

    [SerializeField]
    float speed = 5f;

    Vector2 movement;
    // Start is called before the first frame update


    [SerializeField]
    Animator animator;


    public bool block;
    public bool canMove = true;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.MovePosition(rigidbody.position + movement * speed * Time.fixedDeltaTime);
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!canMove)
            return;
        movement = context.ReadValue<Vector2>();
    }

    public void StopMovement()
    {
        canMove = false;
        movement = Vector2.zero;
    }

    public void EnableMovement()
    {
        canMove = true;
    }
}
