using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]


public class PlayerLevelData
{
    public int level;
    public PlayerStatus status;
    public Sozai[] nextLevelRequaimets;

    [System.Serializable]
    public class PlayerStatus
    {
        public int hp;
        public int speed;

    }
}


