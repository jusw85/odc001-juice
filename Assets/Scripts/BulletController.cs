using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float speed = 4f;

    [NonSerialized]
    public Vector2 direction = Vector2.up;
    [NonSerialized]
    public string triggerTag = "Player";

    private Rigidbody2D rb2d;

    private void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        rb2d.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision) {

    }

}
