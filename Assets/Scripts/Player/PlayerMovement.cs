using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public FloatingJoystick joystick;

    public ParticleSystem walkEffect;

    private Animator anim;
    private Rigidbody rb;
    private Vector3 direction;

    public bool moveable;

    public float moveSpeed;
    public float rotationSpeed;

    private string IS_WALKING = "isWalking";

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        walkEffect = GetComponent<ParticleSystem>();
        walkEffect.Stop();
    }
    
    void Update()
    {
        if (GameManager.instance.gameStarted is true)
        {
            moveable = true;
            Move();
            Rotate();
        }
    }
    // Handle player movement
    private void Move()
    {
        if (moveable)
        {
            rb.velocity = transform.forward * moveSpeed;

            if (rb.velocity.magnitude > 0.1f) 
            {
                anim.SetBool(IS_WALKING, true);
                walkEffect.Play();
            }
            
        }
    }
    // Handle player rotation
    private void Rotate()
    {
        float x = joystick.Horizontal;
        float z = joystick.Vertical;

        direction = new Vector3(x, 0, z);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion toRotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
