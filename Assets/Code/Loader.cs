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
        Application.targetFrameRate = 300;
        Sequence startGame = DOTween.Sequence().Append(filler.DOFillAmount(1, 35f));
        startGame.Restart();
    }
}
