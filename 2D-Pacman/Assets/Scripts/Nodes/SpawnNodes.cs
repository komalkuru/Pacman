using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Create a game board.
/// </summary>

public class SpawnNodes : MonoBehaviour
{ 
    // how many times our node will spawn
    int numToSpawn = 28;

    [Tooltip("Represent the space between all the node ")]
    public float spawnOffset = 0.3f;

    [Tooltip("To store the current spawn node offset ")]
    public float currentSpawnOffset;

    void Start()
    {/*
        // To reaname the game object name
        gameObject.name = "Node";
        return;*/

        if(gameObject.name == "Node")
        {
            currentSpawnOffset = spawnOffset;
            for(int i = 0; i < numToSpawn; i++)
            {
                // Clone a new node
                GameObject nodeClone = Instantiate(gameObject, new Vector3(transform.position.x, transform.position.y + currentSpawnOffset, 0), Quaternion.identity);
                currentSpawnOffset += spawnOffset;
            }
        }
    }
}
