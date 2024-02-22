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
    private int time;
    private bool win;

    public void AddCard()
    {
        cardsRow++;
        row.text = cardsRow.ToString();
    }

    private void Start()
    {
        cardsRow = PlayerPrefs.GetInt("row");
        time = PlayerPrefs.GetInt("time");
        row.text = cardsRow.ToString();
        timer.text = FormatTime(time);
        StartCoroutine(TimerCounter());
    }

    IEnumerator TimerCounter()
    {
        WaitForSeconds second = new WaitForSeconds(1);
        while (true)
        {
            yield return second;
            time++;
            timer.text = FormatTime(time);
        }
    }

    public void EndGame(bool win)
    {
        this.win = win;
        header.text = "Result";
        results.text = win ? "you win!" : "game over!";
        cards.DOAnchorPosX(-2000, 0.5f);
        endWindow.DOAnchorPosY(0, 0.5f);
        endButtons.DOAnchorPosY(50, 0.5f);
        StopAllCoroutines();
        timerEnd.text = FormatTime(time);
        rowEnd.text = $"{cardsRow} cards";
        if (win)
        {
            PlayerPrefs.SetInt("time", time);
            PlayerPrefs.SetInt("row", cardsRow);
            PlayerPrefs.Save();
        }
    }

    private string FormatTime(int seconds)
    {
        int minutes = seconds / 60;
        int remainingSeconds = seconds % 60;
        return string.Format("{0:00}:{1:00}", minutes, remainingSeconds);
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
