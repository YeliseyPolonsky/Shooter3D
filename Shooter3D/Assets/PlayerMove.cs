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
    private Vector3 velocity;
    
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
            velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
        
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);

        float speedMultiplier = 1f;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speedMultiplier = 2f;
        }
        
        worldDirection = transform.TransformVector(moveDirection) * speed * speedMultiplier;
        
        velocity = new Vector3(worldDirection.x, velocity.y, worldDirection.z);
        
        characterController.Move(velocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftControl) || MayStandUp() == false)
        {
            characterController.height = Mathf.Lerp(characterController.height, 0.9f , Time.deltaTime * 10f);
            characterController.center = Vector3.Lerp(characterController.center, Vector3.up * 0.45f , Time.deltaTime * 10f);
            head.localPosition = Vector3.Lerp(head.localPosition, Vector3.up * 0.8f , Time.deltaTime * 10f);
        }
        else
        {
            characterController.height = Mathf.Lerp(characterController.height, 1.8f, Time.deltaTime * 10f);
            characterController.center = Vector3.Lerp(characterController.center, Vector3.up * 0.9f , Time.deltaTime * 10f);
            head.localPosition = Vector3.Lerp(head.localPosition, Vector3.up * 1.6f , Time.deltaTime * 10f);
        }
    }

    private void Gravity()
    {
        if (!characterController.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else if(velocity.y < 0)
        {
            velocity.y =-1f;
        }
    }

    private bool MayStandUp()
    {
        bool hitDown = Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, out RaycastHit downRaycastHit);
        bool hitUp = Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.up, out RaycastHit upRaycastHit);

        if (upRaycastHit.distance + downRaycastHit.distance > 1.8f ||  hitUp == false)
            return true;

        return false;
    }
}
