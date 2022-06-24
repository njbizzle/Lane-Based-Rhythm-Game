// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class MovingObjects : MonoBehaviour
// {
//     [SerializeField] Level level;
//     SpriteRenderer spriteRenderer;
//     BoxCollider2D boxCollider2D;
//     Rigidbody2D rb2D;
//     Vector2 moveVector;
//     Vector2 startPos;
//     Vector2 direction;
//     Vector2 target;
//     float speed;
//     float t;

//     float lSongLenSeconds;
//     float lUnitsPerSecond;
//     float lLevelLenUnits;
//     float lStartOffSetUnits;
//     float lStartOffSetSeconds;

//     void Start()
//     {

//         level = FindObjectOfType<Level>();
//         rb2D = GetComponent<Rigidbody2D>();
//         if(gameObject.tag == "Breakable")
//         {
//             spriteRenderer = GetComponent<SpriteRenderer>();
//             boxCollider2D = GetComponent<BoxCollider2D>();    
//         }


//         lSongLenSeconds = level.songLenSeconds;
//         lUnitsPerSecond = level.unitsPerSecond;
//         lStartOffSetSeconds = lSongLenSeconds*lUnitsPerSecond;
//         lStartOffSetUnits = level.startOffSetUnits;


//         startPos = transform.position;
//         target = new Vector2(transform.position.x - lLevelLenUnits,transform.position.y);
//         Debug.Log(target);
//     }

//     void Update()
//     {
//         //rb2D.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
//         t += Time.deltaTime/lSongLenSeconds;
//         transform.position = Vector3.Lerp(startPos, target, t);
//     }
//     public void ResetMovingObjects()
//     {
//         transform.position = startPos;
//         if(gameObject.tag == "Breakable")
//         {
//             spriteRenderer.enabled = true;
//             boxCollider2D.enabled = true;
//         }
//     }
// }