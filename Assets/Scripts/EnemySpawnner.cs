using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawnner : MonoBehaviour, IGetRandomPosition
{
    public static EnemySpawnner instance;

    [SerializeField] private Transform parentTransform;
    public GameObject enemyPrefab;

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
    // Instantiate enemy objects at random positions
    public void InstantiateEnemy()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Instantiate(enemyPrefab, GetRandomPosition(), Quaternion.identity , parentTransform);
        }
    }
    // Get a random position within a specified range
    public Vector3 GetRandomPosition()
    {
        float x = Random.Range(-15f, 15f);
        float y = 1f;
        float z = Random.Range(-15f, 15f);

        return new Vector3(x, y, z);
    }
}
