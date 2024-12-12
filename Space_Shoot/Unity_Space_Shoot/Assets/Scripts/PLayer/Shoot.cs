using System;
using UnityEngine;
using UnityEngine.Pool;


public class Shoot : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] public int damage=10;
    private ObjectPool<Shoot> myPool;

    public ObjectPool<Shoot> MyPool { get => myPool; set => myPool = value; }
    private float timer;

    void Start(){
        //Destroy(this.gameObject, 4);
    }
    void Update(){
        transform.Translate(transform.up * speed * Time.deltaTime);
        timer += Time.deltaTime;

        if(timer== 4){
            timer=0;
            tryRelease();
        }
        if(this.gameObject.transform.position.y >= 7f ){
            myPool.Release(this);
        }
    }
    public void tryRelease(){
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
