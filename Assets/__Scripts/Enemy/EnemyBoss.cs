using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    public int DamageValue { get { return damageValue; } }
    public int damageValue = 150;
    private SoundController sc;
    [SerializeField] GameObject bullet;
    float fireRate;
    float nextFire;
    [SerializeField] private AudioClip shootClip;
    [SerializeField] [Range(0f, 1.0f)] private float shootVolume = 0.5f;
    private AudioSource audioSource;

    void Start()
    {
        StartCoroutine("HideUnhide");
        fireRate = 0.75f;
        nextFire = Time.time;
        audioSource = GetComponent<AudioSource>();
    }

    // https://stackoverflow.com/a/21567099
    // make the boss disappear and reappear
    IEnumerator HideUnhide()
    {
        while (true)
        {
            yield return (new WaitForSeconds(4));
            GetComponent<Renderer>().enabled = true;
            yield return (new WaitForSeconds(4));
            GetComponent<Renderer>().enabled = false;
        }
    }

    void Update()
    {
        if (GameObject.Find("Player") != null)
        {
            CheckIfTimeToFire();
        }
    }

    // boss firing weapon
    void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
            audioSource.PlayOneShot(shootClip, shootVolume);
        }
    }

    private void OnTriggerEnter2D(Collider2D whatHitMe)
    {
        var player = whatHitMe.GetComponent<PlayerMovement>();
        var bullet = whatHitMe.GetComponent<Bullet>();

        if (bullet)
        {
            Destroy(bullet.gameObject);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (sc)
        {
            sc.PlayOneShot(clip);
        }
    }

}
