using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject continueButton, newGameButton, newGamePanel, optionsPanel;
    public DataManager dataManager;
    // Start is called before the first frame update

    public void ChangeGameButton()
    {
        Debug.Log(dataManager.dataExist);

        if (dataManager.dataExist)
        {
            continueButton.SetActive(true);
        }
        else
        {
            newGameButton.SetActive(true);
        }
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(1);
    }

    public void NewGame()
    {
        newGamePanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Options()
    {
        optionsPanel.SetActive(true);
    }

    
}
