using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy1 : MonoBehaviour
{
    public EnemyType enemyType = EnemyType.Enemy1;
    private GameController gameController;
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private Animator anim;
    private ObjectPool<Enemy1> myPool;
    public ObjectPool<Enemy1> MyPool { get => myPool; set => myPool = value; }
    [SerializeField] private int life=20;
    private int fullLife;
    [SerializeField] public int crashDamage=20;
    public float movementSpeed  = 1f;
    private int scoreValue = 10;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isBlinking = false;
    private bool isMoving = true;
    Vector2 movement;
    protected enemy_Shoot_System shootSystem;
    private float timer;
    void Awake(){
        //For the sprites
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if(gameController==null){
            Debug.Log("El enemigo no pudo conseguir el Game Controller");
        }
        this.rb = this.gameObject.GetComponent<Rigidbody2D>();
        this.anim = this.gameObject.GetComponent<Animator>();
        collider = this.gameObject.GetComponent<BoxCollider2D>();
        movement.y= -1f;
        shootSystem = this.gameObject.GetComponent<enemy_Shoot_System>();
        if (shootSystem == null)
        {
            Debug.LogWarning("enemy_Shoot_System script not found on this GameObject.");
        }
        
        
        StartCoroutine(MoveAndPause());
        //Time Limit
        //Destroy(this.gameObject,8);
    }

    // Update is called once per frame
    private void FixedUpdate() {
         if (isMoving)
        {   anim.SetFloat("Vertical",movement.y);
            rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);

        }else{
            anim.SetFloat("Vertical",0.02f);
            rb.MovePosition(rb.position);
        }
        if(rb.position.y <= -8f ){
            tryRelease();
        }

    }
    IEnumerator MoveAndPause()
    {
        while (true) // Infinite loop to repeat the behavior
        {
            isMoving = true; // Start moving
            yield return new WaitForSeconds(1f);

            isMoving = false; // Stop moving
            yield return new WaitForSeconds(2f);
            if (shootSystem != null)
            {
                shootSystem.ShootNow(); // Replace 'Shoot' with the actual method name
            }
            else{
                Debug.Log("No se puede siparara");
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object is tagged as "playerShot"
        if (collision.CompareTag("playerShot"))
        {
             Shoot projectile = collision.GetComponent<Shoot>();
            if (projectile != null)
            {
                
                if (!isBlinking)
                {
                    life -= projectile.damage;
                    StartCoroutine(BlinkEffect());
                }
                if (life <= 0)
                {
                    gameController.updateScore(scoreValue);
                    tryRelease();
                }
            }
            //Destroy(collision.gameObject); // Destroy the player's shot
            projectile.tryRelease();
        }
        if(collision.CompareTag("Player")){
             PLayer player = collision.GetComponent<PLayer>();
            if (player != null)
            {

                life -= player.crashDamage;

                if (life <= 0)
                {
                    gameController.updateScore(scoreValue);
                    tryRelease();
                }
            }
        }
    }
    public void recharge(){
    life = fullLife;  // Reset life
    movement.y = -1f; // Reset movement direction
    //transform.position = new Vector2(Random.Range(-9, 9), 10f); // Reset position to a spawning point
    spriteRenderer.color = originalColor; // Reset color
    isBlinking = false; // Reset blinking state
    //isMoving = true; // Enable movement
    StopAllCoroutines(); // Stop any ongoing coroutines
    StartCoroutine(MoveAndPause()); 
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
    private void tryRelease(){
        if (myPool != null)
        {
            myPool.Release(this);
        }
        else
        {
            Debug.Log("myPool is null! Cannot release the object. Destroying it now");
            Destroy(this.gameObject);
        }
    }
}
