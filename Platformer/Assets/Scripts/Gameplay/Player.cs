using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    // components
    private Rigidbody2D mRb2d;
    private Animator mAnimator;
    private SpriteRenderer mSpriteRenderer;
    public Transform mGroundChecker;
    private float mGroundCheckRadius = 0.1f;
    public LayerMask mWhatIsGround;

    // movement variables
    public float mSpeed = 1.0f;
    public int MAX_JUMPS = 2;
    public float mJumpForce;
    private int mJumps = 0;
    private bool mIsGrounded = false;

    // ladder variables
    private bool mIsOnLadder = false;
    public float mClimbSpeed;
    private float mGravityStore;

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
        mSpriteRenderer = GetComponent<SpriteRenderer>();

        mIsGrounded = true;
        mHealth = MAX_HEALTH;

        mGravityStore = mRb2d.gravityScale;
    }

    private IEnumerator DamageFlash(float dt)
    {
        mSpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(dt);
        mSpriteRenderer.color = Color.white;
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        CheckGrounded();
    }

    public void Update()
    {
        if (!mIsOnLadder)
        {
            if (mKnockBackTimer <= 0.0f)
                Move();
            else
            {
                KnockBack();
            }
        }
        else
        {
            MoveLadder();
        }

        UpdateAnimations();

        mKnockBackTimer -= Time.deltaTime;
    }

    private void KnockBack()
    {
        if (mIsKnockBackRight)
        {
            //mRb2d.velocity = new Vector2(-mKnockBackSpeed, mKnockBackSpeed);
            mRb2d.velocity = Vector2.left * mKnockBackSpeed;
        }
        else
        {
            // mRb2d.velocity = new Vector2(mKnockBackSpeed, mKnockBackSpeed);
            mRb2d.velocity = Vector2.right * mKnockBackSpeed;
        }
    }

    public void EnableIsOnLadder(bool b)
    {
        mIsOnLadder = b;
        if (mIsOnLadder)
        {
            mRb2d.gravityScale = 0.0f;
        }
        else
            mRb2d.gravityScale = mGravityStore;
    }

    public void EnableKnockBack() { mKnockBackTimer = MAX_KNOCK_BACK_TIME; }

    private void CheckGrounded()
    {
        mIsGrounded = Physics2D.OverlapCircle(mGroundChecker.position, mGroundCheckRadius, mWhatIsGround);
        if (mIsGrounded)
            mJumps = 0;

        mAnimator.SetBool("Grounded", mIsGrounded);
    }

    public void TakeDamage(int dmg)
    {
        mHealth -= dmg;

        if (mHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void AddHealth(int h)
    {
        mHealth += h;
    }

    private void UpdateAnimations()
    {
        mAnimator.SetBool("Running", Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f);

        mAnimator.SetBool("OnLadder", mIsOnLadder);

        mAnimator.SetBool("Climbing", mIsOnLadder && Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f);
    }

    private void MoveLadder()
    {
        //mRb2d.velocity = Vector2.zero;
        float x = Input.GetAxis("Horizontal") * (mIsGrounded ? mSpeed : mClimbSpeed);
        float y = Input.GetAxis("Vertical") * mClimbSpeed;
        mRb2d.velocity = new Vector2(x, y);
    }

    private void Move()
    {
        float x;
        // check keybord input
        x = Input.GetAxis("Horizontal");

        mRb2d.velocity = new Vector2(mSpeed * x, mRb2d.velocity.y);

        SetFacingDirection(x);

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

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
            mIsKnockBackRight = transform.position.x < other.transform.position.x;
            EnableKnockBack();
            StartCoroutine(DamageFlash(0.5f * mKnockBackTimer));
        }
    }


}