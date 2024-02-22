using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System;

public class ScoresManager : MonoBehaviour
{
    [SerializeField]
    private Image[] balls;

    [SerializeField]
    private TextMeshProUGUI endWin,
        endLose,
        gamescore,
        league,
        time,
        result;

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
        league.text = PlayerPrefs.GetString("League", "premier league");
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
        time.text = DateTime.Now.ToString("HH:mm");
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
            result.text = scores > loses ? "You win!" : "Game over";
            PlayerPrefs.SetInt(league.text + "player", scores);
            PlayerPrefs.SetInt(league.text + "lose", loses);
            PlayerPrefs.Save();
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
