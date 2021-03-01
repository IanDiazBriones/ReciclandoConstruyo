using Lean.Gui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;
    protected LeanJoystick Joystick;

    private void Start()
    {
        Joystick = FindObjectOfType<LeanJoystick>();
    }
    // Update is called once per frame
    void Update()
    {
        // Input

        movement = Joystick.Handle.localPosition;

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
    	// Movement
    	rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
