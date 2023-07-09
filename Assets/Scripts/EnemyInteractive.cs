using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteractive : MonoBehaviour
{
    private EnemyAI _enemy;
    
    [SerializeField] private float scaleMultiplier;
    [SerializeField] private int enemyScaleCount;
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Collectable"))
        {
            enemyScaleCount++;
            Destroy(other.gameObject);            
            GrowthUp();

            CollectableSpawnner.instance.InstantiateCollectableClone();
            CollectableSpawnner.instance.collectables.Remove(other.gameObject);

            _enemy.targetLocatedPosition = false;
        }
        else if (other.gameObject.CompareTag("Water"))
        {
            _enemy.enemyNavMesh.enabled = false;
            Destroy(other.gameObject);
        }
    }

    private void GrowthUp()
    {
        transform.DOScale(ScaleEffect(enemyScaleCount), 1f);
        _enemy.enemyNavMesh.speed -= 0.2f;
    }
    private Vector3 ScaleEffect(int playerState)
    {
        float playerScale = 1.5f * playerState * scaleMultiplier;
        playerScale = Mathf.Clamp(playerScale, 5f, 15f);
        Vector3 scale = Vector3.one * playerScale;
        return scale;
    }
}
