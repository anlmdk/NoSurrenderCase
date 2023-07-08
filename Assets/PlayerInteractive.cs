using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractive : MonoBehaviour
{
    private Rigidbody rb;
    
    [SerializeField] private float scaleMultiplier;
    [SerializeField] private float pushPower;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && GameManager.instance.gameStarted)
        {

        }
    }
    private void OnCollisionExit(Collision collision)
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            GameManager.instance.EndGame();
        }
    }
}
