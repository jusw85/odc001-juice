using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : PoolObject {

    public float lifetime = 10f;
    public float speed = 4f;
    [NonSerialized]
    public Vector2 direction = Vector2.up;
    [NonSerialized]
    public string triggerTag = "Enemy";

    private Rigidbody2D rb2d;

    private void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        StartCoroutine(DestroyAfterLifetime());
    }

    private void FixedUpdate() {
        rb2d.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        var tag = collision.gameObject.tag;
        if (tag.Equals(triggerTag)) {
        }
    }

    private IEnumerator DestroyAfterLifetime() {
        yield return new WaitForSeconds(lifetime);
        Destroy();
    }
}
