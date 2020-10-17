using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=CLSiRf_OrBk
public class FireRatePowerUp : MonoBehaviour
{
    [SerializeField] private float speed = 4.0f;
    private Rigidbody2D rb;
    private float fireRateIncrease = 0.075f;
    private float fireRateNormal = 0.15f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.velocity = new Vector2(-1 * speed, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(PickUp(other));
        }
    }

    IEnumerator PickUp(Collider2D player)
    {
        // increase the fire rate for a period then revert back
        WeaponsController wc = player.GetComponent<WeaponsController>();
        wc.firingRate = fireRateIncrease;

        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;

        yield return new WaitForSeconds(2.0f);

        wc.firingRate = fireRateNormal;
        Destroy(gameObject);
    }

}
