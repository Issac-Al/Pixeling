using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DoorMovement : MonoBehaviour
{
    //public Animator animator;
    public float delay = 0.1f; // Retardo entre cada letra
    public bool opening = false;
    public GameObject door;
    public float speed = 40.0f;
    public float targetYPosition = 1.44f;
    public float targetYFloorPosition = -1.95f;
    public int LevelRequiredToOpen;
    private bool opened = false;
    public DataManager dataManager;

    private void Start()
    {
        foreach(int level in dataManager.playerDataSO.levelsAccomplished)
        {
            if(level == LevelRequiredToOpen)
            {
                opened = true;
            }
        }
    }

    private void Update()
    {
        float ypos = door.transform.localPosition.y;
        //Debug.Log(ypos);
        if (opening && opened)
        {
            float currentYPosition = door.transform.localPosition.y;

            if (currentYPosition < targetYPosition)
            {
                // Calcula la nueva posición usando Time.deltaTime para movimiento suave
                float newYPosition = currentYPosition + speed * Time.deltaTime;

                // Asegura que la posición no exceda la posición objetivo
                newYPosition = Mathf.Min(newYPosition, targetYPosition);

                // Actualiza la posición de la puerta
                door.transform.localPosition = new Vector3(door.transform.localPosition.x, newYPosition, door.transform.localPosition.z);
            }
        }
        if (!opening && opened)
        {
            float currentYPositionFloor = door.transform.localPosition.y;

            if (currentYPositionFloor > targetYFloorPosition)
            {
                // Calcula la nueva posición usando Time.deltaTime para movimiento suave
                float newYPositionFloor = currentYPositionFloor - speed * Time.deltaTime;

                // Asegura que la posición no exceda la posición objetivo
                newYPositionFloor = Mathf.Max(newYPositionFloor, targetYFloorPosition);

                // Actualiza la posición de la puerta
                door.transform.localPosition = new Vector3(door.transform.localPosition.x, newYPositionFloor, door.transform.localPosition.z);
            }
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        opening = true;
    }

    private void OnTriggerExit(Collider other)
    {
        opening = false;
    }


}
