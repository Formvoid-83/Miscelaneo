using UnityEngine;
using UnityEngine.Pool;

public class enemyShot : MonoBehaviour
{
    [SerializeField] private float speed=-2f;
    private ObjectPool<enemyShot> myPool;
    private BoxCollider2D collider;

    public ObjectPool<enemyShot> MyPool { get => myPool; set => myPool = value; }
    private float timer;

    void Start(){
        collider = this.gameObject.GetComponent<BoxCollider2D>();
        Destroy(this.gameObject,8);
    }
    void Update(){
        transform.Translate(transform.up * speed * Time.deltaTime);
        timer += Time.deltaTime;

        if(timer== 4){
            timer=0;
            myPool.Release(this);
        }
    }
    /*void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LimitCollition"))
        {
            return;
        }
    }*/
}
