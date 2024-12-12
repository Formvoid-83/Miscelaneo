using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy1 : MonoBehaviour
{
    private GameController gameController;
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private Animator anim;
    private ObjectPool<Enemy1> myPool;
    public ObjectPool<Enemy1> MyPool { get => myPool; set => myPool = value; }
    public float movementSpeed  = 1f;
    private int scoreValue = 10;
    private bool isMoving = true;
    Vector2 movement;
    protected enemy_Shoot_System shootSystem;
    private float timer;
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
        Destroy(this.gameObject,8);
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
        //Timer for Pooling
        if(timer== 4){
            timer=0;
            myPool.Release(this);
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
            gameController.updateScore(scoreValue);
            Destroy(this.gameObject); // Destroy Enemy1
            Destroy(collision.gameObject); // Destroy the player's shot
        }
    }
}
