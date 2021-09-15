using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    [SerializeField] private ContactFilter2D filter;
    private BoxCollider2D bc;
    private Collider2D[] hits = new Collider2D[10];
    protected PlatformGameManager gameManager;

    protected virtual void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<PlatformGameManager>();
    }

    protected virtual void Update()
    {
        bc.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            OnCollide(hits[i]);
            hits[i] = null;
        }
    }

    protected virtual void OnCollide(Collider2D coll)
    {
        Debug.Log("Collision not implemented yet for " + this.name);
    }
}
