using UnityEngine;
using UnityEngine.Pool;
public class enemyShot2 : MonoBehaviour
{
    private float speed;
    private Vector2 direction;
    private ObjectPool<enemyShot2> myPool;
    private BoxCollider2D collider;
    public ObjectPool<enemyShot2> MyPool { get => myPool; set => myPool = value; }
    private float timer;
    public int damage= 25;
    void Start()
    {
        collider = this.gameObject.GetComponent<BoxCollider2D>();
        direction=new Vector2(0f,-1f);
        speed=5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        timer += Time.deltaTime;

        if(timer== 4){
            timer=0;
            myPool.Release(this);
        }
        //Delete itself if out of bounds
        if(this.gameObject.transform.position.y <= -8f ){
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
