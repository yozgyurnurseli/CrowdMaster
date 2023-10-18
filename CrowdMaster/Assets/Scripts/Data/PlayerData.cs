using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int gold;
    public int level;
    public int stackUpgrade;

    public PlayerData(GameManager player)
    {
        gold = player.gold;
        level = player.level;
        stackUpgrade = player.stackUpgrade;
    }
}
