using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject eney1Prefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(EnemySpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator EnemySpawn(){
        for(int i=0; i<5;i++){
                    Instantiate(eney1Prefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
        }
    }
}
