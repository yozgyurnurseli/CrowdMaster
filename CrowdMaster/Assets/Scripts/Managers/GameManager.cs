using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    MyStack stack;

    [SerializeField]
    int maxStack;
    public int goldEarned = 0;

    public int gold;
    public int level;
    public int stackUpgrade;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            gold = data.gold;
            level = data.level;
            stackUpgrade = data.stackUpgrade;
            if (SceneManager.GetActiveScene().buildIndex != level)
            {
                SceneManager.LoadScene(level);
            }

            UIManager.instance.SetGold(gold);
            UIManager.instance.SetStackUpgrade(stackUpgrade);
            UIManager.instance.SetLevelText(level+1);
        }

        if(gold == 0)
        {
            gold = 1000;
            UIManager.instance.SetGold(gold);
        }
        
        //gold = 0;
        //level = 0;
        //stackUpgrade = 0;
        //SaveSystem.SavePlayer(this);
    }

    private void Start()
    {
        for (int i = 0; i < stackUpgrade; i++)
        {
            stack.StackUpgrade();
        }
    }

    public int GetMaxStack()
    {
        return maxStack;
    }

    public void EarnGold()
    {
        gold += 10;
        goldEarned += 10;
        UIManager.instance.SetGold(gold);
    }

    public void StackUpgrade()
    {
        int price = (stackUpgrade + 1) * 100;

        if (gold >= price)
        {
            stackUpgrade++;
            gold -= price;

            UIManager.instance.SetStackUpgrade(stackUpgrade);
            UIManager.instance.SetGold(gold);

            stack.StackUpgrade();
            SaveSystem.SavePlayer(this);
        }
    }

    public void FinishGame()
    {
        if (level < 2)
            level++;
        else
            level = 0;

        SaveSystem.SavePlayer(this);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(level);
    }
}
