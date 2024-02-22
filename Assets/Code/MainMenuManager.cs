using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform topBar,
        menuScreen;

    private void Start()
    {
        menuScreen = GetComponent<RectTransform>();
    }

    public void ShowMenu()
    {
        topBar.DOAnchorPosY(0, 0.5f);
        menuScreen.DOAnchorPosX(0, 0.5f);
    }

    public void HideMenu()
    {
        topBar.DOAnchorPosY(1000, 0.5f);
        menuScreen.DOAnchorPosX(-2000, 0.5f);
    }
}
