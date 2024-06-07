using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public List<int> scoreData, levelsAccomplished, abilities;
    public float tries, score;
    public Vector3 playerPosition;
    public string username, accountNumber, professorEmail, group;
}

