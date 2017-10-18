using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 1f;

    private Rigidbody2D rb2d;

    private Vector2 moveInput;

    private void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start() {
    }

    private void Update() {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    private void FixedUpdate() {
        var moveInputSpeed = moveInput * speed;
        var newPos = new Vector2(rb2d.position.x + moveInputSpeed.x, rb2d.position.y + moveInputSpeed.y);
        rb2d.MovePosition(newPos);
    }
}
