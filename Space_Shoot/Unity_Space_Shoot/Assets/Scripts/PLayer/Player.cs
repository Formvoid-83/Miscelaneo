using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PLayer : MonoBehaviour
{

    private GameController gameController;
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isBlinking = false;

    public float movementSpeed  = 5f;
    public int crashDamage=20;
    public int maxHealth = 100;
    private float currentHealth;
    private Image healthBarFill;
    Vector2 movement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameController =GameObject.FindWithTag("GameController").GetComponent<GameController>();

        this.rb = this.gameObject.GetComponent<Rigidbody2D>();
        this.anim = this.gameObject.GetComponent<Animator>();
        collider = this.gameObject.GetComponent<BoxCollider2D>();

        // Initialize health
        currentHealth = maxHealth;

        // Find the health bar UI element by tag
        GameObject healthBar = GameObject.FindWithTag("healthBarFill");
        if (healthBar != null)
        {
            healthBarFill = healthBar.GetComponent<Image>();
        }
        //For the sprites
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("MovementSpeed", movement.sqrMagnitude);
    }
     private void FixedUpdate() {
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);
    }
     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemyShot"))
        {
            enemyShot shot = collision.GetComponent<enemyShot>();
            if (shot != null)
            {
                TakeDamage(shot.damage); // Reduce health by the damage amount
                //Destroy(collision.gameObject); // Destroy the enemy shot
            }
        }
        if (collision.CompareTag("enemy"))
        {
            Enemy1 enemy = collision.GetComponent<Enemy1>();
            if (enemy != null)
            {
                TakeDamage(enemy.crashDamage); // Reduce health by the damage amount
                //Destroy(collision.gameObject); // Destroy the enemy shot
            }
        }
    }
    void TakeDamage(int damage)
    {
        
        
        if(!isBlinking){
            currentHealth -= damage;
            StartCoroutine(BlinkEffect());
        }
        // Clamp health to ensure it doesn't go below 0
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Update the health bar UI
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }

        // Check if the player is dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    IEnumerator BlinkEffect()
    {
        isBlinking = true;
        Color currentColor = spriteRenderer.color;
        currentColor.a = 0.2f; 
        spriteRenderer.color = currentColor;
        Debug.Log("Blink for my sake");
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
        isBlinking = false;
    }
    void Die()
    {
        Debug.Log("Player is dead!");
  
            if (gameController != null)
            {
            gameController.GameOver();
            }else{
                    Debug.Log("Sorry, no player controller found");       
            }
    }
}
