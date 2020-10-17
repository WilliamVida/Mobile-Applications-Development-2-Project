using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BoundaryCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if objects hit then destroy them
        var enemy = collision.GetComponent<Enemy>();
        if (enemy)
        {
            Destroy(enemy.gameObject);
        }

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

        var healthPowerUp = collision.GetComponent<HealthPowerUp>();
        if (healthPowerUp)
        {
            Destroy(healthPowerUp.gameObject);
        }

        var fireRatePowerUp = collision.GetComponent<FireRatePowerUp>();
        if (fireRatePowerUp)
        {
            Destroy(fireRatePowerUp.gameObject);
        }
    }

}
