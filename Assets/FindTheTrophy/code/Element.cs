using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Element : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Sprite elementSprite;
    private Image image;
    private FindGameManager fgm;
    private bool opened;

    public enum Type
    {
        Star,
        Bomb,
        GoldStar,
        Trophy
    }

    private Type elementType = Type.Bomb;

    public void ChangeType(Type type, Sprite sprite)
    {
        this.elementType = type;
        this.elementSprite = sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!opened)
        {
            OpenCard();
        }
    }

    public void OpenCard()
    {
        image = GetComponent<Image>();
        fgm = FindAnyObjectByType<FindGameManager>();
        opened = true;
        fgm.ElementClicked(elementType);
        image.sprite = elementSprite;
        image.SetNativeSize();
    }
}
