using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Movement variables
    public float movementSpeed = 5.0f;
    public Rigidbody2D m_RigidBody;
    public float jumpForce = 22f;
    public float sprintSpeed = 2.5f;
    private float currentSpeed = 0f;
    // Ground check variables
    private bool m_isGrounded;
    public Transform groundCheckPoint; // the position to check from if we are on ground
    public float groundCheckRadius; // radius from the point to check if we are on ground
    public LayerMask whatIsGround; // Setting the layers that will be ground
    // Double jump variables
    private bool m_canDoubleJump;
    private bool m_inDoubleJump;

    public Animator m_anim;

    // Knockback variables
    public float m_knockBackLength, m_knockBackSpeed;
    private float m_knockBackCounter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_canDoubleJump = false;
        m_inDoubleJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        checkIfGrounded();
        if (m_knockBackCounter <= 0)
        {
            MovingPlayer();
            directionHandle();
        }
        else
        {
            knockingBackHandle();
        }
            animationHandle();
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /* knockingBackHandle - handling the knockback timer */
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void knockingBackHandle()
    {
        m_knockBackCounter -= Time.deltaTime;

        m_RigidBody.linearVelocity = new Vector2(-transform.localScale.x * m_knockBackSpeed, m_RigidBody.linearVelocityY);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /* MovingPlayer - gets player input of movement in order to move the player */
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void MovingPlayer()
    {
        currentSpeed = movementSpeed;

        sprint();

        setSpeed();

        if (Input.GetButtonDown("Jump"))
        {
            jump();
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /* directionHandle - facing the player to the direction its moving */
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void directionHandle()
    {
        if (m_RigidBody.linearVelocityX > 0) // moving right
        {
            transform.localScale = Vector3.one;
        }
        else if (m_RigidBody.linearVelocityX < 0) // changing from right to left
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /* animationHandle - setting all the values of the animation states */
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void animationHandle()
    {
        m_anim.SetFloat("speed", Mathf.Abs(m_RigidBody.linearVelocityX));
        m_anim.SetFloat("ySpeed", m_RigidBody.linearVelocityY);
        m_anim.SetBool("isGrounded", m_isGrounded);
        m_anim.SetBool("isDoubleJump", m_inDoubleJump);
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /* sprint - checking if leftShift is held down and adding speed */
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void sprint()
    {
        // while LeftShift is held down
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed += sprintSpeed;
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /* jump - checking if to set single or double jump */
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void jump()
    {
        // if we clicked the spacebar
        if (m_isGrounded)
        {
            jumpAction();
            m_canDoubleJump = true;
            m_inDoubleJump = false;
        }
        else if (m_canDoubleJump)
        {
            jumpAction();
            m_canDoubleJump = false;
            m_inDoubleJump = true;
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /* checkIfGrounded - checking if the player is on a ground layer surface */
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void checkIfGrounded()
    {
        m_isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);
        if(m_isGrounded)
        {
            m_inDoubleJump = false;
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /* setSpeed - chenging the x velocity to move */
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void setSpeed()
    {
        m_RigidBody.linearVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * currentSpeed, m_RigidBody.linearVelocityY);
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /* jumpAction - chenging the y velocity to jump */
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void jumpAction()
    {
        m_RigidBody.linearVelocity = new Vector2(m_RigidBody.linearVelocityX, jumpForce);
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /* KnockBack - applying knockback to the player */
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void KnockBack()
    {
        m_RigidBody.linearVelocity = new Vector2(0f, jumpForce * 0.5f);
        m_anim.SetTrigger("isKnockingBack");

        m_knockBackCounter = m_knockBackLength; // start the knockback counter
    }
}
