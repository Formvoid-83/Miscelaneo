using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy1 enemyPrefab;
    [SerializeField] private Enemy2 enemyPrefab2;
    [SerializeField] private GameObject roundsTextObj;
    private TextMeshProUGUI roundsText;
    private ObjectPool<Enemy1> enemyPool;
    private ObjectPool<Enemy2> enemyPool2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int randomPoint;
    private void Awake() {
        enemyPool = new ObjectPool<Enemy1>(createEnemy, getEnemy, releaseEnemy, destroyEnemy);
        enemyPool2 = new ObjectPool<Enemy2>(createEnemy2, getEnemy2, releaseEnemy2, destroyEnemy2);
    }
    private Enemy1 createEnemy()
    {
     Enemy1 enemyCopy =   Instantiate(enemyPrefab, transform.position, Quaternion.identity).GetComponent<Enemy1>();;
     enemyCopy.MyPool = enemyPool;
     return enemyCopy;
    }
     private void getEnemy(Enemy1 enemy)
    {
        Debug.Log("GET");
        enemy.MyPool = enemyPool; // Ensure the pool reference is still valid
        Vector2 thePosition = new Vector2(Random.Range(-9,9),transform.position.y);
        enemy.transform.position = thePosition;
        enemy.gameObject.SetActive(true);
        enemy.recharge();
    }
    private void releaseEnemy(Enemy1 enemy)
    {
        Debug.Log("RELEASE");
        enemy.gameObject.SetActive(false);
    }

    private void destroyEnemy(Enemy1 enemy)
    {
        Debug.Log("DESTROY");
        Destroy(enemy.gameObject);
    }
    private Enemy2 createEnemy2()
    {
     Enemy2 enemyCopy =   Instantiate(enemyPrefab2, transform.position, Quaternion.identity).GetComponent<Enemy2>();;
     enemyCopy.MyPool = enemyPool2;
     return enemyCopy;
    }
     private void getEnemy2(Enemy2 enemy)
    {
        Debug.Log("GET");
        enemy.MyPool = enemyPool2; // Ensure the pool reference is still valid
        Vector2 thePosition = new Vector2(Random.Range(-9,9),transform.position.y);
        enemy.transform.position = thePosition;
        enemy.gameObject.SetActive(true);
        enemy.recharge();
    }
    private void releaseEnemy2(Enemy2 enemy)
    {
        Debug.Log("RELEASE");
        enemy.gameObject.SetActive(false);
    }

    private void destroyEnemy2(Enemy2 enemy)
    {
        Debug.Log("DESTROY");
        Destroy(enemy.gameObject);
    }
    void Start()
    {
        roundsText = roundsTextObj.GetComponent<TextMeshProUGUI>();
        StartCoroutine(EnemySpawn());
    }

    IEnumerator EnemySpawn(){
        int i=0;
        while(true){
            i++;
            roundsText.text = "Round "+i;
            yield return new WaitForSeconds(1.5f);
            roundsText.text = "";
            for(int j=0; j<10;j++){
                //Vector2 thePosition = new Vector2(Random.Range(-9,9),transform.position.y);
                //Instantiate(enemyPrefab, thePosition, Quaternion.identity);
                GenerateEnemy(i,j);
                yield return new WaitForSeconds(1.5f);
            }
            yield return new WaitForSeconds(10f);
        }
        
    }
    private void GenerateEnemy(int ronda, int numenemigos){
        if(ronda%10==0){
            enemyPool2.Get();
        }
        else if(ronda%5==0 && numenemigos % 2 ==0){
            enemyPool2.Get();
        }
        else{
            enemyPool.Get();
        }
    }
}
