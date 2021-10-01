using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCoinItem : MonoBehaviour
{
    private SpriteRenderer sr;
    public Vector2 randomThroDir;
    public int coinValue;
    public List<Sprite> sprites = new List<Sprite>();

    // TODO: REWORK : List of coin item scriptable objects, those objects have a value and a sprite, then select random 1 from that list and get its sprite and value

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        sr.sprite = SelectRandomSprite();
    }

    public Sprite SelectRandomSprite()
    {
        Sprite randomSprite = sprites[Random.Range(0, sprites.Count)];
        return randomSprite;
    }
}
