using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawnner : MonoBehaviour
{
    public static EnemySpawnner instance;

    public GameObject enemyPrefab;
    public Transform enemyParentTransform;

    public int enemyCount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !GameManager.instance.gameStarted)
        {
            GameManager.instance.gameStarted = true;
            InstantiateEnemy();
        }
    }
    private void InstantiateEnemy()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Instantiate(enemyPrefab, GetRandomPosition(), Quaternion.identity,enemyParentTransform);
        }
    }
    public Vector3 GetRandomPosition()
    {
        float x = Random.Range(-2f, 2f);
        float y = 1f;
        float z = Random.Range(-2f, 2f);

        return new Vector3(x, y, z);
    }
}
