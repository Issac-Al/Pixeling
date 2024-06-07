using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.WSA;

public class CameraMovement : MonoBehaviour
{
    //public Transform cameraTransform;
    public Transform playerTransform;
    private Vector3 pivotPosition;
    //public Camera mainCamera;
    private Vector3 position;
    private float zoomMultiplier = 0.1f;
    private Vector3 pivotRotation;
    private bool LeftClickPressed = false;
    private bool RightClickPressed = false;
    private Vector2 LastMousePosition;
    private float cameraMovementHorizontalMultiplier = 0.0005f;
    private float cameraMovementVerticalMultiplier = 0.0005f;
    // Start is called before the first frame update
    // Update is called once per frame

    private void Start()
    {
        //position = cameraTransform.localPosition;
        pivotRotation = transform.localEulerAngles;
        pivotPosition = transform.localPosition;
        

    }

    void Update()
    {
        
        

        //transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
        //followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);

        //Debug.Log(position.z);


    }
}
