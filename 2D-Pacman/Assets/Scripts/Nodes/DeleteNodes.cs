using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// create a unneccessary nodes from the board.
/// </summary>
public class DeleteNodes : MonoBehaviour
{
    // Detect node to delete that unwanted node.
    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if(collision.tag == "Node")
        {
            Destroy(collision.gameObject);
        }
    }
}
