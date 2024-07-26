using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTarget;
    [SerializeField]
    private float smoothSpeed = 2f;
    [SerializeField]
    private float player_BoundMin_Y = -1f, player_BoundMin_X = -65f, player_BoundMax_X = 65f;
    [SerializeField]
    private float Y_Gap = 2f;
    private Vector3 tempPos;
    private void Start()
    {
        playerTarget = GameObject.FindWithTag(TagManager.PLAYER_TAG).transform;

    }
    private void Update()
    {
        if (!playerTarget)
            return;
        tempPos = transform.position;
        if(playerTarget.position.y<= player_BoundMin_Y )
        {
            tempPos = Vector3.Lerp(transform.position,new Vector3(playerTarget.position.x,playerTarget.position.y,-10),Time.deltaTime*smoothSpeed);
        }
        else
        {
            tempPos = Vector3.Lerp(transform.position, new Vector3(playerTarget.position.x, playerTarget.position.y+Y_Gap, -10), Time.deltaTime * smoothSpeed);
        }
        if (tempPos.x > player_BoundMax_X) tempPos.x = player_BoundMax_X;
        if (tempPos.x < player_BoundMin_X) tempPos.x = player_BoundMin_X;
        transform.position = tempPos;
    }
}
