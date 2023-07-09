using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteractive : MonoBehaviour
{
    private EnemyAI _enemy;
    
    [SerializeField] private float scaleMultiplier;
    [SerializeField] private int enemyScaleCount;

    private void Start()
    {
        _enemy = GetComponent<EnemyAI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Collectable"))
        {
            enemyScaleCount++;
            Destroy(other.gameObject);            
            GrowthUp();

            CollectableSpawnner.instance.InstantiateCollectableClone(); 
            CollectableSpawnner.instance.collectables.Remove(other.gameObject); 

            _enemy.targetLocatedPosition = false; // Reset the target location flag
        }
        else if (other.gameObject.CompareTag("Water"))
        {
            _enemy.enemyNavMesh.enabled = false;
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
            EnemySpawnner.instance.enemyCount -= 1; // Decrease the enemy count

            if (EnemySpawnner.instance.enemyCount == 0)
            {
                GameManager.instance.EndGame();  // Trigger the end of the game
            }
        }
    }
    // Scale up the enemy and adjust movement speed 
    private void GrowthUp()
    {
        transform.DOScale(ScaleEffect(enemyScaleCount), 1f);
        _enemy.enemyNavMesh.speed -= 0.2f;
    }
    private Vector3 ScaleEffect(int currentScale)
    {
        float enemyScale = 1.5f * currentScale * scaleMultiplier; 
        enemyScale = Mathf.Clamp(enemyScale, 5f, 15f); 
        Vector3 scale = Vector3.one * enemyScale;
        return scale;
    }
}
