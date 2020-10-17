using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BulletDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var bullet = collision.GetComponent<Bullet>();
        if (bullet)
        {
            Destroy(bullet.gameObject);
        }

        var enemyBossWeapon = collision.GetComponent<EnemyBossWeapon>();
        if (enemyBossWeapon)
        {
            Destroy(enemyBossWeapon.gameObject);
        }
    }

}
