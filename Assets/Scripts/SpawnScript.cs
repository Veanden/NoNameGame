using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{

    private GameObject ExplodingPrefab;
    private GameObject EmoPrefab;
    private GameObject EnemyPrefab;
    private bool spawning = false;
    [SerializeField] private float spawnRate = 5f;
    [SerializeField] private List<GameObject> EnemyPrefabsList;
    private int enemy;

    void Start()
    {
        ExplodingPrefab = Resources.Load<GameObject>("prefabs/Enemies/ExplodingBellyEnemy");
        EnemyPrefabsList.Add(ExplodingPrefab);
        EmoPrefab = Resources.Load<GameObject>("prefabs/Enemies/EmoBelly");
        EnemyPrefabsList.Add(EmoPrefab);
    }

    void Update()
    {
        if (!spawning)
        {
            StartCoroutine(SpawnEnemy());
            spawning = true;
        }
    }

    IEnumerator SpawnEnemy()
    {
        enemy = Random.Range(0, EnemyPrefabsList.Count);

        EnemyPrefab = EnemyPrefabsList[enemy];


        yield return new WaitForSeconds(spawnRate);

        Instantiate(EnemyPrefab, transform.position, transform.rotation);

        spawning = false;
    }
}
