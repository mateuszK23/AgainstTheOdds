using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private float movementSpeed = 7f;
    private float jumpForce = 14f;
    private int jumpCounter = 0;
    private float movementX;
    private float teleportDistance = 6;
    private float previousDirection;
    public bool djEnabled = false;
    public bool DJ_gemObtained = false;
    public bool FB_gemObtained = false;
    public bool TP_gemObtained = false; 
    private int maxHealth = 100;
    private int currentHealth;
    public float leftBoundary = -30;
    public float rightBoundary = 30;
    public GameObject firePoint;
    public int remainingFireBalls = 3;
    public int maxAmmo = 6;
    public int maxTeleports = 3;
    public int remainingTeleports = 3;
    public bool grounded = true;
    private Animator anim;
    private SpriteRenderer sr;
    private bool bouncedOffEnemy = false;
    private int upRange;

    [SerializeField] private ProgressBar healthBar;
    [SerializeField] private GameManagerScript gameManager;
    [SerializeField] private TP_IndicatorScript TP_Indicator;
    public GameObject tp;
    // Start is called before the first frame update
    void Start()
    {
        upRange = (int)(30 * StateController.difficulty);
        currentHealth = maxHealth;
        healthBar.initValues(currentHealth, maxHealth);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Teleport();
        Jump();
        if (currentHealth <= 0) gameOver();
    }

    private void Movement()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        if (movementX > 0)
        {
            anim.SetBool("isWalking", true);
            sr.flipX = true;
            firePoint.transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f, transform.position.z);
            firePoint.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (movementX < 0)
        {
            sr.flipX = false;
            anim.SetBool("isWalking", true);
            firePoint.transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y + 0.5f, transform.position.z);
            firePoint.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else anim.SetBool("isWalking", false);

        transform.position += new Vector3(movementX, 0f, 0f) * movementSpeed * Time.deltaTime;
        previousDirection = movementX != 0 ? movementX : previousDirection;
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            int maxJumps = djEnabled ? 2 : 1;
            if (jumpCounter < maxJumps && !bouncedOffEnemy)
            {
                grounded = false;
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                jumpCounter++;
            }
        }
    }

    void Teleport()
    {
        float dir = (movementX != 0) ? movementX : previousDirection;

        if (Input.GetKeyDown("f"))
        {
            if(remainingTeleports > 0)
            {
                GameObject ef = Instantiate(tp, transform.position, Quaternion.identity);
                ParticleSystem px = ef.GetComponent<ParticleSystem>();
                px.Play();
                Destroy(ef, px.main.duration);
                Vector3 newPos = new Vector3(transform.position.x + (dir * teleportDistance), transform.position.y, transform.position.z);
                if (newPos.x >= rightBoundary) newPos.x = rightBoundary - 0.4f;
                if (newPos.x <= leftBoundary) newPos.x = leftBoundary + 0.4f;
                transform.position = newPos;
                remainingTeleports--;
                TP_Indicator.fillAmmo(remainingTeleports);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.CompareTag("Ground"))
        {
            jumpCounter = 0;
            grounded = true;
        }
        if (other.collider.CompareTag("EnemyHead") && !bouncedOffEnemy)
        {
            bouncedOffEnemy = true;
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jumpCounter = 0;
            other.gameObject.GetComponent<EnemyScript>().Die(true);
            Invoke("resumeJumpAbility", 0.2f);
            StateController.score += 5;
        }

        if (other.gameObject.CompareTag("Enemy") && !other.collider.CompareTag("EnemyHead"))
        {
            takeHit(20 + upRange);
        }
        if (other.collider.CompareTag("WorldBoundary"))
        {
            takeHit(20 + upRange);
        }
    }
    private void resumeJumpAbility()
    {
        bouncedOffEnemy = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DJ_Gem"))
        { 
            djEnabled = true;
            DJ_gemObtained = true;
            Destroy(other.gameObject);
            StateController.score += 2;
        }
        if (other.gameObject.CompareTag("FB_Gem"))
        {
            if (remainingFireBalls < maxAmmo)
            {
                if (remainingFireBalls + 3 < maxAmmo) remainingFireBalls += 3;
                else remainingFireBalls += maxAmmo - remainingFireBalls;
                FB_gemObtained = true;
            }
            Destroy(other.gameObject);
            StateController.score += 2;
        }
        if (other.gameObject.CompareTag("TP_Gem"))
        {
            if (remainingTeleports < maxAmmo)
            {
                if (remainingTeleports+1 < maxTeleports) remainingTeleports++;
                TP_gemObtained = true;
            }
            Destroy(other.gameObject);
            StateController.score += 2;
        }
    }
    public void takeHit(int value)
    {
        currentHealth -= value;
        healthBar.setValue(currentHealth);
    }

    private void gameOver()
    {
        GameObject ef = Instantiate(tp, transform.position, Quaternion.identity);
        ParticleSystem px = ef.GetComponent<ParticleSystem>();
        px.Play();
        Destroy(ef, px.main.duration);
        gameObject.SetActive(false);
        Invoke("loadGameOverScene", px.main.duration);
    }

    private void loadGameOverScene()
    {
        gameManager.endGame();
    }
}
