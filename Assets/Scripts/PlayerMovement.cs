using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 20f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(20f, 20f);

    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBody;
    BoxCollider2D myFeet;
    [SerializeField] bool isAlive = true;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        this.myAnimator = GetComponent<Animator>();
        this.myBody = GetComponent<CapsuleCollider2D>();
        this.myFeet = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (!isAlive) return;
        Run();
        FlipPlayer();
        Climbing();
        Die();
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) return;
        moveInput = value.Get<Vector2>();
    }
    void OnJump(InputValue value)
    {
        if (!isAlive) return;
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;
        if(value.isPressed)
        {
            myRigidbody.velocity += new Vector2(0, jumpSpeed);
        }
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) return;
        Instantiate(bullet, gun.position, transform.rotation);
    }
    void Run()
    {
            Vector2 playerMove = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
            myRigidbody.velocity = playerMove;

            bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
            myAnimator.SetBool("isRunning" ,playerHasHorizontalSpeed);
        
    }
    void FlipPlayer()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) >Mathf.Epsilon;
        if(playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1);
        }
    }
    void Climbing()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRigidbody.gravityScale = 5f;
            myAnimator.SetBool("isClimbing" , false);
            return;
        }

        Vector2 climb = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
        myRigidbody.velocity = climb;
        myRigidbody.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);

    }

    void Die()
    {
        if (myBody.IsTouchingLayers(LayerMask.GetMask("Enemies" , "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidbody.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

}
