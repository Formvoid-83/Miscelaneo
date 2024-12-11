using System;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Pool;

public class Shoot_System : MonoBehaviour
{
    [SerializeField] private Shoot shootPrefab;
    private ObjectPool<Shoot> shootPool;
    private float shootRatio;
    private float timer;
    

    private void Awake() {
        shootPool = new ObjectPool<Shoot>(createShot, getShot, releaseShot, destroyShot);
    }
    private Shoot createShot()
    {
     Shoot shotCopy =   Instantiate(shootPrefab, transform.position, Quaternion.identity);
     shotCopy.MyPool = shootPool;
     return shotCopy;
    }

    private void getShot(Shoot shoot)
    {
        Debug.Log("GET");
        shoot.transform.position = transform.position;
        shoot.gameObject.SetActive(true);
    }
    private void releaseShot(Shoot shoot)
    {
        Debug.Log("RELEASE");
        shoot.gameObject.SetActive(false);
    }

    private void destroyShot(Shoot shoot)
    {
        Debug.Log("DESTROY");
        Destroy(shoot.gameObject);
    }


    private void Start() {
        shootRatio = 0.4f;
    }

    void Update(){
        timer += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.Space) && timer > shootRatio){
           shootPool.Get();
           timer=0;
           //shootCopy.gameObject.SetActive(true);
        }
    }
}
