using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //private List<Transform> spawnPoints;
    [SerializeField] private Transform SpawnPositionsWrapper;
    private Transform[] spawnPoints;
    public float spawnInterval = 5;
    public int stage = 0;
    public Transform enemyContainer;

    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject eyeball;


    void Start()
    {
        spawnPoints = SpawnPositionsWrapper.GetComponentsInChildren<Transform>();
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        WaitForSeconds delay = new WaitForSeconds(spawnInterval);
        for (int i = 1; i < spawnPoints.Length; i++)
        {
            if (stage == 0)
            {
                SpawnStageZero(spawnPoints[i]);
            }
        }
        yield return delay;
        StartCoroutine(SpawnEnemies());
    }


    private void SpawnStageZero(Transform spawnPoint)
    {
        GameObject spawnedEnemy = Instantiate(eyeball, spawnPoint);
        spawnedEnemy.transform.SetParent(enemyContainer);
    }
}



