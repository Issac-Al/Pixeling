using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public PlayerData playerData;
    private StreamReader sr;
    private StreamWriter sw;
    public Data playerDataSO;
    public string fileName;

    // Start is called before the first frame update

    private void Start()
    {
        LoadData();
    }

    public void LoadData()
    {
        playerData = new PlayerData();
        if (File.Exists(Application.persistentDataPath
                                  + "/" + fileName))
        {
            sr = new StreamReader(Application.persistentDataPath
                                  + "/" + fileName);
            string objString = sr.ReadToEnd();
            playerData =
                JsonUtility.FromJson<PlayerData>(objString);
            playerDataSO.scoreData = playerData.scoreData;
            playerDataSO.abilities = playerData.abilities;
            playerDataSO.levelsAccomplished = playerData.levelsAccomplished;
            playerDataSO.professorEmail = playerData.professorEmail;
            playerDataSO.accountNumber = playerData.accountNumber;
            playerDataSO.name = playerData.username;
            playerDataSO.group = playerData.group;
            playerDataSO.tries = playerData.tries;
            playerDataSO.playerPosition = playerData.playerPosition;
            sr.Close();
            //Debug.Log(level);
        }
        else
        {
            playerDataSO.scoreData = new List<int>();
            playerDataSO.abilities = new List<int>();
            playerDataSO.levelsAccomplished = new List<int>();
            playerDataSO.professorEmail = "";
            playerDataSO.accountNumber = "";
            playerDataSO.name = "";
            playerDataSO.group = "";
            playerDataSO.tries = 0;
            playerDataSO.playerPosition = new Vector3(0, 0, 6.0f);
            //Debug.Log(playerDataSO.highScore);
        }
    }

    public void SaveData()
    {
        playerData.scoreData = playerDataSO.scoreData;
        playerData.abilities = playerDataSO.abilities;
        playerData.levelsAccomplished = playerDataSO.levelsAccomplished;
        playerData.professorEmail = playerDataSO.professorEmail;
        playerData.accountNumber = playerDataSO.accountNumber;
        playerData.username = playerDataSO.name;
        playerData.group = playerDataSO.group;
        playerData.tries = playerDataSO.tries;
        playerData.playerPosition = playerDataSO.playerPosition;
        sw = new StreamWriter(Application.persistentDataPath + "/" + fileName, false);
        Debug.Log(Application.persistentDataPath + "/" + fileName);
        string objString = JsonUtility.ToJson(playerData);
        sw.WriteLine(objString);
        sw.Close();
    }
}
