using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBreakable : MonoBehaviour
{
    [SerializeField] Player player;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2D;

    void Start()
    {
        player = FindObjectOfType<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "RedFist")
        {
            Destroy(gameObject);
        }
        else
        {
            player.Die();
        }
    }
}
