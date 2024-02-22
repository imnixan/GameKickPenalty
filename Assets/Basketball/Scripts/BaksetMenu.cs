using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class BaksetMenu : MonoBehaviour
{
    [SerializeField]
    private RectTransform topBar,
        buttonsMenu,
        rulesMenu;

    public void LoadBasket()
    {
        PlayerPrefs.SetInt("time", 0);
        PlayerPrefs.SetInt("row", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene("BasketballGame");
    }

    public void ShowBasketMenu()
    {
        buttonsMenu.DOAnchorPosX(0, 0.5f);
        topBar.DOAnchorPosY(0, 0.5f);
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
    }
}
