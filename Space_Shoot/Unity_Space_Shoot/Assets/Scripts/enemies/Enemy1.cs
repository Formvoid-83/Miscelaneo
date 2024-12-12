using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private Animator anim;
    public float movementSpeed  = 1f;
    private bool isMoving = true;
    Vector2 movement;
    protected enemy_Shoot_System shootSystem;
    void Start()
    {
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
            Destroy(this.gameObject); // Destroy Enemy1
            Destroy(collision.gameObject); // Destroy the player's shot
        }
    }
}
