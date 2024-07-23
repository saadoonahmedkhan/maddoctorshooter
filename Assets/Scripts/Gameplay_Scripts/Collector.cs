using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter(Collider2D Collision)
    {
        if(Collision.CompareTag(TagManager.BULLET_TAG))
        {
            Destroy(Collision.gameObject);
        }
    }
}
