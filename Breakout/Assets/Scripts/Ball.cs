using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float halfWidth = collision.collider.bounds.size.x;
            float x = (transform.position.x - collision.transform.position.x) / halfWidth;

            Vector2 direction = new(4 * x, 1);
            direction = direction.normalized;

            float currentSpeed = rb.velocity.magnitude;

            rb.velocity = direction * currentSpeed;
        }


    }

}
