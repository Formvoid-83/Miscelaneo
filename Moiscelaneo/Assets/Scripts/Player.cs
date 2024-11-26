using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveForce= 10f;

    [SerializeField]
    private float jumpForce= 5f;
    public float MovementX;
    [SerializeField]
    private Rigidbody2D myBody;

    private SpriteRenderer sr;
    private Animator anim;
    private string WALKING_ANIMATION= "Walk";
    private bool isGrounded;
    private string GROUND_TAG= "Ground";

    private void Awake(){
        myBody= GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer();
    }
    private void FixedUpdate(){
        PlayerJump();
    }
    void PlayerMoveKeyboard(){
        MovementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(MovementX, 0f, 0f) * moveForce * Time.deltaTime;
    }
    void AnimatePlayer(){
        //Vamos al lado derecho
        if(MovementX > 0){
            anim.SetBool(WALKING_ANIMATION, true);
            sr.flipX= false;
        }
        else if(MovementX < 0){
            anim.SetBool(WALKING_ANIMATION, true);
            sr.flipX= true;
        }
        else{
            anim.SetBool(WALKING_ANIMATION, false);
        }
    }
    void PlayerJump(){
        if(Input.GetButtonDown("Jump") && isGrounded){
            isGrounded= false;
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag(GROUND_TAG)){
            isGrounded=true;
        }
    }
}
