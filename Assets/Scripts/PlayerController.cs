using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int hitpoints = 4;

    [Header("Movement")]
    [SerializeField] private float moveForce = 10f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float jumpForce = 1000f;
    [SerializeField] private float airMoveForce = 5f;

    public float JumpForce
    {
        get { return jumpForce; }
        set { jumpForce = value; }
    }

    public float AirMoveForce
    {
        get { return airMoveForce; }
        set { airMoveForce = value; }
    }

    private bool facingRight = true;
    private bool jump;

    private Transform groundCheck;
    private Transform groundCheck2;
    private bool grounded;
    
    [SerializeField] private bool forbidAirMovement;
    [SerializeField] private float movementDrag = 2.0f;
    
    private float currentDamageTime;
    private float damageTime = 0.25f;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D body;
    private Animator animator;
    private bool hasKey;

    [Header("UI")]
    [SerializeField] private Image[] m_patchImages;

    [SerializeField] private Image keyImage;
    [SerializeField] private Sprite keySprite;
    [SerializeField] private Sprite keySpriteInvisible;

    [SerializeField] private Image[] cinematicBars;
    [SerializeField] private GameObject thoughtBubble;
    [SerializeField] private GameObject thoughtBubbleGarage;
    [SerializeField] private GameObject thoughtBubbleKey;

    private AudioManager audioManager;

    private Menu menu;
    private bool isInMenu;

    private bool finished;
    private bool controllable;

    private void Awake()
    {
        groundCheck = transform.Find("groundCheck");
        groundCheck2 = transform.Find("groundCheck2");

        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        menu = GameObject.Find("Menu").GetComponent<Menu>();
        menu.Player = this;
    }

    private void Start()
    {
        StartCoroutine("StartAnimation");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isInMenu && !finished && controllable)
        {
            isInMenu = true;
            menu.OpenPauseMenu();
        }

        #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.K) && !isInMenu)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 3.0f, ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.L) && !isInMenu)
        {
            DieAndCry();
        }
        #endif

        if (currentDamageTime > 0.0f)
        {
            currentDamageTime -= Time.deltaTime;
            if (currentDamageTime <= 0.0f)
            {
                currentDamageTime = 0.0f;
                spriteRenderer.color = Color.white;
            }
        }

        if (finished || isInMenu)
            return;

        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"))
            || Physics2D.Linecast(transform.position, groundCheck2.position, 1 << LayerMask.NameToLayer("Ground"));

        // If the jump button is pressed and the player is grounded then the player should jump.
        if (Input.GetButtonDown("Jump") && grounded)
            jump = true;
    }


    private void FixedUpdate()
    {
        float tolerance = 0.00001f;

        if (finished || isInMenu)
            return;
        
        float h = Input.GetAxisRaw("Horizontal");

        if (!controllable)
            h = 0f;

        // If no horizontal input --> idle
        animator.speed = (Math.Abs(h) < tolerance) ? 0.0f : 1.0f;

        if (grounded && (Math.Abs(h) < tolerance))
            body.drag = movementDrag;
        else
            body.drag = 0.0f;

        // The Speed animator parameter is set to the absolute value of the horizontal input.
        //anim.SetFloat("Speed", Mathf.Abs(h));

        // If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
        if (h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
        {
            if (forbidAirMovement)
            {
                if (grounded)
                    GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);
            }
            else
            {
                if (grounded)
                    GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);
                else
                    GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * AirMoveForce);
            }
        }

        // If the player's horizontal velocity is greater than the maxSpeed...
        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
            // ... set the player's velocity to the maxSpeed in the x axis.
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        // If the input is moving the player right and the player is facing left...
        if (h >= tolerance && !facingRight)
            // ... flip the player.
            Flip();
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (h < tolerance && facingRight)
            // ... flip the player.
            Flip();
        
        if (jump && controllable)
        {
            audioManager.PlaySound(AudioManager.SoundID.Jump);

            // Add a vertical force to jump
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, JumpForce));
            
            jump = false;
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;
        GetComponent<SpriteRenderer>().flipX = !facingRight; // Potential fix for platform glitch
    }

    public void ReceiveDamage()
    {
        if (hitpoints > 0)
        {
            currentDamageTime = damageTime;
            spriteRenderer.color = Color.red;
            hitpoints--;

            ShowCurrentLifeStatus();

            if (hitpoints == 0)
                DieAndCry();
        }
    }

    private void ShowCurrentLifeStatus()
    {
        foreach (var patch in m_patchImages)
        {
            patch.enabled = false;
        }
        for (int i = 0; i < m_patchImages.Length; ++i)
        {
            if (hitpoints >= (i+1))
                m_patchImages[i].enabled = true;
        }
    }

    public bool HasKey()
    {
        return hasKey;
    }

    public void UseKey()
    {
        hasKey = false;
        keyImage.sprite = keySpriteInvisible;
    }

    public void PickupKey()
    {
        hasKey = true;
        keyImage.sprite = keySprite;
        audioManager.PlaySound(AudioManager.SoundID.PickupKey);
    }

    public void PickupPatch()
    {
        // Only update life status, world switch is handled by patch/world system
        ShowCurrentLifeStatus();
    }

    public void DieAndCry()
    {
        finished = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        animator.SetTrigger("Dead");
        animator.speed = 0.5f;

        audioManager.PlaySound(AudioManager.SoundID.Dead);
        audioManager.PlayMusic(AudioManager.MusicID.End);
        
        StartCoroutine("DelayedGameOver");
    }

    public void FinishGame()
    {
        finished = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        animator.SetTrigger("Dead");
        animator.speed = 0.5f;

        audioManager.PlaySound(AudioManager.SoundID.Dead);
        audioManager.PlayMusic(AudioManager.MusicID.End);

        StartCoroutine("DelayedGameEnd");
    }

    public IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(1.5f);

        // Question Bubble
        audioManager.PlaySound(AudioManager.SoundID.Bubble);
        thoughtBubble.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        thoughtBubble.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Flip();
        yield return new WaitForSeconds(0.5f);

        // Garage Bubble
        audioManager.PlaySound(AudioManager.SoundID.Bubble);
        thoughtBubbleGarage.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 0.4f * JumpForce));
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
        audioManager.PlaySound(AudioManager.SoundID.Bubble);
        thoughtBubbleKey.SetActive(true);
        
        yield return new WaitForSeconds(2.0f);
        thoughtBubbleKey.SetActive(false);

        controllable = true;
        foreach (var c in cinematicBars)
        {
            c.enabled = false;
        }
    }

    public IEnumerator DelayedGameEnd()
    {
        yield return new WaitForSeconds(2.5f);
        menu.OpenGameEndPanel();
    }

    public IEnumerator DelayedGameOver()
    {
        yield return new WaitForSeconds(2.5f);
        menu.OpenGameOverPanel();
    }

    public bool GameFinished()
    {
        return finished;
    }

    public void PauseMenuClosed()
    {
        isInMenu = false;
    }
}
