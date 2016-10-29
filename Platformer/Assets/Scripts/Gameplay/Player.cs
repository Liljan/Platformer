﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    // components
    private Rigidbody2D mRb2d;
    private Animator mAnimator;
    public Transform mGroundChecker;
    private float mGroundCheckRadius = 0.1f;
    public LayerMask mWhatIsGround;

    // movement variables
    public float mSpeed = 1.0f;
    public int MAX_JUMPS = 2;
    public float mJumpForce;
    private int mJumps = 0;
    private bool mIsGrounded = false;

    // health variables
    public float MAX_HEALTH = 3.0f;
    private int mHealth;

    public void Awake()
    {
        mRb2d = GetComponent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();

        mIsGrounded = true;
        mHealth = MAX_HEALTH;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        CheckGrounded();
    }

    public void Update()
    {
        CheckHealth();
        Move();
    }

    private void CheckGrounded()
    {
        mIsGrounded = Physics2D.OverlapCircle(mGroundChecker.position, mGroundCheckRadius, mWhatIsGround);
        if (mIsGrounded)
            mJumps = 0;
    }

    private void CheckHealth()
    {
        if(mHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        float x;
        // check keybord input
        x = Input.GetAxis("Horizontal");

        mRb2d.velocity = new Vector2(mSpeed * x, mRb2d.velocity.y);

        SetFacingDirection(x);

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump button");
        }

        if (Input.GetButtonDown("Jump") && mJumps < MAX_JUMPS-1)
        {
            Jump();
        }
    }

    private void SetFacingDirection(float xAxis)
    {
        if (xAxis <= -0.1f)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (xAxis >= 0.1f)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    private void Jump()
    {
        mRb2d.velocity = new Vector2(mRb2d.velocity.x, mJumpForce);
        ++mJumps;
    }
}
