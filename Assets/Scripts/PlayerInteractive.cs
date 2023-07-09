using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractive : MonoBehaviour
{
    private UIManager _uiManager;
    private PlayerMovement _player;

    public ParticleSystem waterEffect;

    private Rigidbody rb;
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

    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && GameManager.instance.gameStarted)
        {
            _player.moveable = true;
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
    }
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
    private void CollectableInteraction()
    {
        GameManager.instance.score += 100;
        scaleCount++;
        GrowthUp();        
    }
    private void GrowthUp()
    {
        transform.DOScale(ScaleEffect(scaleCount), 1f);
        _player.moveSpeed -= 0.2f;
        pushPower += 5;
    }
    private Vector3 ScaleEffect(int playerState)
    {
        float playerScale = 1.5f * playerState * scaleMultiplier;
        playerScale = Mathf.Clamp(playerScale, 5f, 15f);
        Vector3 scale = Vector3.one * playerScale;
        return scale;
    }
}
