using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private float health = 100f;
    private Enemy enemyScript;
    [SerializeField]
    private Slider EnemyHealthSlider;
    private void Awake()
    {
        enemyScript = GetComponent<Enemy>();
    }
    public void TakeDamage( float damageAmount)
    {
        if (health <= 0)
            return;
        health -= damageAmount;
        if(health<= 0)
        {
            health = 0;
            enemyScript.EnemyDied();
        }
        EnemyHealthSlider.value = health;
    }
}
