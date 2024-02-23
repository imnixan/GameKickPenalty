using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class InfoManager : MonoBehaviour
{
    [SerializeField]
    private InfoElement[] modules;

    [SerializeField]
    private RectTransform infoWin,
        infoDataWin;

    [SerializeField]
    private TextMeshProUGUI infoText,
        header;

    [SerializeField]
    private Image[] btns,
        filters;

    [SerializeField]
    private Image infoImage;
    private Vector2[] positions;

    private MainMenuManager mmm;

    [SerializeField]
    private Color choosedCOlor;

    private void Start()
    {
        mmm = FindAnyObjectByType<MainMenuManager>();
        positions = new Vector2[btns.Length];
        for (int i = 0; i < btns.Length; i++)
        {
            positions[i] = btns[i].rectTransform.anchoredPosition;
        }
    }

    public void ShowInfo()
    {
        mmm.HideMenu();
        infoWin.DOAnchorPosX(0, 0.5f);
    }

    public void HideInfo()
    {
        infoWin.DOAnchorPosX(2000, 0.5f);
        mmm.ShowMenu();
    }

    public void HideData()
    {
        infoDataWin.DOAnchorPosX(2000, 0.5f);
    }

    public void OpenData(int data)
    {
        infoDataWin.DOAnchorPosX(0, 0.5f);
        header.text = modules[data].header;
        infoText.text = modules[data].infoText;
        infoImage.sprite = modules[data].infoSprite;
    }

    public void SetFilter(int filter)
    {
        for (int i = 0; i < btns.Length; i++)
        {
            int pos = i + filter;
            if (pos >= btns.Length)
            {
                pos = pos - btns.Length;
            }
            btns[i].rectTransform.DOAnchorPos(positions[pos], 1.5f);
        }
        foreach (var btn in filters)
        {
            btn.color = Color.white;
        }
        filters[filter].color = choosedCOlor;
    }
}
