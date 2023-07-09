using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerInteractive : MonoBehaviour
{
    private UIManager _uiManager;
    private PlayerMovement _player;

    public ParticleSystem waterEffect;

    private Rigidbody rb , newRb;
    private Animator anim;

    [SerializeField] private float scaleMultiplier;
    [SerializeField] private int scaleCount;
    [SerializeField] private float pushPower;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        _player = GetComponent<PlayerMovement>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && GameManager.instance.gameStarted)
        {
            _player.moveable = true;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            _player.moveable = false;
            collision.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            StartCoroutine(EnemyNavmeshActivate(collision.gameObject));

            rb.AddForce(-transform.forward * pushPower / 4, ForceMode.Impulse);
            Invoke(nameof(MoveableAfterPush), pushPower / 160);

            newRb = collision.gameObject.GetComponent<Rigidbody>();
            newRb.AddForce(transform.forward * pushPower, ForceMode.Impulse);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            _player.moveable = false;
        }
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

            CollectableSpawnner.instance.InstantiateCollectableClone();
            CollectableSpawnner.instance.collectables.Remove(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            _player.moveable = false;
            other.gameObject.GetComponent<NavMeshAgent>().enabled = false;

            StartCoroutine(EnemyNavmeshActivate(other.gameObject));

            rb.AddForce(-transform.forward * pushPower / 2, ForceMode.Impulse);
            Invoke(nameof(MoveableAfterPush), pushPower / 160);

            newRb = other.GetComponent<Rigidbody>();
            newRb.AddForce(transform.forward * (pushPower * 20f), ForceMode.Impulse);
        }
    }
    // Activate and deactivate the NavMeshAgent component of the enemy
    private IEnumerator EnemyNavmeshActivate(GameObject enemy)
    {
        enemy.GetComponent<NavMeshAgent>().enabled = false; 

        yield return new WaitForSeconds(.2f);

        enemy.GetComponent<NavMeshAgent>().enabled = true;
    }
    // Enable player movement after pushing
    private void MoveableAfterPush()
    {
        _player.moveable = true;
    }
    // Handle water interaction
    private void WaterInteraction()
    {
        anim.SetTrigger("isDying");

        _player.moveable = false;
        waterEffect.Play();

        PlayerCameraTracking cam = GetComponent<PlayerCameraTracking>();
        if (cam != null)
        {
            cam.enabled = false;
        }
        GameManager.instance.EndGame();
    }
    // Handle collectable interaction
    private void CollectableInteraction()
    {
        GameManager.instance.score += 100;
        scaleCount++;
        GrowthUp();        
    }
    // Scale up the player and adjust movement speed and push power
    private void GrowthUp()
    {
        transform.DOScale(ScaleEffect(scaleCount), 1f);
        _player.moveSpeed -= 0.2f;
        pushPower += 5;
    }
    // Calculate the scale effect based on player state
    private Vector3 ScaleEffect(int playerState)
    {
        float playerScale = 1.5f * playerState * scaleMultiplier;
        playerScale = Mathf.Clamp(playerScale, 5f, 15f);
        Vector3 scale = Vector3.one * playerScale;
        return scale;
    }
}
