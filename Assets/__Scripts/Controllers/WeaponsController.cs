using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WeaponsController : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 6.0f;
    [SerializeField] private Bullet bulletPrefab;
    public float firingRate = 0f;
    [SerializeField] private AudioClip shootClip;
    [SerializeField] [Range(0f, 1.0f)] private float shootVolume = 0.5f;

    private Coroutine firingCoroutine;
    private GameObject bulletParent;
    private AudioSource audioSource;

    private void Start()
    {
        firingRate = 0.15f;
        bulletParent = GameObject.Find("BulletParent");
        if (!bulletParent)
        {
            bulletParent = new GameObject("BulletParent");
        }
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            firingCoroutine = StartCoroutine(FireCoroutine());
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    private IEnumerator FireCoroutine()
    {
        while (true)
        {
            Bullet bullet = Instantiate(bulletPrefab, bulletParent.transform);
            bullet.transform.position = transform.position;
            audioSource.PlayOneShot(shootClip, shootVolume);

            Rigidbody2D rbb = bullet.GetComponent<Rigidbody2D>();
            rbb.velocity = Vector2.right * bulletSpeed;
            yield return new WaitForSeconds(firingRate);
        }
    }

}
