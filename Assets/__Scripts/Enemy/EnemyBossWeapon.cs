using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=kOzhE3_P2Mk
public class EnemyBossWeapon : MonoBehaviour
{
    float moveSpeed = 17.5f;
    public int DamageValue { get { return damageValue; } }
    int damageValue = 100;
    [SerializeField] [Range(0f, 1.0f)] private float shootVolume = 0.5f;
    Rigidbody2D rb;
    PlayerMovement target;
    Vector2 moveDirection;

    void Start()
    {
        // if player exists then shoot
        if (GameObject.Find("Player") != null)
        {
            rb = GetComponent<Rigidbody2D>();
            target = GameObject.FindObjectOfType<PlayerMovement>();
            moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        var player = col.GetComponent<PlayerMovement>();

        if (col.gameObject.name.Equals("Player"))
        {
            Destroy(gameObject);
        }
    }

}
