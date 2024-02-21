using System.Collections;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform scoresWindow,
        topBar,
        endGameWindow,
        btns;

    private GameManager gm;

    private void Start()
    {
        gm = GetComponent<GameManager>();
    }

    public void ShowEndGame(int playerScore, int loseScore)
    {
        scoresWindow.DOAnchorPosY(10000, 0.5f);
        topBar.DOAnchorPosY(2000, 0.5f);
        endGameWindow.DOAnchorPosY(0, 0.5f);
        btns.DOAnchorPosY(75, 0.5f);
    }
}
