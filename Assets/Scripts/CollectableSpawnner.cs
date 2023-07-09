using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class CollectableSpawnner : MonoBehaviour, IGetRandomPosition
{
    public static CollectableSpawnner instance;

    [SerializeField] private Transform parentTransform;
    [SerializeField] private GameObject collabtablePrefab;

    [SerializeField] private float collectableCount;
    
    public List<GameObject> collectables; 

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
    private void Start()
    {
        collectables = new List<GameObject>();
    }
    // Instantiate collectable objects at random positions
    public void InstantiateCollectable()
    {
        for (int i = 0; i < collectableCount; i++)
        {
            Instantiate(collabtablePrefab, GetRandomPosition(), Quaternion.identity, parentTransform);
        }
    }
    // Instantiate a clone of a collectable object at a random position
    public void InstantiateCollectableClone()
    {
        GameObject clone = Instantiate(collabtablePrefab, GetRandomPosition(), Quaternion.identity, parentTransform);
        clone.transform.localScale = Vector3.zero;
        clone.transform.DOScale(new Vector3(10f, 20f, 10f), 0.3f);
        collectables.Add(clone);
        
    }
    // Get a random position within a specified range
    public Vector3 GetRandomPosition()
    {
        float x = Random.Range(-15f, 15f);
        float y = 1.5f;
        float z = Random.Range(-15f, 15f);

        return new Vector3(x, y, z);
    }
}
