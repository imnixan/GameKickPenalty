using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    [SerializeField]
    private Image filler;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Application.targetFrameRate = 300;
        Sequence startGame = DOTween
            .Sequence()
            .Append(filler.DOFillAmount(1, 3.5f))
            .AppendCallback(() =>
            {
                SceneManager.LoadScene("Menu");
            });
        startGame.Restart();
    }
}
