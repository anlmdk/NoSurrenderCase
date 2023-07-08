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
    
    public GameObject[] collectables; 

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

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !GameManager.instance.gameStarted)
        {
            GameManager.instance.gameStarted = true;
            InstantiateCollectable();
        }
    }
    private void InstantiateCollectable()
    {
        for (int i = 0; i < collectableCount; i++)
        {
            Instantiate(collabtablePrefab, GetRandomPosition(), Quaternion.identity, transform);
        }
    }
    private void InstantiateCollectableClone()
    {
        GameObject clone = Instantiate(collabtablePrefab, GetRandomPosition(), Quaternion.identity, transform);
        clone.transform.localScale = Vector3.zero;
        clone.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 0.3f);
        
    }
    public Vector3 GetRandomPosition()
    {
        float x = Random.Range(-10f, 10f);
        float y = 1.5f;
        float z = Random.Range(-10f, 10f);

        return new Vector3(x, y, z);
    }
}
