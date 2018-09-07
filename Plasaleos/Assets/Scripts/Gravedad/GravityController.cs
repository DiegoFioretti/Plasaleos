using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour {

    [SerializeField] private float force = 9.8f;
    private Vector2 gravity;

	// Use this for initialization
	void Start () {
        ChangeGravity();
	}
	
	// Update is called once per frame
	void FixedUpdate() {
        ChangeGravity();
	}

    private void ChangeGravity()
    {
        gravity = -transform.up * force;
        Physics2D.gravity = gravity;
    }
}
