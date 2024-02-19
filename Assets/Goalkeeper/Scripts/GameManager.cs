using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Kicker kicker;

    private UIManager uiManager;

    private string nextScene;

    private void Start()
    {
        uiManager = GetComponent<UIManager>();
    }

    public void GameEnd()
    {
        kicker.EndGame();
        uiManager.ShowEndGame();
    }

    public void RestartGame()
    {
        nextScene = SceneManager.GetActiveScene().name;
        uiManager.ReloadAnim();
    }

    public void QuitGame()
    {
        nextScene = "QUIT";
        uiManager.ReloadAnim();
    }

    public void Menu()
    {
        nextScene = "Menu";
        uiManager.ReloadAnim();
    }

    public void LoadNextScene()
    {
        if (nextScene == "QUIT")
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(nextScene);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Menu();
        }
    }
}
