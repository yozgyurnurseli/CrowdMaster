using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField]
    Transform canvas;

    Transform startUI;
    Transform gameUI;
    Transform finishUI;

    RectTransform stackBarFill;
    TextMeshProUGUI stackText;
    TextMeshProUGUI goldText;
    TextMeshProUGUI stackUpgradeText;
    TextMeshProUGUI earnedGoldText;

    TextMeshProUGUI levelText1;
    TextMeshProUGUI levelText2;

    int maxStack;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        startUI = canvas.GetChild(0);
        gameUI = canvas.GetChild(1).GetChild(2);
        stackBarFill = gameUI.GetChild(0).GetChild(0).GetComponent<RectTransform>();
        stackText = stackBarFill.GetChild(0).GetComponent<TextMeshProUGUI>();

        goldText = canvas.GetChild(1).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
        stackUpgradeText = startUI.GetChild(4).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();

        finishUI = canvas.GetChild(2);
        earnedGoldText = finishUI.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();

        levelText1 = startUI.GetChild(2).GetComponent<TextMeshProUGUI>(); ;
        levelText2 = gameUI.GetChild(1).GetComponent<TextMeshProUGUI>(); ;
    }

    private void Start()
    {
        maxStack = GameManager.instance.GetMaxStack();
    }

    public void StartGame()
    {
        startUI.gameObject.SetActive(false);
        gameUI.gameObject.SetActive(true);
    }

    public void SetLevelText(int level)
    {
        levelText1.text = "Level " + level.ToString();
        levelText2.text = "Level " + level.ToString();
    }

    public void SetStackCount(float count)
    {
        float width = count / maxStack * 350f;
        stackBarFill.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);

        stackText.text = count.ToString();
    }

    public void SetGold(int gold)
    {
        goldText.text = gold.ToString();
    }

    public void SetStackUpgrade(int stackUpgrade)
    {
        stackUpgradeText.text = ((stackUpgrade + 1)*100).ToString();
    }

    public void FinishGame()
    {
        gameUI.parent.gameObject.SetActive(false);
        finishUI.gameObject.SetActive(true);
        earnedGoldText.text = "+" + GameManager.instance.goldEarned.ToString();
        GameManager.instance.FinishGame();
    }
}
