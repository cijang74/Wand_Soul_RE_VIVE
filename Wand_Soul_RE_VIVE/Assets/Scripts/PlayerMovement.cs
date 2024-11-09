using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    [SerializeField] float Speed = 10f;
    [SerializeField] float jumpSpeed = 10f;
    CapsuleCollider2D myCapusleCollider;

    bool canDoubleJump;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCapusleCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        Run();
        FlipSprite();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {  
        if(value.isPressed)
        {
            if(myCapusleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                myRigidbody.velocity += new Vector2(0f, jumpSpeed);
                canDoubleJump = true;
            }
            else if(canDoubleJump)
            {
                myRigidbody.velocity += new Vector2(0f, jumpSpeed);
                canDoubleJump = false;
            }
        }

    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * Speed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
    }

    void FlipSprite()
    {
        bool playerHasHoizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHoizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

}
