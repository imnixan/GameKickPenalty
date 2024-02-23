using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform topBar,
        menuScreen,
        findScreen;

    private void Start()
    {
        menuScreen = GetComponent<RectTransform>();
    }

    public void ShowMenu()
    {
        topBar.DOAnchorPosY(0, 0.5f);
        menuScreen.DOAnchorPosX(0, 0.5f);
        findScreen.DOAnchorPosX(2000, 0.5f);
    }

    public void HideMenu()
    {
        topBar.DOAnchorPosY(1000, 0.5f);
        menuScreen.DOAnchorPosX(-2000, 0.5f);
    }

    public void ShowFindGame()
    {
        HideMenu();
        findScreen.DOAnchorPosX(0, 0.5f);
    }

    public void PlayFindGame()
    {
        PlayerPrefs.SetInt("score", 0);
        PlayerPrefs.SetInt("time", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene("FindTheTrophy");
    }
}
