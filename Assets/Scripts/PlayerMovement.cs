using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public FloatingJoystick joystick;

    private Animator anim;
    private Rigidbody rb;
    private Vector3 direction;
    [SerializeField] private float smoothTime;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    private Vector3 targetVelocity;
    private Vector3 currentVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        Move();
        Rotate();
    }
    private void Move()
    {
        float x = joystick.Horizontal;
        float z = joystick.Vertical;

        Vector3 movement = new Vector3(x, 0, z);
        targetVelocity = movement * moveSpeed;

        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, smoothTime);

        if (rb.velocity.magnitude > 0.1f)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }

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
