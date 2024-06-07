using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/Data")]
public class Data : ScriptableObject
{
    public List<int> scoreData, levelsAccomplished, abilities;
    public float tries, score;
    public Vector3 playerPosition;
    public string username, accountNumber, professorEmail, group;
}

