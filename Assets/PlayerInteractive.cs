using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractive : MonoBehaviour
{
    private UIManager _uiManager;
    private PlayerMovement _player;

    private Rigidbody rb;
    private Animator anim;

    [SerializeField] private float scaleMultiplier;
    [SerializeField] private float pushPower;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        _player = GetComponent<PlayerMovement>();
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
            WaterInteraction();
        }
        else if (other.gameObject.CompareTag("Collectable"))
        {
            CollectableInteraction();
            Destroy(other.gameObject);
        }
    }
    private void WaterInteraction()
    {
        anim.SetTrigger("isDying");

        _player.moveable = false;

        PlayerCameraTracking cam = GetComponent<PlayerCameraTracking>();
        if (cam != null)
        {
            cam.enabled = false;
        }
        GameManager.instance.EndGame();
    }
    private void CollectableInteraction()
    {
        GameManager.instance.score += 100;
    }
}
