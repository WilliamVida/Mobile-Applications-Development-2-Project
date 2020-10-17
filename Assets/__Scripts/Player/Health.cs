using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    // if the player dies then the health becomes 100 for some reason
    private float startingPlayerHealth = 100f;
    private float maxPlayerHealth = 200f;
    public float currentPlayerHealth;
    [SerializeField] private TextMeshProUGUI healthText;

    private void Start()
    {
        currentPlayerHealth = startingPlayerHealth;
    }

    private void Update()
    {
        healthText.text = currentPlayerHealth.ToString();

        // set to the maximum health if it goes over the limit
        if (currentPlayerHealth >= maxPlayerHealth)
        {
            currentPlayerHealth = maxPlayerHealth;
        }
        // set to the minimum health
        else if (currentPlayerHealth <= 0)
        {
            currentPlayerHealth = 0;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D whatHitMe)
    {
        var enemy = whatHitMe.GetComponent<Enemy>();
        var enemyBoss = whatHitMe.GetComponent<EnemyBoss>();
        var enemyBossWeapon = whatHitMe.GetComponent<EnemyBossWeapon>();

        // if the player hits anything from the enemy
        if (enemy)
        {
            currentPlayerHealth -= enemy.DamageValue;
            healthText.text = currentPlayerHealth.ToString("");
        }

        if (enemyBoss)
        {
            currentPlayerHealth -= enemyBoss.DamageValue;
        }

        if (enemyBossWeapon)
        {
            currentPlayerHealth -= enemyBossWeapon.DamageValue;
        }

        if (currentPlayerHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}
