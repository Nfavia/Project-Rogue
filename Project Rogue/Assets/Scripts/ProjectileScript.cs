using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField]
    private float despawnTime = 3f;
    private float timer;

    private void Start()
    {
        timer = Time.time + despawnTime;
    }

    private void Update()
    {
        if (Time.time >= timer)
            Destroy(gameObject);
    }

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
