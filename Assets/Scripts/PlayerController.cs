using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable {

    public float moveSpeed = 1f;
    //[Range(0.01f, 5f)]
    public float fireCooldown = 0.5f;
    public float bulletSpeed = 4f;
    public GameObject bullet;
    public GameObject deathExplosion;

    private PoolManager poolManager;

    private Rigidbody2D rb2d;
    private Raycaster raycaster;
    private Transform bulletSpawnPoint;

    private Vector2 moveInput;
    private bool canFire = true;

    private void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
        raycaster = GetComponent<Raycaster>();
        bulletSpawnPoint = transform.Find("BulletSpawnPoint");
    }

    private void Start() {
        poolManager = FindObjectOfType<PoolManager>();
        poolManager.CreatePool(bullet, 100);
    }

    private void Update() {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        var velocity = moveInput * moveSpeed;
        Vector3 displacement = velocity * Time.deltaTime;
        Move(displacement);

        var isFirePressed = Input.GetButton("Jump");
        if (isFirePressed && canFire) {
            canFire = false;
            var bulletObj = poolManager.ReuseObject(bullet, bulletSpawnPoint.position, Quaternion.identity);
            var bulletController = bulletObj.GetComponent<BulletController>();
            bulletController.speed = bulletSpeed;
            bulletController.direction = Vector2.up;
            bulletController.triggerTag = "Enemy";
            StartCoroutine(FireCoolDownRoutine());
        }
    }

    private IEnumerator FireCoolDownRoutine() {
        yield return new WaitForSeconds(fireCooldown);
        canFire = true;
    }

    private void Move(Vector3 displacement) {
        Raycaster.CollisionInfo collisions;

        raycaster.UpdateRaycastOrigins();
        raycaster.HandleCollisions(ref displacement, out collisions);

        transform.Translate(displacement);
    }

    private void OnValidate() {
        moveSpeed = Mathf.Clamp(moveSpeed, 0f, float.MaxValue);
        fireCooldown = Mathf.Clamp(fireCooldown, 0f, float.MaxValue);
        bulletSpeed = Mathf.Clamp(bulletSpeed, 0f, float.MaxValue);
    }

    public void Damage() {
        Instantiate(deathExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
