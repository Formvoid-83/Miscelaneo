using System;
using UnityEngine;
using UnityEngine.Pool;

public class enemy_Shoot_System : MonoBehaviour
{
     [SerializeField] private GameObject enemy_shot1Prefab;
     [SerializeField] private GameObject enemy_shot2Prefab;
     [SerializeField] private GameObject spawnPoint1;
     [SerializeField] private GameObject[] spawnArray;
    private ObjectPool<enemyShot> enemyShot1Pool;
    private ObjectPool<enemyShot2> enemyShot2Pool;
    private EnemyType enemyType;
    private float shootRatio;
    private float timer;
    

    private void Awake() {
        // Determine the enemy type
        if (TryGetComponent(out Enemy1 enemy1))
        {
            enemyType = enemy1.enemyType;
            enemyShot1Pool = new ObjectPool<enemyShot>(createShot1, getShot1, releaseShot1, destroyShot1);
        }
        else if (TryGetComponent(out Enemy2 enemy2))
        {
            enemyType = enemy2.enemyType;
            enemyShot2Pool = new ObjectPool<enemyShot2>(createShot2, null, releaseShot2, destroyShot2);
        }
        else
        {
            Debug.LogError("Enemy type not identified.");
        }
    }
    private enemyShot createShot1()
    {
     enemyShot shotCopy =   Instantiate(enemy_shot1Prefab, spawnPoint1.transform.position, Quaternion.identity).GetComponent<enemyShot>();
     shotCopy.MyPool = enemyShot1Pool;
     return shotCopy;
    }

    private void getShot1(enemyShot shoot)
    {
        Debug.Log("GET");
        shoot.transform.position = transform.position;
        shoot.gameObject.SetActive(true);
    }
    private void releaseShot1(enemyShot shoot)
    {
        Debug.Log("RELEASE");
        shoot.gameObject.SetActive(false);
    }

    private void destroyShot1(enemyShot shoot)
    {
        Debug.Log("DESTROY");
        Destroy(shoot.gameObject);
    }
    private enemyShot2 createShot2()
    {
     enemyShot2 shotCopy =   Instantiate(enemy_shot2Prefab, gameObject.transform.position, Quaternion.identity).GetComponent<enemyShot2>();
     shotCopy.MyPool = enemyShot2Pool;
     return shotCopy;
    }

    private void getShot2()
    {
        for(int i = 0 ; i<spawnArray.Length;i++){
             Debug.Log("GET");
            enemyShot2 shot= enemyShot2Pool.Get();
            shot.gameObject.SetActive(true);
            shot.transform.position = spawnArray[i].transform.position;
        }
       
    }
    private void releaseShot2(enemyShot2 shoot)
    {
        Debug.Log("RELEASE");
        shoot.gameObject.SetActive(false);
    }

    private void destroyShot2(enemyShot2 shoot)
    {
        Debug.Log("DESTROY");
        Destroy(shoot.gameObject);
    }


    private void Start() {
        shootRatio = 0.4f;
    }

    void Update(){
        timer += Time.deltaTime;
    }
    public void ShootNow(){
        if(timer > shootRatio){
            if(enemyType == EnemyType.Enemy1){
                enemyShot1Pool.Get();   
            }
            else{
                getShot2();
            }
            timer=0;
        }
    }
    
}
