using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies;

    [SerializeField] private float spawnInterval = 2f; //how many seconds between each spawn

    private int randomEnemy;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(spawnEnemy(spawnInterval, enemies));
    }

    //spawn enemies
    private IEnumerator spawnEnemy(float interval, List<GameObject> spawnList)
    {
        yield return new WaitForSeconds(interval);
        randomEnemy = Random.Range(0, enemies.Count); //spawn a random enemy from list
        Instantiate(spawnList[randomEnemy], this.transform);
        
        StartCoroutine(spawnEnemy(spawnInterval, enemies)); //continue spawning
    }
}
