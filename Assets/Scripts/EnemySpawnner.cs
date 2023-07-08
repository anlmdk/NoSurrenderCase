using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawnner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public int enemyCount;

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
            Debug.Log(Instantiate(enemyPrefab, GetRandomPosition(), Quaternion.identity) + "true");
        }
    }
    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(-5f, 5f);
        float y = 1f;
        float z = Random.Range(-5f, 5f);

        return new Vector3(x, y, z);
    }
}
