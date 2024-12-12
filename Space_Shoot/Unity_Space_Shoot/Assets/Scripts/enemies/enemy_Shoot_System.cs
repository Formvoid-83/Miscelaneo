using UnityEngine;
using UnityEngine.Pool;

public class enemy_Shoot_System : MonoBehaviour
{
     [SerializeField] private enemyShot enemy_shootPrefab;
    private ObjectPool<enemyShot> shootPool;
    private float shootRatio;
    private float timer;
    

    private void Awake() {
        shootPool = new ObjectPool<enemyShot>(createShot, getShot, releaseShot, destroyShot);
    }
    private enemyShot createShot()
    {
     enemyShot shotCopy =   Instantiate(enemy_shootPrefab, transform.position, Quaternion.identity);
     shotCopy.MyPool = shootPool;
     return shotCopy;
    }

    private void getShot(enemyShot shoot)
    {
        Debug.Log("GET");
        shoot.transform.position = transform.position;
        shoot.gameObject.SetActive(true);
    }
    private void releaseShot(enemyShot shoot)
    {
        Debug.Log("RELEASE");
        shoot.gameObject.SetActive(false);
    }

    private void destroyShot(enemyShot shoot)
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
           shootPool.Get();
           timer=0;
        }
    }
    
}
