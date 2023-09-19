using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Running")]
    [Tooltip("Determines how fast the player can move")]
    [SerializeField] private float runSpeed = 7f;
    [Tooltip("Determines how fast the player will stop moving horizontally on the ground")]
    [SerializeField] private float groundStopSpeed = 1.5f;

    [Header("Jumping")]
    [Tooltip("Determines how high the player can jump")]
    [SerializeField] private float jumpPower = 12f;
    [Tooltip("Determines whether the player can perform wall jumps or not")] 
    [SerializeField] private bool canWallJump = true;
    [Tooltip("Determines how far off the wall the player will go when performing a wall jump")]
    [SerializeField] private float wallJumpPower = 10f;
    [Range(0f,1f)] [Tooltip("A temporary penalty to air control after performing a wall jump")]
    [SerializeField] private float wallJumpPenaltyMultiplier = 0.01f;
    [Tooltip("How long the air control penalty will be applied for (in seconds)")]
    [SerializeField] private float wallJumpPenaltyLength = 2f;
    [Tooltip("Determines whether the player can perform climb jumps or not")]
    [SerializeField] private bool canClimbJump = true;
    [Tooltip("Determines how far off the wall the player will go when performing a climb jump")]
    [SerializeField] private float climbJumpPower = 12f;
    
    [Header("Air Control")]
    [Tooltip("Determines how fast the player will stop moving horizontally when in the air")]
    [SerializeField] private float airStopSpeed = 0.5f;
    [Range(0f,1f)] [Tooltip("The amount of control the player has over horizontal movement when in the air")]
    [SerializeField] private float aircontrol = 0.75f;
    [SerializeField] private float freefallSpeed = 20f;
    
    [Header("Walls")]
    [Tooltip("Determines whether the player can wall slide or not")] 
    [SerializeField] private bool canWallSlide = true;
    [Min(0.25f)] [Tooltip("The speed at which the player will slide down a wall if moving towards it")]
    [SerializeField] private float wallSlideSpeed = 4f;
    [Tooltip("Determines whether the player can climb up walls or not")] 
    [SerializeField] private bool canWallClimb = true;
    [Min(0.25f)] [Tooltip("The speed at which the player will climb up a wall when grabbing it")]
    [SerializeField] private float wallClimbSpeed = 8f;

    [Header("Dashing")] 
    [Tooltip("Determines whether the player can dash or not")] 
    [SerializeField] private bool canDash = true;
    [Tooltip("The speed at which the player will perform a dash in any given direction")]
    [SerializeField] private float dashVelocity = 25f;
    [Tooltip("The length of a dash in seconds")]
    [SerializeField] private float dashLength = .1f;
    [Range(0f,1f)] [Tooltip("Affects how the velocity changes at the end of a dash (1 = no effect)")]
    [SerializeField] private float dashVelocityPenalty = 1f;

    [Header("Stamina")] 
    [Tooltip("Imagine having to worry about stamina lol, couldn't be me")] 
    [SerializeField] private bool unlimitedStamina = false;
    [Tooltip("Determines how long the player can grab, jump, and climb walls until they need to touch the ground")]
    [SerializeField] private float maxStamina = 100f;
    [Tooltip("How much stamina per second is depleted while the player is grabbing a wall")]
    [SerializeField] private float grabStaminaCost = 25f;
    [Tooltip("How much stamina is depleted when performing a wall jump")]
    [SerializeField] private float wallJumpStaminaCost = 10f;
    [Tooltip("How much stamina is depleted when performing a climb jump")]
    [SerializeField] private float climbJumpStaminaCost = 25f;
    [Tooltip("How much stamina per second is depleted while the player is climbing a wall")]
    [SerializeField] private float climbStaminaCost = 30f;

    [Header("Inputs")] 
    [Range(0f,1f)] [Tooltip("Determines how tolerant axis inputs are")] 
    [SerializeField] private float deadzone = 0.2f;
    [Tooltip("Determines the length of coyote time after leaving a platform for a jump")] 
    [SerializeField] private float coyoteTime = 0.1f;
    [Tooltip("The name of the grab input axis")]
    [SerializeField] private String grabInputName = "Grab";
    [Tooltip("The name of the dash input button")]
    [SerializeField] private String dashInputName = "Dash";
    [Tooltip("The name of the other dash input button (m key)")] //Testing m key for dash, control key on mac does weird stuff lol
    [SerializeField] private String dashInputName2 = "Dash2"; // Dash2 input is m 
    [Tooltip("The name of the other dash input button (right shift key)")] //Testing m key for dash, control key on mac does weird stuff lol
    [SerializeField] private String dashInputName3 = "Dash3"; // Dash3 input is right shift

    [Header("Control Variables")] 
    [SerializeField] private float maxVelocity = 10f; // Stores the velocity we want to try and maintain our player under
    [SerializeField] private int maxJumps = 1; // The max number of jumps the player can store
    private int climbSide; // Stores x direction the wall we are touching is in
    private float airtime = 0f; // Stores the amount of airtime the player has left after a jump
    private float airControlMultiplier = 1f; // Stores the current penalty multiplier for horizontal air control
    private float stamina = 0f; // Stores the amount of stamina the player has left
    private bool facingRight = true; // Stores whether the player is facing right or left
    private bool grounded = false; // Stores whether the player is on the ground or not
    private bool climbing = false; // Stores whether the player is on a wall or not
    private bool grabbing = false; // Stores whether the player is grabbing a wall or not
     
    public int jumps = 0; // The number of jumps available to the player
    public int dashes = 0; // The number of dashes available to the player
    [HideInInspector] public bool dashing = false; // Stores whether the player is dashing or not

    private bool jumpInput = false;
    private bool runInput = false;
    private bool grabInput = false;
    private bool dashInput = false;

    [Header("References")]
    private Rigidbody2D rb; // The player's rigidbody
    private Timer timer; // The timer reference

    /* Specifications:
     * -> Sets component references at game start
     */
    private void Start()
    {
        // Set the rigidbody reference for the player
        rb = GetComponent<Rigidbody2D>();
        timer = FindObjectOfType<Timer>();
    }

    /* Specifications:
     * -> Handles player inputs
     * -> Handles frame sensitive physics updates
     */
    private void Update()
    {
        /* Input Detection */
        #region Inputs

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > deadzone ||
            Mathf.Abs(Input.GetAxisRaw("Vertical")) > deadzone ||
            Input.GetButtonDown("Jump") ||
            Input.GetButtonDown(dashInputName) || 
            Input.GetButtonDown(dashInputName2) ||
            Input.GetButton(grabInputName) || 
            Input.GetAxisRaw(grabInputName) > deadzone)
        {
            if (!timer.isCounting)
                timer.TimerStart();
        }
        
        // Detect run input
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > deadzone)
        {
            runInput = true;
            if (Input.GetAxisRaw("Horizontal") > deadzone)
                facingRight = true;
            else
                facingRight = false;
        }
        else
            runInput = false;

        // Detect jump input
        if (Input.GetButtonDown("Jump"))
        {
            if (jumps > 0)
                jumpInput = true;
            else
                StartCoroutine(EarlyJump());
        }
        
        // Detect grab input
        if (stamina > 0f)
        {
            if (Input.GetButton(grabInputName) || Input.GetAxisRaw(grabInputName) > deadzone)
                grabInput = true;
            else
                grabInput = false;
        }

        // Detect dash input
        if (dashes > 0 && canDash)
        {
            // Testing different dash button with 'Dash 2' and 'Dash 3', which are the 'm' and 'right shift' keys
            if (Input.GetButtonDown(dashInputName) || Input.GetButtonDown(dashInputName2) || Input.GetButtonDown(dashInputName3))
                dashInput = true;
        }

        #endregion

        /* ---Vertical Movement--- */
        #region Frame Sensitive Vertical Movement
        
        // Check if the player is climbing
        if (climbing && !dashing)
        {
            // Check if the player is grabbing the wall
            if (grabInput && stamina > 0f && airtime == 0f && canWallClimb)
            {
                if (Input.GetAxisRaw("Vertical") < -deadzone)
                {
                    // Maintain a constant wall slide speed
                    if (rb.velocity.y < -wallSlideSpeed)
                        rb.velocity = new Vector2(0f, -wallSlideSpeed);
                }
                else if (Input.GetAxisRaw("Vertical") > deadzone)
                {
                    // Maintain a constant wall climb speed
                    rb.velocity = new Vector2(0f, wallClimbSpeed);
                    if (!grounded)
                        stamina -= Time.deltaTime * climbStaminaCost;
                }
                else
                {
                    // Stop the player from falling and decrement stamina over time
                    rb.velocity = new Vector2(0f, 0f);
                    if (!grounded)
                        stamina -= Time.deltaTime * grabStaminaCost;
                }
            }
            // Check if the player is moving downwards
            else if (rb.velocity.y < -1f)
            {
                // Check if the player is moving in towards the wall
                if (climbSide * Input.GetAxisRaw("Horizontal") > 0f 
                    && Mathf.Abs(Input.GetAxisRaw("Horizontal")) > deadzone && canWallSlide)
                {
                    // Apply a force to slow down the player on the wall
                    if (Input.GetAxisRaw("Vertical") < -deadzone)
                    {
                        if (rb.velocity.y < -wallSlideSpeed * 3f)
                            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed * 3f);
                    }
                    else if (rb.velocity.y < -wallSlideSpeed)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
                    }
                }
            }
        }
        // Update whether the player is currently grabbing a wall or not
        if (climbing && grabInput && stamina > 0f && airtime == 0f && canWallClimb)
            grabbing = true;
        else
            grabbing = false;

        float freefallVelocity = 0f;
        // Control gravity and freefall velocity based on current player action
        if (dashing)
        {
            freefallVelocity = -dashVelocity;
            rb.gravityScale = 0f;
        }
        else if (Input.GetAxisRaw("Vertical") < -deadzone && !climbing)
        {
            freefallVelocity = -1.5f * freefallSpeed;
            rb.gravityScale = 10f;
        }
        else if (climbing && grabInput && stamina > 0f && rb.velocity.y <= 0f && Input.GetAxisRaw("Vertical") > -deadzone && canWallClimb)
        {
            freefallVelocity = -1f * freefallSpeed;
            rb.gravityScale = 0f;
        }
        else
        {
            freefallVelocity = -1f * freefallSpeed;
            rb.gravityScale = 7f;
        }
        // Clamp player freefall velocity to the provided value
        if (rb.velocity.y < freefallVelocity)
            rb.velocity = new Vector2(rb.velocity.x, freefallVelocity);
        
        #endregion
        
        /* Variable Updates and Checks */
        #region Variable Manipulation

        // Update air control multiplier based on the length given for the wall jump penalty
        if (airControlMultiplier < 1f)
            airControlMultiplier += Time.deltaTime / wallJumpPenaltyLength;
        else
            airControlMultiplier = 1f;

        // Zero out stamina if it goes negative
        if (unlimitedStamina)
            stamina = maxStamina;
        else if (stamina < 0f)
            stamina = 0f;
        
        // Slow down the player if going to fast
        if (rb.velocity.x > maxVelocity)
            rb.velocity = Vector2.Lerp(rb.velocity,new Vector2(maxVelocity, rb.velocity.y), 2f * Time.deltaTime);
        else if (rb.velocity.x < -maxVelocity)
            rb.velocity = Vector2.Lerp(rb.velocity,new Vector2(-maxVelocity, rb.velocity.y), 2f * Time.deltaTime);
        
        // Check if the player is dashing to slow them down faster
        if (dashing)
        {
            if (rb.velocity.x > maxVelocity)
                rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(maxVelocity, rb.velocity.y), 5f * Time.deltaTime);
            else if (rb.velocity.x < -maxVelocity)
                rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(-maxVelocity, rb.velocity.y),5f * Time.deltaTime);
            if (rb.velocity.y > maxVelocity)
                rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(rb.velocity.x, maxVelocity), 5f * Time.deltaTime);
            else if (rb.velocity.y < -maxVelocity)
                rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(rb.velocity.x, -maxVelocity), 5f * Time.deltaTime);
        }
            

        #endregion
    }

    /* Specifications:
     * -> Handles movement physics
     */
    private void FixedUpdate()
    {
        /* ---Horizontal Movement--- */
        #region Horizontal Movement
        
        // Check if the player is not dashing
        if (!dashing)
        {
            // Check if the player is currently running
            if (runInput && !grabbing)
            {
                // Check if player is within max speed or switching directions (Able to apply movement)
                if (Mathf.Abs(rb.velocity.x) <= runSpeed || rb.velocity.x * Input.GetAxisRaw("Horizontal") < 0f)
                {
                    // Check if the player is grounded
                    if (grounded)
                    {
                        // Check if player is switching direction
                        if (rb.velocity.x * Input.GetAxisRaw("Horizontal") < 0f)
                        {
                            // Cancel player speed to zero
                            rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(0f, rb.velocity.y), 1f);
                        }
                        // Set new movement velocity
                        rb.velocity = Vector2.Lerp(rb.velocity, 
                            new Vector2(runSpeed * Input.GetAxisRaw("Horizontal"), rb.velocity.y), .5f);
                    }
                    else // If in the air
                    {
                        // Check if player is switching direction
                        if (rb.velocity.x * Input.GetAxisRaw("Horizontal") < 0f)
                        {
                            // Cancel player speed to zero but slower than when on the ground
                            rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(0f, rb.velocity.y), .1f * aircontrol * airControlMultiplier);
                        }
                        // Set new movement velocity
                        rb.velocity = Vector2.Lerp(rb.velocity, 
                            new Vector2(runSpeed * Input.GetAxisRaw("Horizontal"), rb.velocity.y), .5f * aircontrol * airControlMultiplier);
                    }
                }
            }
            // Check if the player's velocity is not zero
            else if (Mathf.Abs(rb.velocity.x) != 0f)
            {
                // The vector that determines how quickly the player will slow down
                Vector2 slowdownVector;
                // Check if grounded
                if (grounded)
                {
                    // Set the new slowdownVector values using the given ground stop speed
                    slowdownVector = new Vector2(groundStopSpeed, 0f);
                    if (rb.velocity.x > 0f)
                    {
                        if (rb.velocity.x - slowdownVector.x < 0f)
                            rb.velocity = new Vector2(0f, rb.velocity.y);
                        else
                            rb.velocity -= slowdownVector;
                    }
                    else
                    {
                        if (rb.velocity.x + slowdownVector.x > 0f)
                            rb.velocity = new Vector2(0f, rb.velocity.y);
                        else
                            rb.velocity += slowdownVector;
                    }
                }
                else
                {
                    // Set the new slowdownVector values using the given air stop speed
                    slowdownVector = new Vector2(airStopSpeed, 0f);
                    if (rb.velocity.x > 0f)
                    {
                        if (rb.velocity.x - slowdownVector.x < 0f)
                            rb.velocity = new Vector2(0f, rb.velocity.y);
                        else
                            rb.velocity -= slowdownVector;
                    }
                    else
                    {
                        if (rb.velocity.x + slowdownVector.x > 0f)
                            rb.velocity = new Vector2(0f, rb.velocity.y);
                        else
                            rb.velocity += slowdownVector;
                    }
                }
            }
        }
        
        #endregion

        /* ---Vertical Movement--- */
        #region Vertical Movement
        
        // Apply jump impulse force on the vertical
        if (jumpInput && jumps > 0)
        {
            if (grounded)
            {
                airtime = 0.5f;
                grounded = false;
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                jumpInput = false;
                jumps--;
            }
            else if (climbing)
            {
                // Check if the player is grabbing a wall
                if (grabInput && stamina > 0f && canClimbJump)
                {
                    airtime = 0.5f;
                    grabInput = false;
                    rb.velocity = new Vector2(0f, climbJumpPower);
                    jumpInput = false;
                    jumps--;
                    if (stamina > 0f)
                        stamina -= climbJumpStaminaCost;
                }
                else if (canWallJump) // If not grabbing
                {
                    airtime = 0.5f;
                    rb.velocity = new Vector2(climbSide * -wallJumpPower, 1.25f * wallJumpPower);
                    jumpInput = false;
                    jumps--;
                    airControlMultiplier = wallJumpPenaltyMultiplier;
                    if (stamina > 0f)
                        stamina -= wallJumpStaminaCost;
                }
            }
            else if (!dashing)
            {
                airtime = 0.5f;
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                jumpInput = false;
                jumps--;
            }
        }

        // Check if the jump button is being held
        if (Input.GetButton("Jump") && !dashing)
        {
            // Check if player still has airtime and is moving up
            if (airtime > 0f && rb.velocity.y > 0f)
            {
                // Decrement airtime
                airtime -= Time.deltaTime;
                // Apply a force to boost the player up
                rb.AddForce(Vector2.up * jumpPower * 12f * airtime, ForceMode2D.Force);
            }
            else // Player has run out of airtime
            {
                airtime = 0f;
            }
        }
        // Check if player is not holding the jump button and is not dashing
        else if (!dashing)
        {
            // Reset airtime to zero
            airtime = 0f;
        }

        #endregion
        
        /* ---Dash--- */
        #region Dash Movement

        // Check for dash input
        if (dashInput && !dashing)
        {
            dashInput = false;
            dashes--;
            // Direction variables
            float vertical = 0;
            float horizontal = 0;
            // Set horizontal direction value
            if (Input.GetAxisRaw("Horizontal") > deadzone)
                horizontal = 1;
            else if (Input.GetAxisRaw("Horizontal") < -deadzone)
                horizontal = -1;
            // Set vertical direction value
            if (Input.GetAxisRaw("Vertical") > deadzone)
                vertical = 1;
            else if (Input.GetAxisRaw("Vertical") < -deadzone)
                vertical = -1;
            // Run the dash function
            StartCoroutine(Dash(horizontal, vertical));
        }

        #endregion
    }

    
    /* Specifications:
     * -> Handles dash implementation
     */
    private IEnumerator Dash(float horizontal, float vertical)
    {
        // Set dashing status to true
        dashing = true;
        // The Vector we will use to apply the dash velocity
        Vector2 dashVector = Vector2.zero;
        if (horizontal == 0 && vertical == 0)
        {
            if (facingRight)
                dashVector = new Vector2(dashVelocity,0f);
            else
                dashVector = new Vector2(-dashVelocity,0f);
        }
        else if (Mathf.Abs(horizontal) > 0 && Mathf.Abs(vertical) > 0)
        {
            horizontal *= Mathf.Cos(Mathf.Deg2Rad * 45);
            vertical *= Mathf.Cos(Mathf.Deg2Rad * 45);
            dashVector = new Vector2(horizontal * dashVelocity,vertical * dashVelocity);
        }
        else
        {
            dashVector = new Vector2(horizontal * dashVelocity,vertical * dashVelocity);
        }
        // Set the new velocity to perform a dash
        rb.velocity = dashVector;
        // Wait dash length before resetting variables
        yield return new WaitForSeconds(dashLength);
        rb.velocity = new Vector2(rb.velocity.x * dashVelocityPenalty, rb.velocity.y * dashVelocityPenalty);
        dashing = false;
    }
    
    /* Specifications:
     * -> Stores jump input for a tenth of a second
     */
    private IEnumerator EarlyJump()
    {
        jumpInput = true;
        yield return new WaitForSeconds(coyoteTime);
        jumpInput = false;
    }
    
    /* Specifications:
     * -> If ground is true, then increments jumps by one and sets grounded to true
     * -> If grounded is false, then waits five hundredths of a second before setting grounded to false
     * (Accommodates for delayed jump inputs - "Coyote time")
     */
    public IEnumerator Ground(bool ground)
    {
        if (ground)
        {
            grounded = true;
            if (jumps < maxJumps)
                jumps = maxJumps;
            stamina = maxStamina;
        }
        else
        {
            yield return new WaitForSeconds(coyoteTime);
            if (jumps == maxJumps)
                jumps--;
            grounded = false;
        }
    }

    /* Specifications:
     * -> If climb is true, then increments jumps by one and sets climbing to true
     * -> If climb is false, then waits five hundredths of a second before setting climbing to false
     * (Accommodates for delayed jump inputs - "Coyote time")
     */
    public IEnumerator Climb(bool climb, bool right)
    {
        if (climb)
        {
            if (climbSide != 0)
            {
                if (climbSide < 0 && right && Input.GetAxisRaw("Horizontal") > deadzone)
                    climbSide = 1;
                else if (climbSide > 0 && !right && Input.GetAxisRaw("Horizontal") < -deadzone)
                    climbSide = -1;
                climbing = true;
                if (jumps < maxJumps && (canWallClimb || canWallJump))
                    jumps = maxJumps;
            }
            else
            {
                if (right)
                    climbSide = 1;
                else
                    climbSide = -1;
                climbing = true;
                if (jumps < maxJumps && (canWallClimb || canWallJump))
                    jumps = maxJumps;
            }
        }
        else
        {
            yield return new WaitForSeconds(coyoteTime / 2f);
            jumps = 0;
            climbing = false;
            // Update the side we are climbing to none (zero)
            climbSide = 0;
        }
    }
}
