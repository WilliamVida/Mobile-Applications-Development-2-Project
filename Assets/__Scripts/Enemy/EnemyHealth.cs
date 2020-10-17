using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float startingEnemyHealth = 100.0f;
    private float currentEnemyHealth;
    private float bulletDamage = 50.0f;
    [SerializeField] private AudioClip dieSound;
    private SoundController sc;
    public delegate void EnemyKilled(EnemyHealth enemyHealth);

    public static EnemyKilled EnemyKilledEvent;
    public int ScoreValue { get { return scoreValue; } }
    [SerializeField] private int scoreValue = 10;

    private void Start()
    {
        sc = SoundController.FindSoundController();
        currentEnemyHealth = startingEnemyHealth;
    }

    private void OnTriggerEnter2D(Collider2D whatHitMe)
    {
        var bullet = whatHitMe.GetComponent<Bullet>();

        if (bullet)
        {
            currentEnemyHealth -= bulletDamage;
        }

        if (currentEnemyHealth <= 0)
        {
            // sometimes might give error "NullReferenceException: Object reference not set to an instance of an object"
            sc.PlayOneShot(dieSound);

            PublishEnemyKilledEvent();
            Destroy(gameObject);
        }
    }

    private void PublishEnemyKilledEvent()
    {
        if (EnemyKilledEvent != null)
        {
            EnemyKilledEvent(this);
        }
    }

}
