using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Kicker kicker;

    [SerializeField]
    private GameObject target;

    private UIManager uiManager;

    private string nextScene;

    private void Start()
    {
        uiManager = GetComponent<UIManager>();
    }

    public void GameEnd(int playerScore, int loseScore)
    {
        kicker.EndGame();
        uiManager.ShowEndGame(playerScore, loseScore);
        target.SetActive(false);
    }

    public void RestartGame()
    {
        nextScene = SceneManager.GetActiveScene().name;
        LoadNextScene();
    }

    public void QuitGame()
    {
        nextScene = "QUIT";
    }

    public void Menu()
    {
        nextScene = "Menu";
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
