using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class Raycaster : MonoBehaviour {

    public LayerMask collisionMask;
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;

    private RaycastOrigins raycastOrigins;
    private new BoxCollider2D collider;

    private float horizontalRaySpacing;
    private float verticalRaySpacing;
    private const float skinWidth = .05f;

    private void Awake() {
        collider = GetComponent<BoxCollider2D>();
        UpdateRaySpacing();
    }

    private void OnValidate() {
        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);
    }

    public void UpdateRaySpacing() {
        Bounds bounds = GetInnerBounds();
        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    private Bounds GetInnerBounds() {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);
        return bounds;
    }

    public void UpdateRaycastOrigins() {
        Bounds bounds = GetInnerBounds();

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    public void HandleCollisions(ref Vector3 displacement, out CollisionInfo collisions) {
        collisions = new CollisionInfo();
        HorizontalCollisions(ref displacement, ref collisions);
        VerticalCollisions(ref displacement, ref collisions);
    }

    private void HorizontalCollisions(ref Vector3 displacement, ref CollisionInfo collisions) {
        if (displacement.x == 0f) {
            return;
        }
        bool isMovingRight = Mathf.Sign(displacement.x) > 0f;
        Vector2 rayOrigin = isMovingRight ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft;
        float rayLength = Mathf.Abs(displacement.x) + skinWidth;
        Vector2 rayDirection = isMovingRight ? Vector2.right : Vector2.left;

        for (int i = 0; i < horizontalRayCount; i++) {
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, rayLength, collisionMask);
            Debug.DrawRay(rayOrigin, rayDirection * rayLength, Color.red);

            if (hit) {
                collisions.collider2D = hit.collider;
                rayLength = hit.distance;
                if (isMovingRight) collisions.right = true; else collisions.left = true;
            }
            rayOrigin += Vector2.up * horizontalRaySpacing;
        }

        displacement.x = rayDirection.x * (rayLength - skinWidth);
    }

    private void VerticalCollisions(ref Vector3 displacement, ref CollisionInfo collisions) {
        if (displacement.y == 0f) {
            return;
        }
        bool isMovingUp = Mathf.Sign(displacement.y) > 0f;
        Vector2 rayOrigin = isMovingUp ? raycastOrigins.topLeft : raycastOrigins.bottomLeft;
        rayOrigin.x += displacement.x;
        float rayLength = Mathf.Abs(displacement.y) + skinWidth;
        Vector2 rayDirection = isMovingUp ? Vector2.up : Vector2.down;

        for (int i = 0; i < verticalRayCount; i++) {
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, rayLength, collisionMask);
            Debug.DrawRay(rayOrigin, rayDirection * rayLength, Color.red);

            if (hit) {
                collisions.collider2D = hit.collider;
                rayLength = hit.distance;
                if (isMovingUp) collisions.above = true; else collisions.below = true;
            }
            rayOrigin += Vector2.right * verticalRaySpacing;
        }

        displacement.y = rayDirection.y * (rayLength - skinWidth);
    }

    public struct CollisionInfo {
        public bool
            left, right,
            above, below;
        public Collider2D collider2D;
    }

    private struct RaycastOrigins {
        public Vector2
            topLeft, topRight,
            bottomLeft, bottomRight;
    }

}
