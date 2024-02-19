using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField]
    private Image soundStatus,
        vibroStatus;

    [SerializeField]
    private Sprite on,
        off;

    private void Start()
    {
        SetStatuses();
    }

    public void Quit()
    {
        SceneManager.LoadScene("Menu");
    }

    private void SetStatuses()
    {
        soundStatus.sprite = PlayerPrefs.GetInt("Sound", 1) == 1 ? on : off;
        vibroStatus.sprite = PlayerPrefs.GetInt("Vibro", 1) == 1 ? on : off;
    }

    public void SwitchSettings(string settings)
    {
        PlayerPrefs.SetInt(settings, PlayerPrefs.GetInt(settings, 1) == 1 ? 0 : 1);
        PlayerPrefs.Save();
        SetStatuses();
    }
}
