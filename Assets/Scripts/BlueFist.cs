using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueFist : MonoBehaviour
{
    //GameObject Refrences
    [SerializeField] Level level;
    [SerializeField] Player player;

    //Components
    SpriteRenderer spriteRenderer;
    CircleCollider2D circleCollider2D;

    [SerializeField] float bFistScreenTime = 0.5f;
    [SerializeField] float bFistScreenTimeBreaking = 0.25f;
    [SerializeField] float bFistDelayTime = 0.1f;
    [SerializeField] Vector3 bPlayerPosOffset;

    Vector2 bMoveOffsetVector;
    Vector2 bChangeLanesFist;
    Vector2 bFistXPos;
    float bPlayerMoveSpeed;
    float step = 0;
    bool bNotBreaking = true;

    void Start()
    {
        level = FindObjectOfType<Level>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        player = FindObjectOfType<Player>();
        bPlayerPosOffset = transform.position;
        bPlayerMoveSpeed = player.playerMoveSpeed;
        bFistXPos = Vector2.zero;

        MoveBlueFistToPlayer();
        transform.position = new Vector3(-10,player.laneCoords[player.startingLane],0) + bPlayerPosOffset;

        spriteRenderer.enabled = false;
        circleCollider2D.enabled = false;
    }

    void Update()
    {
        if (bChangeLanesFist != Vector2.zero)
        {
            float step = bPlayerMoveSpeed*Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position,bChangeLanesFist,step);
            if ((Vector2)transform.position == bChangeLanesFist)
            {
                step = 0;
            }
        }
        else
        {
            transform.position = (Vector2)player.transform.position + (Vector2)bPlayerPosOffset + bMoveOffsetVector;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            StopPunchBlue();
            StartCoroutine(ExtendBlueFist());
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        StopPunchBlue();
    }

    public void MoveBlueFistToPlayer()
    {
        if (bNotBreaking)
        {
            bChangeLanesFist = Vector2.zero;
            transform.position = player.GetPlayerPos()+ bPlayerPosOffset;
        }
    }

    IEnumerator ExtendBlueFist()
    {
        yield return new WaitForSeconds(bFistDelayTime);
        spriteRenderer.enabled = true;
        circleCollider2D.enabled = true;
        yield return new WaitForSeconds(bFistScreenTime);
        if (bNotBreaking)
        {
            spriteRenderer.enabled = false;
            circleCollider2D.enabled = false;
        }
    }
    public void StopPunchBlue()
    {
        spriteRenderer.enabled = false;
        circleCollider2D.enabled = false;
        StopCoroutine(ExtendBlueFist());
        StopCoroutine(BBreakUpFistCR());
        MoveBlueFistToPlayer();
        bNotBreaking = true;
    }
    public void BBreakUpMoveFist(Vector2 target)
    {
        if(bNotBreaking)
        {
            bChangeLanesFist = target;
            bNotBreaking = false;
            StopCoroutine(ExtendBlueFist());
            StartCoroutine(BBreakUpFistCR());
        }
    }
    IEnumerator BBreakUpFistCR()
    {
        spriteRenderer.enabled = true;
        circleCollider2D.enabled = true;
        yield return new WaitForSeconds(0.5f);
        MoveBlueFistToPlayer();
        bNotBreaking = true;
    }
}