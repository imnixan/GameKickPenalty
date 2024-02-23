using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class BasketGameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timer,
        timerEnd,
        row,
        rowEnd,
        results,
        header;

    [SerializeField]
    private RectTransform cards,
        endWindow,
        endButtons;

    private int cardsRow;
    private bool win;
    private Timer time;

    public void AddCard()
    {
        cardsRow++;
        row.text = cardsRow.ToString();
    }

    private void Start()
    {
        cardsRow = PlayerPrefs.GetInt("row");
        time = FindAnyObjectByType<Timer>();
        row.text = cardsRow.ToString();
        timer.text = time.FormatTime(time.GetTime());
        time.StartTime();
    }

    private void Update()
    {
        timer.text = time.FormatTime(time.GetTime());
    }

    public void EndGame(bool win)
    {
        this.win = win;
        header.text = "Result";
        results.text = win ? "you win!" : "game over!";
        cards.DOAnchorPosX(-2000, 0.5f);
        endWindow.DOAnchorPosY(0, 0.5f);
        endButtons.DOAnchorPosY(50, 0.5f);
        time.StopTimer();
        timerEnd.text = time.FormatTime(time.GetTime());
        rowEnd.text = $"{cardsRow} cards";
        if (win)
        {
            PlayerPrefs.SetInt("time", time.GetTime());
            PlayerPrefs.SetInt("row", cardsRow);
            PlayerPrefs.Save();
        }
    }

    public void Restart()
    {
        if (!win)
        {
            PlayerPrefs.SetInt("time", 0);
            PlayerPrefs.SetInt("row", 0);
            PlayerPrefs.Save();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
