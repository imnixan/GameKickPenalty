using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class CardGameManager : MonoBehaviour
{
    [SerializeField]
    private Sprite[] cardFaces;

    private Card[] cards;

    private void Start()
    {
        cards = GetComponentsInChildren<Card>();
        cards.Shuffle();
        cardFaces.Shuffle();
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].SetCard(cardFaces[i]);
        }
        StartCoroutine(StartGame());
    }

    private int cardCount;

    public void CardOpened(int cardId)
    {
        if (cardId == cardCount)
        {
            cardCount++;
            if (cardCount == 4)
            {
                Debug.Log("Win");
            }
        }
        else
        {
            Debug.Log("Lose");
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
