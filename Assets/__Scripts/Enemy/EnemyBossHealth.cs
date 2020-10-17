using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossHealth : MonoBehaviour
{
    private float startingEnemyHealth = 2000.0f;
    private float currentEnemyHealth;
    private float bulletDamage = 50.0f;
    [SerializeField] private AudioClip dieSound;
    private SoundController sc;
    public delegate void EnemyKilled(EnemyBossHealth enemyBossHealth);

    public static EnemyKilled EnemyKilledEvent;
    public int ScoreValue { get { return scoreValue; } }
    private int scoreValue = 500;
    private AudioSource audioSource;
    [SerializeField] private GameObject explosionFX;
    private float explosionDuration = 4.0f;

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
            // https://answers.unity.com/questions/385639/how-to-spawn-prefabs-with-percent-random.html
            // chance to suffer no damage
            if (Random.value > 0.4)
            {
                currentEnemyHealth -= bulletDamage;
            }
        }

        if (currentEnemyHealth <= 0)
        {   
            sc.PlayOneShot(dieSound);
            GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);
            Destroy(explosion, explosionDuration);
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
