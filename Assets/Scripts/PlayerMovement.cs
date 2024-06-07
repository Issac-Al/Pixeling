using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public Transform virtualCamera;
    public Transform followTransform;
    private float rotationPower = 150f;
    private Vector2 _look;
    //public Transform playerModel;
    private Animator _animator;
    public DataManager dataManager;

    private float turnSmoothVelocity;

    private void Start()
    {
        transform.position = dataManager.playerDataSO.playerPosition;
        Cursor.lockState = CursorLockMode.Locked;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 _move = new Vector2(horizontal, vertical);

        #region Player Based Rotation
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime;
        
        _look = new Vector2(mouseX, mouseY);

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //Move the player based on the X input on the controller
        //transform.rotation *= Quaternion.AngleAxis(_look.x * rotationPower, Vector3.up);

        #endregion

        #region Follow Transform Rotation

        //Rotate the Follow Target transform based on the input
        followTransform.transform.rotation *= Quaternion.AngleAxis(_look.x * rotationPower, Vector3.up);

        #endregion

        #region Vertical Rotation
        followTransform.transform.rotation *= Quaternion.AngleAxis(_look.y * rotationPower, Vector3.right);

        var angles = followTransform.transform.eulerAngles;
        //Debug.Log("angles" + angles);
        angles.z = 0;

        var angle = followTransform.transform.eulerAngles.x;
        //Debug.Log("angle" + angle);

        //Clamp the Up/Down rotation
        if (angle > 180 && angle < 340)
        {
            angles.x = 340;
        }
        else if (angle < 180 && angle > 40)
        {
            angles.x = 40;
        }


        followTransform.transform.eulerAngles = angles;
        #endregion


        //nextRotation = Quaternion.Lerp(followTransform.transform.rotation, nextRotation, Time.deltaTime * rotationLerp);

        //Debug.Log(aimValue);

        float moveSpeed = speed / 100f;
        Vector3 position = (transform.forward * _move.y * moveSpeed) + (transform.right * _move.x * moveSpeed);
        //Vector3 position = transform.position * _move.y * moveSpeed +  _move.x * moveSpeed;
        //nextPosition = transform.position + position;

        Vector3 camForward = followTransform.forward;
        Vector3 camRight = followTransform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();
        Vector3 moveDirection = camForward * vertical + camRight * horizontal;

        //Set the player rotation based on the look transform
        if (horizontal != 0 || vertical != 0)
        {
            _animator.SetBool("moving", true);
            // Calcula el ángulo de rotación basado en la dirección del movimiento
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;

            // Calcula el ángulo de rotación para el giro suave
            float Charangle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            // Aplica la rotación al transform del personaje
            transform.rotation = Quaternion.Euler(0f, Charangle, 0f);

            // Mueve al personaje usando el CharacterController
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);

        }
        else
        {
            _animator.SetBool("moving", false);
        }

    }
}
