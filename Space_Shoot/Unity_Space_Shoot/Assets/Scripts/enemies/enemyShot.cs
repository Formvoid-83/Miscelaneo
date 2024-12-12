using UnityEngine;
using UnityEngine.Pool;

public class enemyShot : MonoBehaviour
{
    private float speed;
    private Vector2 direction;
    private ObjectPool<enemyShot> myPool;
    private BoxCollider2D collider;

    public ObjectPool<enemyShot> MyPool { get => myPool; set => myPool = value; }
    private float timer;
    public float damage= 15f;

    void Start(){
        collider = this.gameObject.GetComponent<BoxCollider2D>();
        direction=new Vector2(0f,-1f);
        speed=5f;
        Destroy(this.gameObject,8);
    }
    void Update(){
        transform.Translate(direction * speed * Time.deltaTime);
        timer += Time.deltaTime;

        if(timer== 4){
            timer=0;
            myPool.Release(this);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LimitCollition"))
        {
            myPool.Release(this);
        }
    }
}
