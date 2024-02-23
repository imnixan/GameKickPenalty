using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class FindGameManager : MonoBehaviour
{
    [SerializeField]
    private Sprite star,
        goldStar,
        trophy;

    private Element[] elementsArray;
    private List<Element> elements = new List<Element>();
    private Timer timer;

    [SerializeField]
    private TextMeshProUGUI scoreCounter,
        scoreEnd,
        timerEnd,
        timerCounter,
        result;

    [SerializeField]
    private RectTransform top,
        endMenu,
        buttons;

    [SerializeField]
    private Image[] hearts;

    private int scores;
    private int hp = 3;

    private void Start()
    {
        timer = FindAnyObjectByType<Timer>();
        elementsArray = GetComponentsInChildren<Element>();
        elements.AddRange(elementsArray);
        for (int i = 0; i < 4; i++)
        {
            int elementId = Random.Range(0, elements.Count);
            elements[elementId].ChangeType(Element.Type.Star, star);
            elements.RemoveAt(elementId);
        }

        int goldStarCount = Random.Range(4, 7);
        for (int i = 0; i < goldStarCount; i++)
        {
            int elementId = Random.Range(0, elements.Count);
            elements[elementId].ChangeType(Element.Type.GoldStar, goldStar);
            elements.RemoveAt(elementId);
        }
        elements[Random.Range(0, elements.Count)].ChangeType(Element.Type.Trophy, trophy);
        elements.Clear();
        scores = PlayerPrefs.GetInt("score");
        scoreCounter.text = $"scores:\n{scores}";
    }

    public void ElementClicked(Element.Type type)
    {
        switch (type)
        {
            case Element.Type.Bomb:
                DecraseHp();
                Debug.Log("boom");
                break;
            case Element.Type.Star:
                scores -= 10;
                if (scores < 0)
                {
                    scores = 0;
                }
                scoreCounter.text = $"scores:\n{scores}";
                Debug.Log("Star");
                break;
            case Element.Type.GoldStar:
                scores += 10;
                scoreCounter.text = $"scores:\n{scores}";
                Debug.Log("GoldStar");
                break;
            case Element.Type.Trophy:
                Debug.Log("Trophy");
                WinGame();
                break;
        }
    }

    private void Update()
    {
        timerCounter.text = $"time:\n{timer.FormatTime(timer.GetTime())}";
    }

    private void WinGame()
    {
        result.text = "You are win!";
        EndGame();
        scoreEnd.text = scores.ToString();
        timerEnd.text = timer.FormatTime(timer.GetTime());
        PlayerPrefs.SetInt("score", scores);
        PlayerPrefs.SetInt("time", timer.GetTime());
        PlayerPrefs.Save();
    }

    private void EndGame()
    {
        timer.StopTimer();

        top.DOAnchorPosY(2000, 0.5f);
        endMenu.DOAnchorPosX(0, 0.5f);
        buttons.DOAnchorPosY(0, 0.5f);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void LoseGame()
    {
        Debug.Log("Lose");
        result.text = "Game over!";
        EndGame();
        scoreEnd.text = 0.ToString();
        timerEnd.text = timer.FormatTime(timer.GetTime());
        PlayerPrefs.SetInt("score", 0);
        PlayerPrefs.SetInt("time", 0);
        PlayerPrefs.Save();
    }

    private void DecraseHp()
    {
        hp--;
        hearts[hp].DOColor(Color.white / 2, 0.5f);
        if (hp == 0)
        {
            LoseGame();
        }
    }
}
