using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCoinItem : MonoBehaviour
{
    public CoinObject[] coins;
    public Vector2 randomThrowDir; // TODO: at start (when instantiated) throw coin in a random direction a little bit
    public int throwSpeed;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Player player;

    private CoinObject coin;



    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void Start()
    {
        var xRandomDir = Random.Range(0, randomThrowDir.x);
        var yRandomDir = Random.Range(0, randomThrowDir.y);
        rb.velocity = new Vector2(xRandomDir * throwSpeed, yRandomDir * throwSpeed);
        coin = SelectRandomCoin();
        sr.sprite = coin.uiDisplay;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            // Grant player random coin value
            player.playerData.moneyAmount += coin.coinValue;
            Destroy(this.gameObject);
        }
    }


    public CoinObject SelectRandomCoin()
    {
        var randomCoin = coins[Random.Range(0, coins.Length)];
        return randomCoin;
    }
}
