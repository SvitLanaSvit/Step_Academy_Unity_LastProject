using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nijia : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    public float speed, jumpForse, moveInput;

    public bool isGrounded = true;
    bool jumpb, facingRight = true;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpsValue;

    public Transform position;

    public int countCoin = 0;
    public Text scoreText;

    public Transform getPosition()
    {
        return position;
    }

    void Start()
    {
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        playAnimation();
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }

    void playAnimation()
    {
        if (moveInput != 0)
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void Update()
    {
        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForse;
            extraJumps--;
            anim.SetBool("Jump", true);
            isGrounded = false;
            Debug.Log("isGrounded: " + isGrounded);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForse;
            anim.SetBool("Jump", true);
            isGrounded = false;
            Debug.Log("isGrounded?: " + isGrounded);
        }

        if (isGrounded)
        {
            Debug.Log("isGrounded: " + isGrounded);
            anim.SetBool("Jump", false);
        }
        else if (!isGrounded)
        {
            Debug.Log("!isGrounded: " + isGrounded);
            anim.SetBool("Jump", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            countCoin++;
            scoreText.text = "Count of coins: " + countCoin;
        }
    }
}
