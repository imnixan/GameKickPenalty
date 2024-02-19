using System.Collections;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform scoresWindow,
        hpWindow,
        endGameWindow;

    [SerializeField]
    private float scoresShowY,
        scoresHideY,
        hpShowY,
        hpHideY,
        endShowY,
        endHideY;

    private Sequence showGameUI,
        showEndGameUi,
        hideEndGameUi;

    private GameManager gm;

    private void Start()
    {
        gm = GetComponent<GameManager>();
        InitHideWindows();
        CreateAnims();
        ShowGameUi();
    }

    private void InitHideWindows()
    {
        scoresWindow.anchoredPosition = new Vector2(scoresWindow.anchoredPosition.x, scoresHideY);
        hpWindow.anchoredPosition = new Vector2(hpWindow.anchoredPosition.x, hpHideY);
        endGameWindow.anchoredPosition = new Vector2(endGameWindow.anchoredPosition.x, endHideY);
    }

    private void CreateAnims()
    {
        showGameUI = DOTween.Sequence();
        showGameUI.Append(scoresWindow.DOAnchorPosY(scoresShowY, 0.3f));
        showGameUI.Join(hpWindow.DOAnchorPosY(hpShowY, 0.3f));

        showEndGameUi = DOTween.Sequence();
        showEndGameUi.Append(scoresWindow.DOAnchorPosY(scoresHideY, 0.3f));
        showEndGameUi.Join(hpWindow.DOAnchorPosY(hpHideY, 0.3f));
        showEndGameUi.Append(endGameWindow.DOAnchorPosY(endShowY, 0.3f));

        hideEndGameUi = DOTween.Sequence();
        hideEndGameUi.Append(endGameWindow.DOAnchorPosY(endHideY, 0.3f));
        hideEndGameUi.AppendCallback(gm.LoadNextScene);
    }

    public void ReloadAnim()
    {
        hideEndGameUi.Restart();
    }

    public void ShowGameUi()
    {
        showGameUI.Restart();
    }

    public void ShowEndGame()
    {
        showEndGameUi.Restart();
    }
}
