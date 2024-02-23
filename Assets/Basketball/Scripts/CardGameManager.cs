using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class CardGameManager : MonoBehaviour
{
    [SerializeField]
    private Sprite[] cardFaces;

    private Card[] cards;
    private BasketGameManager bgm;

    private void Start()
    {
        bgm = FindAnyObjectByType<BasketGameManager>();
        cards = GetComponentsInChildren<Card>();
        cards.Shuffle();
        cardFaces.Shuffle();
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].SetCard(cardFaces[i]);
        }
    }

    [SerializeField]
    private RectTransform playBtn;

    public void PlayGame()
    {
        StartCoroutine(StartGame());
        playBtn.DOAnchorPosY(-1000, 0.5f);
    }

    private int cardCount;

    [SerializeField]
    private AudioClip open;

    public void CardOpened(int cardId)
    {
        AudioSource.PlayClipAtPoint(open, Vector2.zero);
        if (cardId == cardCount)
        {
            cardCount++;
            bgm.AddCard();
            if (cardCount == 4)
            {
                bgm.EndGame(true);
            }
        }
        else
        {
            bgm.EndGame(false);
        }
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].HideCard(i);
            yield return new WaitForSeconds(.75f);
        }
        foreach (var card in cards)
        {
            card.active = true;
        }
    }
}

public static class ArrayShuffler
{
    public static void Shuffle<T>(this T[] array)
    {
        System.Random rng = new System.Random();
        int n = array.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
}
