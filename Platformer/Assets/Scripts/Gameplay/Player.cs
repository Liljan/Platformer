using UnityEngine;
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
    public int MAX_HEALTH = 3;
    private int mHealth;

    // knockback variables
    public float mKnockBackSpeed;
    private bool mIsKnockBackRight;
    public float MAX_KNOCK_BACK_TIME;
    public float mKnockBackTimer = 0.0f;

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
        if (mKnockBackTimer <= 0.0f)
            Move();
        else
        {
            KnockBack();
        }

        mKnockBackTimer -= Time.deltaTime;
    }

    private void KnockBack()
    {
        if (mIsKnockBackRight)
        {
            mRb2d.velocity = new Vector2(-mKnockBackSpeed, mKnockBackSpeed);
        }
        else
        {
            mRb2d.velocity = new Vector2(mKnockBackSpeed, mKnockBackSpeed);
        }
    }

    public void EnableKnockBack() { mKnockBackTimer = MAX_KNOCK_BACK_TIME; }

    private void CheckGrounded()
    {
        mIsGrounded = Physics2D.OverlapCircle(mGroundChecker.position, mGroundCheckRadius, mWhatIsGround);
        if (mIsGrounded)
            mJumps = 0;

        mAnimator.SetBool("Grounded", mIsGrounded);
    }

    public void TakeDamage()
    {
        if (mHealth <= 0)
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

        if (Mathf.Abs(x) > 0.1f)
        {
            mAnimator.SetBool("Running", true);
        }
        else
        {
            mAnimator.SetBool("Running", false);
        }

        if (Input.GetButtonDown("Jump") && mJumps < MAX_JUMPS - 1)
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

    public void Jump()
    {
        mRb2d.velocity = new Vector2(mRb2d.velocity.x, mJumpForce);
        ++mJumps;
    }

    public bool GetIsGrounded()
    {
        return mIsGrounded;
    }

}