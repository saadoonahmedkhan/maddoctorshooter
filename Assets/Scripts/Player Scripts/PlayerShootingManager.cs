using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject BulletPrefab;
    [SerializeField]
    private Transform BulletSpawnPos;
    public void Shoot(float FacingDirection)
    {
        GameObject newBullet = Instantiate(BulletPrefab,BulletSpawnPos.position,Quaternion.identity);
        if(FacingDirection<0)
        {
            newBullet.GetComponent<Bullet_Scripts>().SetNegativeSpeed();
        }
    }
}
