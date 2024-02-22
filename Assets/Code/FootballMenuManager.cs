using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class FootballMenuManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform topBar,
        buttonsMenu,
        rulesMenu,
        statisticMenu;

    public void LoadFootball(string league)
    {
        PlayerPrefs.SetString("League", league);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GoalKeeperGame");
    }

    public void ShowFootballMenu()
    {
        buttonsMenu.DOAnchorPosX(0, 0.5f);
        topBar.DOAnchorPosY(0, 0.5f);
    }

    public void ShowStatistic()
    {
        buttonsMenu.DOAnchorPosX(-2000, 0.5f);
        statisticMenu.DOAnchorPosX(0, 0.5f);
        topBar.DOAnchorPosY(1000, 0.5f);
    }

    public void ShowRules()
    {
        buttonsMenu.DOAnchorPosX(-2000, 0.5f);
        rulesMenu.DOAnchorPosX(0, 0.5f);
        topBar.DOAnchorPosY(1000, 0.5f);
    }

    public void HideAll()
    {
        buttonsMenu.DOAnchorPosX(-2000, 0.5f);
        topBar.DOAnchorPosY(1000, 0.5f);
        rulesMenu.DOAnchorPosX(2000, 0.5f);
        statisticMenu.DOAnchorPosX(2000, 0.5F);
    }
}
