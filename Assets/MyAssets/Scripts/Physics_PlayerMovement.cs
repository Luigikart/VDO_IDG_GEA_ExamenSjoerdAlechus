using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Physics_PlayerMovement : MonoBehaviour
{
    Vector2 moveInput = new Vector2();
    Camera cam => Camera.main;
    Rigidbody rb => GetComponent<Rigidbody>();


    [Header("Movement")]
    [SerializeField] float acceleration = 6f;
    [SerializeField] float sprintMultiplier = 2f;
    [SerializeField] float maxSpeed = 6f;
    [SerializeField] bool isSprinting = false;
    [SerializeField] bool fpsMode = false;
    bool isMoving = false;


    [Header("Jump")]
    [SerializeField] float JumpForce = 6f;

    [Header("Threshold")]
    [SerializeField] float VertialMotionThreshold = 0.1f;
    int jumpCount = 0 ;
    [SerializeField] int jumpLimit = 2;
    private Vector3 oldPosition; 



   

    void Update()
    {
        Vector3 getMoveDirection = MoveDirection();
        MovePlayer(getMoveDirection);
        if (isMoving&& !fpsMode) RotatePlayer(getMoveDirection);
      
    }
    
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        isMoving = moveInput.x != 0 || moveInput.y != 0;
        print(isMoving);

        
    }
    public void OnSprint(InputValue value)
    {
     

        isSprinting = value.isPressed;
    }
 


    public float GetCurrentSpeed()
    {
     
     print(rb.linearVelocity.magnitude);
        return rb.linearVelocity.magnitude;
    }

    public bool GetIsSprinting()
   {
       return isSprinting;
   }

    public void OnJump(InputValue value)
    {
        bool getJump = value.isPressed;
        bool isGrounded = GroundDetection.instance.isGrounded(transform.position);
        if (isGrounded)
        {  
            jumpCount = 0;
        }

        if (getJump && isGrounded || getJump && jumpCount <= jumpLimit)
        {   
            jumpCount ++;
            print(jumpCount);
            

            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }

    }

    Vector3 CameraAdjustedVector(Vector3 moveDirection)
    {
        Vector3 forward = cam.transform.forward * moveDirection.z;
        Vector3 right = cam.transform.right * moveDirection.x;
        Vector3 combinedDirection = forward + right;
        Vector3 finalDirection = new Vector3(combinedDirection.x, 0f, combinedDirection.z);
        return finalDirection;
    }

    Vector3 MoveDirection()
    {
        Vector3 adjustedMovement = new Vector3(moveInput.x, 0f, moveInput.y);
        Vector3 moveDirection = CameraAdjustedVector(adjustedMovement);
        return moveDirection;
    }


   
    public Vector3 GetMoveDirection()
    {
        return MoveDirection();
    }
    
    public float  GetVerticalVelocity()
    {
        return rb.linearVelocity.y;
    }

    public float CalculateCurrentSpeed()
    {
        return rb.linearVelocity.magnitude / (0.5f * sprintMultiplier);


    }
   
    float SprintConversion(float value)
    {
        float result = isSprinting ? value * sprintMultiplier : value;
        return result;
    }

    void MovePlayer(Vector3 direction)

    {
        if(!GroundDetection.instance.isGrounded(transform.position)) return;

        Vector3 forceDirection = direction * SprintConversion(acceleration) * Time.deltaTime;
    	Vector3 cachedVelocity = rb.linearVelocity + new Vector3 (forceDirection.x, 0, forceDirection.z);
        rb.linearVelocity = cachedVelocity; // meer repsonsive neegeert massa van object

       
        if(IsMovingVertically(cachedVelocity))

        {

        rb.linearVelocity =  Vector3.ClampMagnitude(cachedVelocity, SprintConversion(maxSpeed));

        }
        
       
    }
    void RotatePlayer(Vector3 direction)
    {
        if (direction == Vector3.zero) return;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    bool IsMovingVertically(Vector3 moveDirection)
    {

        return moveDirection.y <  VertialMotionThreshold || moveDirection.y > VertialMotionThreshold; 
        
    }
       
}
