using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    private Image[] hpIcon;

    [SerializeField]
    private Sprite nohp;

    [SerializeField]
    private GameManager gm;

    private int hpLeft = 3;

    private void Start()
    {
        hpIcon = GetComponentsInChildren<Image>();
    }

    public void RestoreHp()
    {
        if (PlayerPrefs.GetInt("Vibro", 1) == 1)
        {
            Handheld.Vibrate();
        }
        if (hpLeft >= 0)
        {
            hpLeft--;
            hpIcon[hpLeft].sprite = nohp;
            if (hpLeft == 0)
            {
                gm.GameEnd();
            }
        }
    }
}
