using System;
using UnityEngine;

public class EnemyLogic : MonoBehaviour {
    [SerializeField] private int MaxDistFromSpawn;
    [SerializeField] private float MaxDetectionDist;
    [SerializeField] private float allowedDistError;
    [SerializeField] private int MaxWaitBetweenMoves;
    [SerializeField] private int MinWaitBetweenMoves;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpCooldown;
    private Rigidbody2D rb;
    private System.Random rnd;
    private Vector2 spawnPosition;
    private Vector2 initialPosition;
    private Vector2 finalPosition;
    private float lastTimeMoved;
    private bool isMoving;
    private float waitBetweenMoves;
    // private PlayerLogic player;
    private void OnEnable() {
        rb = GetComponent<Rigidbody2D>();
        rnd = new System.Random();
        spawnPosition = new Vector2(this.transform.position.x, this.transform.position.y);
        initialPosition = new Vector2(this.transform.position.x, this.transform.position.y);
        finalPosition = new Vector2(this.transform.position.x, this.transform.position.y);
        waitBetweenMoves = getWaitBetweenMoves();
        lastTimeMoved=-1*waitBetweenMoves; // to start moving on enable directly
    }
    private void Update() {

        // if(Mathf.Abs(this.transform.position.x-player.transform.position.x)<MaxDetectionDist
        //    && (this.transform.position.y-player.transform.position.x)<MaxDetectionHeight) {
        //     // go in hostile mode
        // }

        if(this.transform.position.x<=finalPosition.x-allowedDistError || this.transform.position.x>=finalPosition.x+allowedDistError) {
            moveToDestination();
        } else {
            isMoving=false;
            // after reaching destination, it has to wait till next movement
            if(Time.time-lastTimeMoved>waitBetweenMoves) {
                float totalMoveDist = getMoveDistance();
                finalPosition.x += totalMoveDist;
                waitBetweenMoves = getWaitBetweenMoves();
            } // else dont move, just wait
        }
    }

    private float getMoveDistance() {
        initialPosition = finalPosition;
        // max range of movement
        float xMax = MaxDistFromSpawn - (float)(initialPosition.x-spawnPosition.x);
        float xMin = -1*(MaxDistFromSpawn - (float)(spawnPosition.x-initialPosition.x));

        // random distance integer part
        int randomDistanceInteger = rnd.Next((int)xMin, (int)xMax);

        // random distance float part
        float randomDistanceDecimal = (float)rnd.NextDouble();
        float maxDecimalAllowed = Math.Min((xMax-(float)Mathf.Floor(xMax)), (xMin-(float)Mathf.Floor(xMin)));
        randomDistanceDecimal = randomDistanceDecimal*-1 +maxDecimalAllowed;
        //Debug.Log(xMax-Math.Floor(xMax));
        
        // random sign for int + or - float
        float randomSign = (float)rnd.Next(-1, 2);
        randomDistanceDecimal*=randomSign;

        // random finalDistance +ve or -ve to move
        float finalDistance = (float)randomDistanceInteger+(float)randomDistanceDecimal;
        return finalDistance;
    }
    private void moveToDestination() {
        // it has not reached destination
        isMoving=true;
        // get move direction
        Vector3 moveDir = new Vector3(1, 0, 0);
        if(finalPosition.x-initialPosition.x<0) moveDir*=-1;
        // get move distance
        float moveDist = moveSpeed*Time.deltaTime;
        this.transform.position+=moveDir*moveDist;
        lastTimeMoved = Time.time;
    }

    private float getWaitBetweenMoves() {
        float currWait;
        currWait = rnd.Next(MinWaitBetweenMoves, MaxWaitBetweenMoves-1);
        currWait += (float)rnd.NextDouble();
        return currWait;
    }

    public bool getIsMoving() {return isMoving;}
}
