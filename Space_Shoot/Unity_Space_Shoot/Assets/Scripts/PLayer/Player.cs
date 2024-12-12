using UnityEngine;
using UnityEngine.UI;

public class PLayer : MonoBehaviour
{

    private GameController gameController;
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private Animator anim;

    public float movementSpeed  = 5f;
    public float maxHealth = 100f;
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
    }
    void TakeDamage(float damage)
    {
        currentHealth -= damage;

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
