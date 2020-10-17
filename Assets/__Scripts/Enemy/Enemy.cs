using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int DamageValue { get { return damageValue; } }
    [SerializeField] public int damageValue;
    [SerializeField] private GameObject explosionFX;
    private float explosionDuration = 1.0f;
    private SoundController sc;

    private void OnTriggerEnter2D(Collider2D whatHitMe)
    {
        var player = whatHitMe.GetComponent<PlayerMovement>();
        var bullet = whatHitMe.GetComponent<Bullet>();

        if (player)
        {
            Destroy(gameObject);
        }

        if (bullet)
        {
            Destroy(bullet.gameObject);
            GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);
            Destroy(explosion, explosionDuration);
        }
    }

}
