using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedZone : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Level level;
    Rigidbody2D rb2D;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if((col.gameObject.tag == "Player") && (player.invincible == false))
        {
            player.Die();
        }
    }
}
