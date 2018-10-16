using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POHit : MonoBehaviour {

    [SerializeField] private LayerMask lm;
    [SerializeField] private float minSpeed = 1;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb.velocity.magnitude > minSpeed)
        {
            if (lm == (lm | (1 << collision.gameObject.layer)))
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
