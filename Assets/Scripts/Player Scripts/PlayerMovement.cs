using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3.5f;
    [SerializeField]
    private float minbound_X = -71f, maxbound_X = 71f, minbound_Y = -3.3f, maxbound_Y = 0f;
    private Vector3 tempPos;
    private float xAxis, yAxis;
    private PlayerAnimation playerAnimation;
    [SerializeField]
    private float shootWaitTime = 0.5f;
    private float waitBeforeShooting;
    [SerializeField]
    private float moveWaitTime = 0.3f;
    private float waitBeforeMoving;
    private bool canMove = true;
    private PlayerShootingManager playerShootingManager;
    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        playerShootingManager = GetComponent<PlayerShootingManager>();
    }
    // Update is called once per frame
    void Update()
    { 
        HandleMovement();
        HandleAnimation();
        HandleFacingDirection();
        HandleShooting();
        CheckIfCanMove();
    }
    void HandleMovement()
    {
        xAxis = Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS);
        yAxis = Input.GetAxisRaw(TagManager.VERTICAL_AXIS);

        if (!canMove)
            return;
        tempPos = transform.position;

        tempPos.x += xAxis * moveSpeed * Time.deltaTime;
        tempPos.y += yAxis * moveSpeed * Time.deltaTime;
        if (tempPos.x < minbound_X)
            tempPos.x = minbound_X;
        if (tempPos.x > maxbound_X)
            tempPos.x = maxbound_X;
        if (tempPos.y < minbound_Y)
            tempPos.y = minbound_Y;
        if (tempPos.y > maxbound_Y)
            tempPos.y = maxbound_Y;
        transform.position = tempPos;
    }
    void HandleAnimation()
    {
        if (!canMove)
            return;
        Debug.Log("xAxis: " + xAxis + " yAxis: " + yAxis);
        if (Mathf.Abs(xAxis) > 0 || Mathf.Abs(yAxis) > 0)
        {
            Debug.Log("Playing walk animation: " + TagManager.WALK_ANIMATION_NAME);
            playerAnimation.PlayAnimation(TagManager.WALK_ANIMATION_NAME);
        }
        else
        {
            Debug.Log("Playing idle animation: " + TagManager.IDLE_ANIMATION_NAME);
            playerAnimation.PlayAnimation(TagManager.IDLE_ANIMATION_NAME);
        } 
    }
    void HandleFacingDirection()
    {
        if (xAxis > 0)
            playerAnimation.SetFacingDirection(true);
        else if (xAxis < 0)
            playerAnimation.SetFacingDirection(false);
    }
    void StopMovement()
    {
        canMove = false;
        waitBeforeMoving = Time.time + moveWaitTime;
    }
    void Shoot()
    {
        waitBeforeShooting = Time.time + shootWaitTime;
        StopMovement();
        playerAnimation.PlayAnimation(TagManager.SHOOT_ANIMATION_NAME);
        playerShootingManager.Shoot(transform.localScale.x);
    }
    void CheckIfCanMove()
    {
        if (Time.time > waitBeforeMoving)
            canMove = true;
    }
    void HandleShooting()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (Time.time > waitBeforeShooting)
                Shoot();
        }

    }

}
