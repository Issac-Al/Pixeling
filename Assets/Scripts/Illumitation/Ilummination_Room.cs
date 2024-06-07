using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ilummination_Room : MonoBehaviour
{
    public DataManager dataManager;
    private bool zoneCompleted = false;
    private void Start()
    {
        foreach(int level in dataManager.playerDataSO.levelsAccomplished)
        {
            if(level == 1)
            {
                zoneCompleted = true;
                break;
            }
        }

        if (zoneCompleted)
        {
            foreach (GameObject light in GameObject.FindGameObjectsWithTag("Light"))
            {
                light.SetActive(true);
            }
        }
    }
}
