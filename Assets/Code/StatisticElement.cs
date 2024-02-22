using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class StatisticElement : MonoBehaviour
{
    [SerializeField]
    private string league;

    [SerializeField]
    private TextMeshProUGUI win,
        lose;

    private void Start()
    {
        win.text = PlayerPrefs.GetInt(league + "player").ToString();
        lose.text = PlayerPrefs.GetInt(league + "lose").ToString();
    }
}
