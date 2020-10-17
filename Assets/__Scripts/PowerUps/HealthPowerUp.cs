using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=CLSiRf_OrBk
public class HealthPowerUp : MonoBehaviour
{
    [SerializeField] private float speed = 4.0f;
    private Rigidbody2D rb;
    private float healthIncrease = 40f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(-1 * speed, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PickUp(other);
        }
    }

    private void PickUp(Collider2D player)
    {
        // increase health
        Health health = player.GetComponent<Health>();
        health.currentPlayerHealth += healthIncrease;
        Destroy(gameObject);
    }

}
