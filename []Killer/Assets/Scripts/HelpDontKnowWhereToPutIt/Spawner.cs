using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author:          Anna Mach
 * Collaborators:   
 */
public class Spawner : MonoBehaviour
{
    public Transform spawnedContainer;
    public GameObject prefabToSpawn;

    public float timeBetweenSpawns = 10;
    public int maxObjectsSpawned = 10;

    private float timer = 0;

    private void Start()
    {
        if(spawnedContainer == null)
        {
            spawnedContainer = gameObject.transform.parent;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenSpawns)
        {
            timer = 0;
            if(spawnedContainer.childCount < maxObjectsSpawned)
            {
                Instantiate(prefabToSpawn, transform.position, Quaternion.identity, spawnedContainer);
            }
        }
    }
}
