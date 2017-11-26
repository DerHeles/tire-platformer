using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * customized version of https://www.assetstore.unity3d.com/en/#!/content/11228 
 * 
 */


public class PlayerController : MonoBehaviour
{
	[HideInInspector]
    public bool facingRight = true;         // For determining which way the player is currently facing.
    [HideInInspector]
    public bool jump = false;               // Condition for whether the player should jump.


    public float moveForce = 10f;          // Amount of force added to move the player left and right.
    public float maxSpeed = 5f;             // The fastest the player can travel in the x axis.
    public float jumpForce = 1000f;         // Amount of force added when the player jumps.
    public float airMoveForce = 5f;
    
    private Transform groundCheck;          // A position marking where to check if the player is grounded.
    private Transform groundCheck2;
    [SerializeField]
    private bool grounded = false;          // Whether or not the player is grounded.
    //private Animator anim;                  // Reference to the player's m_animator component.

    private Rigidbody2D m_body;

    [SerializeField] private bool m_forbidAirMovement = false;

    [SerializeField] private float m_movementDrag = 2.0f;

    [SerializeField] private int m_hitpoints = 3;


    private float m_currentDamageTime;
    private float m_damageTime = 0.25f;
    private SpriteRenderer m_spriteRenderer;

    private Animator m_animator;
    private bool m_hasKey;

    [Header("UI")]
    [SerializeField] private Image[] m_patchImages;

    [SerializeField] private Image keyImage;
    [SerializeField] private Sprite keySprite;
    [SerializeField] private Sprite keySpriteInvisible;

    private bool m_finished = false;

    public AnimationClip deadAnimation;

    private bool controllable;
    public Image[] cinematicBars;
    public GameObject thoughtBubble;
    public GameObject thoughtBubbleGarage;
    public GameObject thoughtBubbleKey;

    private AudioManager m_audioManager;

    void Awake()
    {
        // Setting up references.
        groundCheck = transform.Find("groundCheck");
        groundCheck2 = transform.Find("groundCheck2");
        //anim = GetComponent<Animator>();
        m_body = GetComponent<Rigidbody2D>();
        //Debug.Log("Drag=" + m_body.drag);
        m_spriteRenderer = GetComponent<SpriteRenderer>();

        m_animator = GetComponent<Animator>();

        m_audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void Start()
    {
        //StartCoroutine("StartAnimation");
        controllable = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 3.0f, ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            DieAndCry();    
        }

        if (m_currentDamageTime > 0.0f)
        {
            m_currentDamageTime -= Time.deltaTime;
            if (m_currentDamageTime <= 0.0f)
            {
                m_currentDamageTime = 0.0f;
                m_spriteRenderer.color = Color.white;
            }
        }

        if (m_finished)
            return;

        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"))
            || Physics2D.Linecast(transform.position, groundCheck2.position, 1 << LayerMask.NameToLayer("Ground"));

        // If the jump button is pressed and the player is grounded then the player should jump.
        if (Input.GetButtonDown("Jump") && grounded)
            jump = true;

        
    }


    void FixedUpdate()
    {
        if (m_finished)
            return;

        // Cache the horizontal input.
        float h = Input.GetAxisRaw("Horizontal");

        if (!controllable)
            h = 0f;

        // If no horizontal input --> idle
        //m_animator.SetBool("Idle", h == 0);
        m_animator.speed = (h == 0) ? 0.0f : 1.0f;
        

        if (grounded && h == 0.0f)
        {
            //GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //Debug.Log("BREMSEN");
            m_body.drag = m_movementDrag;

            //m_animator.SetBool("Idle", true);
        }
        else
        {
            m_body.drag = 0.0f;
        }

        // The Speed m_animator parameter is set to the absolute value of the horizontal input.
        //anim.SetFloat("Speed", Mathf.Abs(h));

        // If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
        if (h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
        {
            if (m_forbidAirMovement)
            {
                if (grounded)
                {
                    // ... add a force to the player.
                    GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);
                }
            }
            else
            {
                if (grounded)
                {
                    // ... add a force to the player.
                    GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);
                }
                else
                {
                    GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * airMoveForce);
                }
            }
        }
            

        // If the player's horizontal velocity is greater than the maxSpeed...
        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
            // ... set the player's velocity to the maxSpeed in the x axis.
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        // If the input is moving the player right and the player is facing left...
        if (h > 0 && !facingRight)
            // ... flip the player.
            Flip();
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (h < 0 && facingRight)
            // ... flip the player.
            Flip();

        // If the player should jump...
        if (jump && controllable)
        {
            // Set the Jump m_animator trigger parameter.
            //anim.SetTrigger("Jump");
            
            //AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);
            m_audioManager.PlaySound(AudioManager.SoundID.Jump);

            // Add a vertical force to the player.
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));

            // Make sure the player can't jump again until the jump conditions from Update are satisfied.
            jump = false;
        }
    }


    void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        //Vector3 theScale = transform.localScale;
        //theScale.x *= -1;
        //transform.localScale = theScale;
        GetComponent<SpriteRenderer>().flipX = !facingRight; // Potential fix for platform glitch
    }

    public void ReceiveDamage()
    {
        if (m_hitpoints > 0)
        {
            m_currentDamageTime = m_damageTime;
            m_spriteRenderer.color = Color.red;

            m_hitpoints--;
            ShowCurrentLifeStatus();
            if (m_hitpoints == 0)
            {
                Debug.Log("DEAD");
                DieAndCry();
                
            }
        }
        Debug.Log("DAMAGE");
    }

    private void ShowCurrentLifeStatus()
    {
        foreach (var patch in m_patchImages)
        {
            patch.enabled = false;
        }
        for (int i = 0; i < m_patchImages.Length; ++i)
        {
            if (m_hitpoints >= (i+1))
                m_patchImages[i].enabled = true;
        }
    }

    public bool HasKey()
    {
        return m_hasKey;
    }

    public void UseKey()
    {
        m_hasKey = false;
        keyImage.sprite = keySpriteInvisible;
    }

    public void PickupKey()
    {
        m_hasKey = true;
        keyImage.sprite = keySprite;
        m_audioManager.PlaySound(AudioManager.SoundID.PickupKey);
    }

    public void PickupPatch()
    {
        //m_hitpoints++;
        //m_hitpoints = Math.Min(m_hitpoints, 3);
        ShowCurrentLifeStatus();
    }

    public void DieAndCry()
    {
        m_finished = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //m_animator.speed = 0f;

        // other animation
        // TO DO
        //m_animator.Play();
        m_animator.SetTrigger("Dead");
        m_animator.speed = 0.5f;

        m_audioManager.PlaySound(AudioManager.SoundID.Dead);
        m_audioManager.PlayMusic(AudioManager.MusicID.End);

        Debug.Log("TOT");
    }

    public IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(1.5f);

        // Bubble
        m_audioManager.PlaySound(AudioManager.SoundID.Bubble);
        thoughtBubble.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        thoughtBubble.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Flip();
        yield return new WaitForSeconds(0.5f);

        // Garage Bubble
        m_audioManager.PlaySound(AudioManager.SoundID.Bubble);
        thoughtBubbleGarage.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 0.4f * jumpForce));
        yield return new WaitForSeconds(1.0f);
        thoughtBubbleGarage.SetActive(false);
        yield return new WaitForSeconds(0.5f);

        controllable = true;
        foreach (var c in cinematicBars)
        {
            c.enabled = false;
        }
    }

    public void ShowKeyBubble()
    {
        StartCoroutine("KeyBubbleAnimation");
    }

    public IEnumerator KeyBubbleAnimation()
    {
        controllable = false;
        foreach (var c in cinematicBars)
        {
            c.enabled = true;
        }
        yield return new WaitForSeconds(0.5f);
        m_audioManager.PlaySound(AudioManager.SoundID.Bubble);
        thoughtBubbleKey.SetActive(true);
        
        yield return new WaitForSeconds(2.0f);
        thoughtBubbleKey.SetActive(false);

        controllable = true;
        foreach (var c in cinematicBars)
        {
            c.enabled = false;
        }
    }
}
