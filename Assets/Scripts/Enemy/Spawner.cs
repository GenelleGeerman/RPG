using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnDelay;
    [SerializeField] private int spawnCount;
    [SerializeField] private int maxSpawn;
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private bool spawnable;
    [SerializeField] private bool isSingleSpawn;
    private GameObject currentSpawn;
    public bool IsSpawnable {  get => spawnable; set => spawnable = value; }
    private void Start()
    {
        if (isSingleSpawn && spawnable && currentSpawn == null)
        {
            Invoke("SingleSpawn", spawnDelay);
        }
    }
    private void FixedUpdate()
    {
        if (spawnable && !isSingleSpawn && spawnCount < maxSpawn)
        {
            StartCoroutine(Spawn());
        }
    }
    private IEnumerator Spawn()
    {
        spawnable = false;
        var spawn = Instantiate(spawnPrefab, transform);
        spawnCount++;
        yield return new WaitForSeconds(spawnDelay);
        spawnable = true;
    }

    private void SingleSpawn()
    {
        spawnable = false;
        currentSpawn = Instantiate(spawnPrefab, transform);
        spawnCount++;

    }

   
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, new Vector2(0.5f, 0.5f));
    }
}
