using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : PoolObject, IDamageable {

    public float moveSpeed = 2f;

    private void Start() {
    }

    private void Update() {
        var velocity = Vector2.down * moveSpeed;
        Vector3 displacement = velocity * Time.deltaTime;
        transform.Translate(displacement);
    }

    public void Damage() {
        Destroy();
    }

}
