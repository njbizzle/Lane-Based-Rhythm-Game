using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public float playerMovementX;

    public float songLenSeconds;
    public float unitsPerSecond;
    public int levelLenUnits;

    public float startOffSetUnits;
    public float startOffSetSeconds;

    int startPos;
    int target;
    float t;

    void Start()
    {

        unitsPerSecond = 10;
        startOffSetUnits = 0;
        startOffSetSeconds = songLenSeconds*unitsPerSecond;
        startPos = 0;
        target = levelLenUnits;
        playerMovementX = 0;
    }

    void Update()
    {
        t += Time.deltaTime/songLenSeconds;
        //playerMovementX = Mathf.Lerp(startPos, target, t);
    }

    public float GetMoveSpeed()
    {
        return 1f;
    }
}
