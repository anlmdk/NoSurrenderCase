using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent enemyNavMesh;
    public bool targetLocatedPosition;

    void Start()
    {
        enemyNavMesh = GetComponent<NavMeshAgent>();
        StartCoroutine(TargetLocatedFalse());
    }
    void Update()
    {
        if (!targetLocatedPosition)
        {
            FindNearestCollectable();
        }
    }
    // Find the nearest collectable object and set it as the destination for the enemy
    private void FindNearestCollectable()
    {
        if (CollectableSpawnner.instance.collectables.Count == 0)
        {
            return;
        }

        List<GameObject> collectablesCopy = new List<GameObject>(CollectableSpawnner.instance.collectables);

        int randomIndex = Random.Range(0, collectablesCopy.Count);
        var randomTargetCollectable = collectablesCopy[randomIndex];

        if (enemyNavMesh.isActiveAndEnabled)
        {
            enemyNavMesh.SetDestination(randomTargetCollectable.transform.position);
            targetLocatedPosition = true;
        }
    }
    // Continuously reset the targetLocatedPosition flag at regular intervals
    private IEnumerator TargetLocatedFalse()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            if (enemyNavMesh.enabled) targetLocatedPosition = false;
        }
    }
}
