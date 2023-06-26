using System;
using UnityEngine;

public class EnemyLogic : MonoBehaviour {
    [SerializeField] private int MaxDistFromSpawn;
    [SerializeField] private float allowedDistError;
    [SerializeField] private float WaitBetweenMoves;
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rb;
    private System.Random rnd;
    private Vector2 spawnPosition;
    private Vector2 initialPosition;
    private Vector2 finalPosition;
    private float lastTimeMoved;
    private bool isMoving;
    private void OnEnable() {
        rb = GetComponent<Rigidbody2D>();
        rnd = new System.Random();
        spawnPosition = new Vector2(this.transform.position.x, this.transform.position.y);
        initialPosition = new Vector2(this.transform.position.x, this.transform.position.y);
        finalPosition = new Vector2(this.transform.position.x, this.transform.position.y);
        lastTimeMoved=Time.time;
    }
    private void Update() {
        if(this.transform.position.x<=finalPosition.x-allowedDistError || this.transform.position.x>=finalPosition.x+allowedDistError) {
            isMoving=true;
            Vector3 moveDir = new Vector3(1, 0, 0);
            float moveDist = moveSpeed*Time.deltaTime;
            if(finalPosition.x-initialPosition.x<0) moveDir*=-1;
            this.transform.position+=moveDir*moveDist;
            lastTimeMoved = Time.time;
        } else {
            isMoving=false;
            if(Time.time-lastTimeMoved>WaitBetweenMoves) {
                float totalMoveDist = getMoveDistance();
                finalPosition.x += totalMoveDist;
            }
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
        float randomDistanceDouble = (float)rnd.NextDouble();
        float maxDoubleAllowed = Math.Min((xMax-(int)xMax), xMin-(int)xMin);
        if(randomDistanceDouble>maxDoubleAllowed) randomDistanceDouble=maxDoubleAllowed;

        // random sign for int + or - float

        // random finalDistance +ve or -ve to move
        float finalDistance = (float)randomDistanceInteger+randomDistanceDouble;
        return finalDistance;
    }

    public bool getIsMoving() { return isMoving;}
}
