using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemWorld : MonoBehaviour
{
    public static ItemWorld SpawnItemWorld(Item item, Vector3 position)
    {
        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    public static ItemWorld DropItem(Vector3 dropPosition, Item item)
    {
        Vector2 dropPos = new Vector2(dropPosition.x + 1, dropPosition.y);
        ItemWorld itemWorld = SpawnItemWorld(item, dropPos);
        return itemWorld;
    }

    private Item item;
    private SpriteRenderer sr;
    private TextMeshPro tmp;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        tmp = transform.Find("AmountText").GetComponent<TextMeshPro>();
    }

    public void SetItem(Item item)
    {
        this.item = item;
        sr.sprite = item.GetSprite();
        if (item.amount > 1)
        {
            tmp.SetText(item.amount.ToString());
        }
        else
            tmp.SetText("");
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
