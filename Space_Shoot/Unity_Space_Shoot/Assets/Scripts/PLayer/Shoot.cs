using System;
using UnityEngine;
using UnityEngine.Pool;


public class Shoot : MonoBehaviour
{
    [SerializeField] private float speed;
    private ObjectPool<Shoot> myPool;

    public ObjectPool<Shoot> MyPool { get => myPool; set => myPool = value; }
    private float timer;

    void Start(){
        Destroy(this.gameObject, 4);
    }
    void Update(){
        transform.Translate(transform.up * speed * Time.deltaTime);
        timer += Time.deltaTime;

        if(timer== 4){
            timer=0;
            myPool.Release(this);
        }
    }
    
}
