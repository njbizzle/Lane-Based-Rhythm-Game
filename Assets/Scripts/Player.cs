using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //GameObject Refrences
    [SerializeField] RedFist redFist;
    [SerializeField] BlueFist blueFist;
    [SerializeField] RedZone redZone;
    [SerializeField] Level level;

    //Components
    SpriteRenderer spriteRenderer;

    [SerializeField] public int playerMoveSpeed;
    [SerializeField] public int[] laneCoords;
    [SerializeField] public int startingLane;
    [SerializeField] int spaceBetweenLanes;
    [SerializeField] int currentLane;
    
    public bool invincible = false;
    public bool isMoving = false;
    int laneAmount;
    Vector2 playerXPos;
    Vector2 changeLanes = new Vector2(0,0);
    Vector2 playerMoveOffsetVector;

    void Start()
    {
        playerMoveOffsetVector = new Vector3(-10,laneCoords[startingLane],0);

        redZone = FindObjectOfType<RedZone>();
        redFist = FindObjectOfType<RedFist>();
        blueFist = FindObjectOfType<BlueFist>();
        level = FindObjectOfType<Level>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentLane = startingLane;
        playerXPos = Vector2.zero;
        laneAmount = laneCoords.Length;
        spaceBetweenLanes = laneCoords[0]-laneCoords[1];
    }

    void Update()
    {
        playerXPos.x = level.playerMovementX+playerMoveOffsetVector.x;
        playerXPos.y = playerMoveOffsetVector.y;
        transform.position = playerXPos;
        // if(playerMoveOffsetVector != Vector2.zero)
        // {
        //     playerMoveOffsetVector = Vector2.zero;
        // }

        if (changeLanes != Vector2.zero)
        {
            float step = playerMoveSpeed*Time.deltaTime;
            playerMoveOffsetVector = Vector2.MoveTowards(transform.position,changeLanes,step);
            if ((Vector2)playerMoveOffsetVector == changeLanes)
            {
                isMoving = false;
                changeLanes = Vector2.zero;
                //redFist.MoveRedFistToPlayer();
                blueFist.MoveBlueFistToPlayer();
            }
        }
        if(isMoving == false)
        {
            if ((Input.GetKeyDown(KeyCode.LeftArrow)) && (Input.GetKey(KeyCode.Z)))
            {
                if(currentLane != 0)
                {
                    MoveLanes(true,5);
                    currentLane-=1;
                    redFist.RBreakUpMoveFist((Vector2)playerMoveOffsetVector + new Vector2(0,7.5f));
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if(currentLane != 0)
                {
                    MoveLanes(false,5);
                    currentLane-=1;
                }
            }

            if ((Input.GetKeyDown(KeyCode.RightArrow)) && (Input.GetKey(KeyCode.X)))
            {
                if(currentLane != laneAmount-1){
                    MoveLanes(true,-5);
                    currentLane+=1;
                    blueFist.BBreakUpMoveFist((Vector2)playerMoveOffsetVector + new Vector2(0,-7.5f));
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if(currentLane != laneAmount-1){
                    MoveLanes(false,-5);
                    currentLane+=1;
                }
            }
        }
    }

    public void MoveLanes(bool breaking, int num)
    {
        isMoving = true;
        changeLanes = playerMoveOffsetVector + new Vector2(0,num);
        if (breaking == false)
        {
            redFist.MoveRedFistToPlayer();
            blueFist.MoveBlueFistToPlayer();
            redFist.StopPunchRed();
            blueFist.StopPunchBlue();
        }
    }
    public void Die()
    {
        //reset player pos
    }
    public Vector3 GetPlayerPos()
    {
        if (changeLanes == Vector2.zero)
        {
            return playerMoveOffsetVector;
        }
        else
        {
            return changeLanes;
        }
    }
}