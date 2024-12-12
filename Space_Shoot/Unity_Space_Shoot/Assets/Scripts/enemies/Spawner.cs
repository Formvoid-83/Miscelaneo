using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy1 enemyPrefab;
    [SerializeField] private GameObject roundsTextObj;
    private TextMeshProUGUI roundsText;
    private ObjectPool<Enemy1> enemyPool;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int randomPoint;
    private void Awake() {
        enemyPool = new ObjectPool<Enemy1>(createEnemy, getEnemy, releaseEnemy, destroyEnemy);
    }
    private Enemy1 createEnemy()
    {
     Enemy1 enemyCopy =   Instantiate(enemyPrefab, transform.position, Quaternion.identity);
     enemyCopy.MyPool = enemyPool;
     return enemyCopy;
    }
     private void getEnemy(Enemy1 enemy)
    {
        Debug.Log("GET");
        enemy.transform.position = transform.position;
        enemy.gameObject.SetActive(true);
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
    void Start()
    {
        roundsText = roundsTextObj.GetComponent<TextMeshProUGUI>();
        StartCoroutine(EnemySpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator EnemySpawn(){
        for(int i=0;i<3;i++){
            roundsText.text = "Round "+i;
            yield return new WaitForSeconds(1.5f);
            roundsText.text = "";
            for(int j=0; j<5;j++){
                Vector2 thePosition = new Vector2(Random.Range(-9,9),transform.position.y);
                Instantiate(enemyPrefab, thePosition, Quaternion.identity);
                yield return new WaitForSeconds(1.5f);
            }
            yield return new WaitForSeconds(10f);
        }
        
    }
}
