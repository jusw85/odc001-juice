using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 1f;

    private Rigidbody2D rb2d;
    private Raycaster raycaster;

    private Vector2 moveInput;

    private void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
        raycaster = GetComponent<Raycaster>();
    }

    private void Start() {
    }

    private void Update() {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        var velocity = moveInput * speed;
        Vector3 displacement = velocity * Time.deltaTime;
        Move(displacement);
    }

    private void Move(Vector3 displacement) {
        Raycaster.CollisionInfo collisions;

        raycaster.UpdateRaycastOrigins();
        raycaster.HandleCollisions(ref displacement, out collisions);

        transform.Translate(displacement);
    }

}
