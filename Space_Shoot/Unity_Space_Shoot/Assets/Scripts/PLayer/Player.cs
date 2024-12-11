using UnityEngine;

public class PLayer : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private Animator anim;
    public float movementSpeed  = 5f;
    Vector2 movement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.rb = this.gameObject.GetComponent<Rigidbody2D>();
        this.anim = this.gameObject.GetComponent<Animator>();
        collider = this.gameObject.GetComponent<BoxCollider2D>();
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
}
