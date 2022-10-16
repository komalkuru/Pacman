using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// To move the player from node to node.
/// </summary>
public class MovementController : MonoBehaviour
{
    public GameManager gameManager; 

    [Tooltip("Store current node.")]
    public GameObject currentNode;

    [Tooltip("Initial speed of player.")]
    public float speed = 4f;

    [Tooltip("Store the current direction of player.")]
    public string direction = "";

    [Tooltip("Store the last direction of player.")]
    public string lastMovingDirection = "";

    public bool canWrap = true;
    public bool isGhost = false;
    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {        
        NodeController currentNodeController = currentNode.GetComponent<NodeController>(); 

        //Move towards it's current node
        transform.position = Vector2.MoveTowards(transform.position, currentNode.transform.position, speed * Time.deltaTime);

        // Reverse player movement smoothly
        bool reverseDirection = false;

        if((direction == "left" && lastMovingDirection == "right") || 
            (direction == "right" && lastMovingDirection == "left") ||
            (direction == "down" && lastMovingDirection == "up") ||
            (direction == "up" && lastMovingDirection == "down")) 
        {
            reverseDirection = true;
        }
        
        //Figure out if we are at the center of our current node 
        if(transform.position.x == currentNode.transform.position.x && transform.position.y == currentNode.transform.position.y)
         {
            if(isGhost)
            {
                //When player reach the center of the node before we use our direction, tell the ghost that player reach the center of the node.
                GetComponent<EnemyController>().ReachedCenterOfTheNode(currentNodeController);
            }
            //If we reached the center of the left wrap, wrap to the rightWrap
            if(currentNodeController.isWrapLeftNode && canWrap)
            {
                Debug.Log("left to right");
                currentNode = gameManager.rightWrapNode;
                direction = "left";
                lastMovingDirection = "left";
                transform.position = currentNode.transform.position; // switch the position
                canWrap = false;
            }
            //If we reached the center of the right wrap, wrap to the leftWrap
            else if(currentNodeController.isWrapRightNode && canWrap)
            {
                Debug.Log("right to left");
                currentNode = gameManager.leftWrapNode;
                direction = "right";
                lastMovingDirection = "right";
                transform.position = currentNode.transform.position; // switch the position
                canWrap = false;
            }
            //Otherwise firnd the next node
            else
            {
                //Id player is not a ghost that is respawning, and player are on the start node, and player trying to move down, stop.
                if(currentNodeController.isGhostAtStartingNode && direction == "down" 
                    && (!isGhost || GetComponent<EnemyController>().currentGhostNodeState != EnemyController.GhostNodeState.respawning))
                {
                    direction = lastMovingDirection;
                }
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
        //we are not center of the node
        else
        {
            canWrap = true;
        }
    }

    public void SetDirection(string newDirection)
    {
        direction = newDirection;
    }
}
