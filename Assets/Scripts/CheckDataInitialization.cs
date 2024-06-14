using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDataInitialization : MonoBehaviour
{
    public DataManager dataMananger;
    public MainMenu mainMenu;

    private void Update()
    {
        if (dataMananger.dataInitialized)
        {
            mainMenu.ChangeGameButton();
            this.enabled = false;
        }
    }

}
