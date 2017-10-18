using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    private PoolManager poolManager;

    public GameObject enemy;

    public float initialDelay = 3.0f;
    public float spawnMinDelay = 1.0f;
    public float spawnMaxDelay = 2.0f;

    public bool runForever = true;
    public int spawnInstances = 1;
    public int numPerSpawn = 1;

    private bool isStarted = false;
    private bool isRunning = false;

    private Vector2 minBounds;
    private Vector2 maxBounds;

    private void Awake() {
        var bounds = GetComponent<Collider2D>().bounds;
        minBounds = bounds.min;
        maxBounds = bounds.max;
    }

    private void Start() {
        poolManager = FindObjectOfType<PoolManager>();
        poolManager.CreatePool(enemy, 50);
    }

    private void Update() {
        if (!isStarted) {
            initialDelay -= Time.deltaTime;
            if (initialDelay <= 0)
                isStarted = true;
        } else if (!isRunning && (runForever || spawnInstances > 0)) {
            StartCoroutine(SpawnCoroutine());
        }
    }

    private IEnumerator SpawnCoroutine() {
        isRunning = true;
        while (runForever || (spawnInstances - numPerSpawn) >= 0) {
            spawnInstances -= numPerSpawn;
            for (int i = 0; i < numPerSpawn; i++) {
                var obj = poolManager.ReuseObject(enemy, GetRandomPoint(minBounds, maxBounds), Quaternion.identity);
            }
            yield return new WaitForSeconds(Random.Range(spawnMinDelay, spawnMaxDelay));
        }
        isRunning = false;
    }

    private Vector2 GetRandomPoint(Vector2 minBounds, Vector2 maxBounds) {
        return new Vector2(Random.Range(minBounds.x, maxBounds.x), Random.Range(minBounds.y, maxBounds.y));
    }

}
