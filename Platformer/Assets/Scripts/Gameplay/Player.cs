using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    // components
    private Rigidbody2D mRb2d;


    // movement variables
    public float mSpeed = 1.0f;
    private int mJumps = 0;


    public void Awake()
    {
        mRb2d = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float x;
        // check keybord input
        x = Input.GetAxis("Horizontal");

        mRb2d.velocity = Vector2.right * mSpeed * x * Time.deltaTime;

        SetFacingDirection(x);
    }

    private void SetFacingDirection(float xAxis)
    {
        if (xAxis <= -0.1f)
            transform.localScale = new Vector3(-1.0f,1.0f,1.0f);
        else if(xAxis >= 0.1f)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }


    private void Jump()
    {

    }
}
