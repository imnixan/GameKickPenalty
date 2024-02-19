using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoresManager : MonoBehaviour
{
    [SerializeField]
    private Sprite[] digits;

    [SerializeField]
    private Image[] InGameWindows,
        EndGameWindows,
        RecordWindows;

    private int scores;
    private int bestRecord;

    private void Start()
    {
        bestRecord = PlayerPrefs.GetInt("Best");
        UpdateScoresOnTable();
    }

    public void AddScore()
    {
        scores++;
        if (scores > bestRecord)
        {
            bestRecord = scores;
            PlayerPrefs.SetInt("Best", bestRecord);
            PlayerPrefs.Save();
        }
        UpdateScoresOnTable();
    }

    private void UpdateScoresOnTable()
    {
        if (scores > 99)
        {
            InGameWindows[0].sprite = digits[scores / 100];
            InGameWindows[1].sprite = digits[(scores / 10) % 10];
            InGameWindows[2].sprite = digits[scores % 10];

            EndGameWindows[0].sprite = digits[scores / 100];
            EndGameWindows[1].sprite = digits[(scores / 10) % 10];
            EndGameWindows[2].sprite = digits[scores % 10];

            RecordWindows[0].sprite = digits[bestRecord / 100];
            RecordWindows[1].sprite = digits[(bestRecord / 10) % 10];
            RecordWindows[2].sprite = digits[bestRecord % 10];
        }
        else if (scores > 9)
        {
            InGameWindows[1].sprite = digits[scores / 10];
            InGameWindows[2].sprite = digits[scores % 10];

            EndGameWindows[1].sprite = digits[scores / 10];
            EndGameWindows[2].sprite = digits[scores % 10];

            RecordWindows[1].sprite = digits[bestRecord / 10];
            RecordWindows[2].sprite = digits[bestRecord % 10];
        }
        else
        {
            InGameWindows[2].sprite = digits[scores];

            EndGameWindows[2].sprite = digits[scores];

            RecordWindows[2].sprite = digits[bestRecord];
        }
    }
}
