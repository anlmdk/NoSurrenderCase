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
    private void FindNearestCollectable()
    {
        int randomIndex = Random.Range(0, CollectableSpawnner.instance.collectables.Count);
        var randomTargetCollectable = CollectableSpawnner.instance.collectables[randomIndex];

        if (!enemyNavMesh.enabled) return;

        enemyNavMesh.SetDestination(randomTargetCollectable.transform.position);
        targetLocatedPosition = true;
    }
    private IEnumerator TargetLocatedFalse()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            if (enemyNavMesh.enabled) targetLocatedPosition = false;
        }
    }
}
