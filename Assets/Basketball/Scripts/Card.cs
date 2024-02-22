using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class Card : MonoBehaviour, IPointerClickHandler
{
    private int cardId;
    private Material cardMat;

    [SerializeField]
    private Image suit;

    public bool active;
    private CardGameManager cgm;

    private void Start()
    {
        cardMat = new Material(suit.material);
        suit.material = cardMat;
        cardMat.SetFloat("_DissolveAmount", -1);
        cgm = GetComponentInParent<CardGameManager>();
    }

    public void SetCard(Sprite card)
    {
        GetComponent<Image>().sprite = card;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (active)
        {
            ShowCard();
            active = false;
            cgm.CardOpened(cardId);
        }
    }

    public void ShowCard()
    {
        DOTween.To(
            () => cardMat.GetFloat("_DissolveAmount"),
            x => cardMat.SetFloat("_DissolveAmount", x),
            -1,
            2.0f
        );
    }

    public void HideCard(int cardId)
    {
        this.cardId = cardId;
        DOTween.To(
            () => cardMat.GetFloat("_DissolveAmount"),
            x => cardMat.SetFloat("_DissolveAmount", x),
            1,
            2.0f
        );
    }
}
