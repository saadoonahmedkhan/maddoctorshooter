using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private float MoveSpeed = 15f;
    [SerializeField]
    private float DamageAmount = 35;
    private Vector3 move = Vector3.zero;
    private Vector3 tempScale;
    private void Start()
    {
        SetNegativeSpeed();
    }
    private void Update()
    {
        MoveBullet(); 
    }
    void MoveBullet()
    {
        move.x = MoveSpeed * Time.deltaTime;
        transform.position += move;
    }
    void SetNegativeSpeed()
    {
        MoveSpeed *= -1f;
        tempScale = transform.localScale;
        tempScale.x = -tempScale.x;
        transform.localScale = tempScale;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
     if(collision.CompareTag(TagManager.ENEMY_TAG))
        {
            //Deal Damage
        }
    }
}
