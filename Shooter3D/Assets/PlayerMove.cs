using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private float mouseSensitivity = 10f;
    [SerializeField] private Transform head;

    private float xRotation;
    
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float jumpHeight = 3f;
    
    private Vector3 moveDirection;
    private Vector3 worldDirection;
    
    [SerializeField] private float gravity = -20f;
    private float yVelocity;
    
    void Update()
    {
        Rotate();
        Gravity();
        Move();
    }

    private void Rotate()
    {
        transform.Rotate(0,Input.GetAxis("Mouse X") * mouseSensitivity,0);
        
        xRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        head.localRotation = Quaternion.Euler(new Vector3(xRotation,0,0));
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
            yVelocity += jumpHeight;
        
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        moveDirection = Vector3.Normalize(moveDirection);

        worldDirection = transform.TransformVector(moveDirection);
        characterController.Move(worldDirection * speed * Time.deltaTime 
                                 + Vector3.up * yVelocity * Time.deltaTime);
    }

    private void Gravity()
    {
        if (!characterController.isGrounded)
        {
            yVelocity += gravity * Time.deltaTime;
        }
        else
        {
            yVelocity = -0.5f;
        }
    }
}
