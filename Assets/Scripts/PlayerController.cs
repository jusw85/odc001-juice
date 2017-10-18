using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 1f;
    public float fireCooldown = 0.5f;
    public GameObject bullet;

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
    }

    private void Update() {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        var velocity = moveInput * speed;
        Vector3 displacement = velocity * Time.deltaTime;
        Move(displacement);

        var isFirePressed = Input.GetButton("Jump");
        if (isFirePressed && canFire) {
            canFire = false;
            Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);
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

}
