using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHit();
        }
        Destroy(gameObject);
    }

    private void EnemyHit()
    {
        Debug.Log("Enemy hit");
    }
}
