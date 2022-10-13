using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// To move the player from node to node.
/// </summary>
public class MovementController : MonoBehaviour
{
    [Tooltip("Store current node.")]
    public GameObject currentNode;

    [Tooltip("Initial speed of player.")]
    public float speed = 4f;

    [Tooltip("Store the current direction of player.")]
    public string direction = "";

    [Tooltip("Store the last direction of player.")]
    public string lastMovingDirection = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        NodeController currentNodeController = currentNode.GetComponent<NodeController>(); 

        transform.position = Vector2.MoveTowards(transform.position, currentNode.transform.position, speed * Time.deltaTime);
        
        //Figure out if we are at the center of our current node 
        if(transform.position.x == currentNode.transform.position.x && transform.position.y == currentNode.transform.position.y)
        {
            //Get the next node from our node controller using our current direction.
            GameObject newNode = currentNodeController.GetNodeFromDirection(direction);

            //If we can move in a desired direction
            if (newNode != null)
            {
                currentNode = newNode;
                lastMovingDirection = direction;
            }
            // We can't move in na desired direction, try to keep going in the last moving direction.
            else
            {
                direction = lastMovingDirection;
                newNode = currentNodeController.GetNodeFromDirection(direction);
                if (newNode != null)
                {
                    currentNode = newNode;
                }
            }
        }
    }

    public void SetDirection(string newDirection)
    {
        direction = newDirection;
    }
}
