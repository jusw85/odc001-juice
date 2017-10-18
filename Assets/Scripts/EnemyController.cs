using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable {

    public float moveSpeed = 2f;

    //private PoolManager poolManager;

    private void Start() {
        //poolManager = FindObjectOfType<PoolManager>();
        //poolManager.CreatePool(bullet, 100);
    }

    private void Update() {
        var velocity = Vector2.down * moveSpeed;
        Vector3 displacement = velocity * Time.deltaTime;
        transform.Translate(displacement);
    }

    public void Damage() {
        Destroy(gameObject);
    }

}
