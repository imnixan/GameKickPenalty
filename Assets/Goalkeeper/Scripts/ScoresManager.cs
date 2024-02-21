using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScoresManager : MonoBehaviour
{
    [SerializeField]
    private Image[] balls;

    private int scores;
    private int loses;

    private int kicksLeft = 5;
    private GameManager gm;

    private void Start()
    {
        gm = FindAnyObjectByType<GameManager>();
    }

    public void AddScore()
    {
        scores++;
        CheckEndGame();
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
