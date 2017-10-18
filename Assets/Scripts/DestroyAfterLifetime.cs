using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterLifetime : MonoBehaviour {

    public float lifetime = 3f;

    private void Start() {
        StartCoroutine(DestroyAfterLifetimeRoutine());
    }

    private IEnumerator DestroyAfterLifetimeRoutine() {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

}
