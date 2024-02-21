using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ScoresManager : MonoBehaviour
{
    [SerializeField]
    private Image[] balls;

    [SerializeField]
    private TextMeshProUGUI endWin,
        endLose,
        gamescore;

    [SerializeField]
    private RectTransform scoreBoard;
    private int scores;
    private int loses;

    private int kicksLeft = 5;
    private GameManager gm;
    private Kicker kicker;

    private void Start()
    {
        kicker = FindAnyObjectByType<Kicker>();
        gm = FindAnyObjectByType<GameManager>();
    }

    public void AddScore()
    {
        scores++;
        CheckEndGame();
    }

    private void ShowScore()
    {
        gamescore.text = $"{scores}:{loses}";
        kicker.enabled = false;
        Sequence showScore = DOTween.Sequence();
        showScore
            .Append(scoreBoard.DOAnchorPosX(0, 0.5f))
            .AppendInterval(1.25f)
            .Append(scoreBoard.DOAnchorPosX(2000, 0.5f))
            .AppendCallback(() =>
            {
                kicker.enabled = true;
            })
            .Restart();
    }

    private void CheckEndGame()
    {
        kicksLeft--;
        if (kicksLeft == 0)
        {
            gm.GameEnd(scores, loses);
        }
        if (kicksLeft >= 0)
        {
            UpdateKicks();
        }
        if (kicksLeft > 0)
        {
            ShowScore();
        }
        endWin.text = scores.ToString();
        endLose.text = loses.ToString();
    }

    public void AddLose()
    {
        loses++;
        CheckEndGame();
    }

    private void UpdateKicks()
    {
        balls[kicksLeft].DOColor(new Color(0.5f, 0.5f, 0.5f, 0.2f), 0.5f);
    }
}
