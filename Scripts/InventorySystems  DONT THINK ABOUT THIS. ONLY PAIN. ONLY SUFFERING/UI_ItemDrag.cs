using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_ItemDrag : MonoBehaviour
{
    public static UI_ItemDrag instance { get; private set; }

    private Canvas canvas;
    private RectTransform rt;
    private RectTransform parentRt;
    private CanvasGroup cg;
    private Image image;
    private Item item;

    private void Awake()
    {
        instance = this;

        rt = GetComponent<RectTransform>();
        cg = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        image = transform.Find("Image").GetComponent<Image>();
        parentRt = transform.GetComponent<RectTransform>();

        Hide();
    }

    private void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRt, Input.mousePosition, null, out Vector2 localPoint);
        transform.localPosition = localPoint;
    }

    public Item GetItem()
    {
        return item;
    }

    public void SetItem(Item item)
    {
        this.item = item;
    }

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show(Item item)
    {
        gameObject.SetActive(true);

        SetItem(item);
        SetSprite(item.GetSprite());
        UpdatePosition();
    }
}
