using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform playerTarget;
    [SerializeField]
    private float moveSpeed = 2f;
    private Vector3 tempScale;
    [SerializeField]
    private float stoppingDistance = 1.5f;
    private PlayerAnimation enemyAnimation;
    [SerializeField]
    private float attackWaitTime  = 2.5f;
    private float attackTimer;
    [SerializeField]
    private float AttackFinishedWaitTime = 0.5f;
    private float attackFinishedTimer;
    [SerializeField]
    private EnemyDamageArea enemyDamageArea;
    private bool enemyDied;
    private void Awake()
    {
        playerTarget = GameObject.FindWithTag(TagManager.PLAYER_TAG).transform;
        enemyAnimation = GetComponent<PlayerAnimation>();
    }
    private void Update()
    {
        if(enemyDied)
        {
            return;
        }
        SearchForPlayer();  
    }
    private void SearchForPlayer()
    {
        if (!playerTarget)
        {
            return;
        }
        else
        {
            if (Vector3.Distance(transform.position, playerTarget.position)> stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerTarget.position, moveSpeed * Time.deltaTime);
                enemyAnimation.PlayAnimation(TagManager.WALK_ANIMATION_NAME);
                HandleFacingDirection();
            }
            else
            {
                CheckIfAttackFinished();
                Attack();
            }
        }
    }
    void HandleFacingDirection()
    {
        tempScale = transform.localScale;
        if(transform.position.x>playerTarget.position.x)
        {
            tempScale.x = Mathf.Abs(tempScale.x);
        }
        else
        {
            tempScale.x = -Mathf.Abs(tempScale.x);
        }
        transform.localScale = tempScale;
    }
    void CheckIfAttackFinished()
    {
        if(Time.time>AttackFinishedWaitTime)
        {
            enemyAnimation.PlayAnimation(TagManager.IDLE_ANIMATION_NAME);
        }
    }
    void Attack()
    {
        if (Time.time > attackTimer)
        {
            attackFinishedTimer = Time.time + AttackFinishedWaitTime;
            attackTimer = Time.time + attackWaitTime;
            enemyAnimation.PlayAnimation(TagManager.ATTACK_ANIMATION_NAME);
        }
    }
    void EnemyAttacked()
    {
        enemyDamageArea.gameObject.SetActive(true);
        enemyDamageArea.ResetDeactivateTimer();
    }
    public void EnemyDied()
    {
        enemyDied = true;
        enemyAnimation.PlayAnimation(TagManager.DEATH_ANIMATION_NAME);
        Invoke("DestroyEnemyAfterDelay",1.5f);
    }
    void DestroyEnemyAfterDelay()
    {
        Destroy(gameObject);
    }
}
