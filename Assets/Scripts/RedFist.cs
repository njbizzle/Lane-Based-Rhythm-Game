using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFist : MonoBehaviour
{
    //GameObject Refrences
    [SerializeField] Level level;
    [SerializeField] Player player;

    //Components
    SpriteRenderer spriteRenderer;
    CircleCollider2D circleCollider2D;

    [SerializeField] float rFistScreenTime = 0.5f;
    [SerializeField] float rFistScreenTimeBreaking = 0.25f;
    [SerializeField] float rFistDelayTime = 0.1f;
    [SerializeField] Vector3 rPlayerPosOffset;

    Vector2 rMoveOffsetVector;
    Vector2 rChangeLanesFist;
    Vector2 rFistXPos;
    float rPlayerMoveSpeed;
    float step = 0;
    bool rNotBreaking = true;

    void Start()
    {
        level = FindObjectOfType<Level>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        player = FindObjectOfType<Player>();
        rPlayerPosOffset = transform.position;
        rPlayerMoveSpeed = player.playerMoveSpeed;
        rFistXPos = Vector2.zero;

        MoveRedFistToPlayer();
        transform.position = new Vector3(-10,player.laneCoords[player.startingLane],0) + rPlayerPosOffset;

        spriteRenderer.enabled = false;
        circleCollider2D.enabled = false;
    }

    void Update()
    {
        if (rChangeLanesFist != Vector2.zero)
        {
            float step = rPlayerMoveSpeed*Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position,rChangeLanesFist,step);
            if ((Vector2)transform.position == rChangeLanesFist)
            {
                step = 0;
            }
        }
        else
        {
            transform.position = (Vector2)player.transform.position + (Vector2)rPlayerPosOffset + rMoveOffsetVector;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            StopPunchRed();
            StartCoroutine(ExtendRedFist());
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        StopPunchRed();
    }

    public void MoveRedFistToPlayer()
    {
        if (rNotBreaking)
        {
            rChangeLanesFist = Vector2.zero;
            transform.position = player.GetPlayerPos()+ rPlayerPosOffset;
        }
    }

    IEnumerator ExtendRedFist()
    {
        yield return new WaitForSeconds(rFistDelayTime);
        spriteRenderer.enabled = true;
        circleCollider2D.enabled = true;
        yield return new WaitForSeconds(rFistScreenTime);
        if (rNotBreaking)
        {
            spriteRenderer.enabled = false;
            circleCollider2D.enabled = false;
        }
    }
    public void StopPunchRed()
    {
        spriteRenderer.enabled = false;
        circleCollider2D.enabled = false;
        StopCoroutine(ExtendRedFist());
        StopCoroutine(RBreakUpFistCR());
        MoveRedFistToPlayer();
        rNotBreaking = true;
    }
    public void RBreakUpMoveFist(Vector2 target)
    {
        if(rNotBreaking)
        {
            rChangeLanesFist = target;
            rNotBreaking = false;
            StopCoroutine(ExtendRedFist());
            StartCoroutine(RBreakUpFistCR());
        }
    }
    IEnumerator RBreakUpFistCR()
    {
        spriteRenderer.enabled = true;
        circleCollider2D.enabled = true;
        yield return new WaitForSeconds(0.5f);
        MoveRedFistToPlayer();
        rNotBreaking = true;
    }
}
