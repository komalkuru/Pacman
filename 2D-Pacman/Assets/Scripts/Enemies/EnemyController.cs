using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum GhostNodeState
    {
        respawning,
        leftNode,
        rightNode,
        centerNode,
        startNode,
        movingInNodes
    }

    public enum GhostType
    {
        red, 
        blue, 
        pink, 
        orange
    }

    public GhostNodeState currentGhostNodeState;
    public GhostNodeState respawnState;//Where ghost want to go back for respawning
    public GhostType ghostType;

    //[SerializeField] private
    public GameObject ghostNodeStart;
    public GameObject ghostNodeLeft;
    public GameObject ghostNodeRight;
    public GameObject ghostNodeCenter;

    public MovementController movementController;

    public GameObject startingNode;
    public GameManager gameManager;


    public bool readyToLeaveHome = false;
    public bool testRespone = false;
    public bool isFrightened = false;

    public GameObject[] scatterNodes;
    public int scatterNodeIndex;

    void Awake()
    {
        scatterNodeIndex = 0;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        movementController = gameObject.GetComponent<MovementController>();

        //Chnage the state of the Enemy on the basis of the ghost colour
        if(ghostType == GhostType.red)
        {
            currentGhostNodeState = GhostNodeState.startNode;
            respawnState = GhostNodeState.centerNode;
            startingNode = ghostNodeStart;
            readyToLeaveHome = true; 
        } 
        else if (ghostType == GhostType.pink)
        {
            currentGhostNodeState = GhostNodeState.centerNode;
            startingNode = ghostNodeCenter;
            respawnState = GhostNodeState.centerNode;
        }
        else if (ghostType == GhostType.blue)
        {
            currentGhostNodeState = GhostNodeState.leftNode;
            respawnState = GhostNodeState.leftNode;
            startingNode = ghostNodeLeft;
        }
        else if (ghostType == GhostType.orange)
        {
            currentGhostNodeState = GhostNodeState.rightNode;
            respawnState = GhostNodeState.rightNode;
            startingNode = ghostNodeRight; 
        }
        //Ghost is going to move into it's start position
        movementController.currentNode = startingNode;
        transform.position = startingNode.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(testRespone == true)
        {
            readyToLeaveHome = false;
            currentGhostNodeState = GhostNodeState.respawning;
            testRespone = false;
        }
    }

    //By default ghosts are goin to reache center of the nodes as soon as we turn on the game
    //ghost immediately start moving towards the startnode
    public void ReachedCenterOfTheNode(NodeController nodeController)
    {
        if(currentGhostNodeState == GhostNodeState.movingInNodes)
        {
            //Scatter Mode
            if(gameManager.currentGhostMode == GameManager.GhostMode.scatter)
            {
                Debug.Log("ScatterMode checking");
                //If ghost reached scatter node, add 1 node to the scatter node index
                if(transform.position.x == scatterNodes[scatterNodeIndex].transform.position.x && transform.position.y == scatterNodes[scatterNodeIndex].transform.position.y)
                {
                    scatterNodeIndex++;
                    Debug.Log("ScatterMode checking");

                    if (scatterNodeIndex == scatterNodes.Length - 1)
                    {
                        scatterNodeIndex = 0;
                    }
                }

                string direction = GetClosestDirection(scatterNodes[scatterNodeIndex].transform.position);
                movementController.SetDirection(direction);
            }
            //Frightened mode
            else if(isFrightened)
            {

            }
            //Chase Mode
            else
            {

            }
            //Determine next game node to go to
            if(ghostType == GhostType.red)
            {
                DetermineRedGhostDirection();
            }
        } else if(currentGhostNodeState == GhostNodeState.respawning)
        {
            string direction = "";
            //Determine where the ghost currently are
            //Ghost reached their start node, move to the center node 
            if(transform.position.x == ghostNodeStart.transform.position.x && transform.position.y == ghostNodeStart.transform.position.y)
            {
                Debug.Log("gone down");

                direction = "down";
            }
            //If ghost reached their center node, either finish their respawn, or move to the left or right node
            else if(transform.position.x == ghostNodeCenter.transform.position.x && transform.position.y == ghostNodeCenter.transform.position.y)
            {
                if(respawnState == GhostNodeState.centerNode)
                {
                    //Ghost reached at center node and ready to respawn
                    currentGhostNodeState = respawnState;
                } else if(respawnState == GhostNodeState.leftNode)
                {
                    direction = "left";

                }
                else if (respawnState == GhostNodeState.rightNode)
                {
                    direction = "right";

                }
            }
            //If ghost's respawn state is either the left or right node, and ghost got to that node, leave home again
            else if((transform.position.x == ghostNodeLeft.transform.position.x && transform.position.y == ghostNodeLeft.transform.position.y)
                || (transform.position.x == ghostNodeRight.transform.position.x && transform.position.y == ghostNodeRight.transform.position.y))
            {
                currentGhostNodeState = respawnState;
            }
            //Ghost are in the game board still, locate Ghost's start node
            else
            {
                //Determine quickest direction to home
                direction = GetClosestDirection(ghostNodeStart.transform.position);
            }
            movementController.SetDirection(direction);
        }
        else
        {
            //if ghosts are ready to leave home
            if(readyToLeaveHome)
            {
                //If ghosts are in left home node, move to the center
                if(currentGhostNodeState == GhostNodeState.leftNode)
                {
                    currentGhostNodeState = GhostNodeState.centerNode;
                    movementController.SetDirection("right");
                }
                //If ghosts are in right home node, move to the center
                else if (currentGhostNodeState == GhostNodeState.rightNode)
                {
                    currentGhostNodeState = GhostNodeState.centerNode;
                    movementController.SetDirection("left");
                }
                //If ghosts are in center of the home node, move to the start node 
                else if(currentGhostNodeState == GhostNodeState.centerNode)
                {
                    currentGhostNodeState = GhostNodeState.startNode;
                    movementController.SetDirection("up");
                }
                //If ghost are in start of the home node, start moving around the game
                else if(currentGhostNodeState == GhostNodeState.startNode)
                {
                    currentGhostNodeState = GhostNodeState.movingInNodes;
                    movementController.SetDirection("left");
                }
            }
        }
    }

    void DetermineRedGhostDirection()
    {
        string direction = GetClosestDirection(gameManager.pacman.transform.position);
        movementController.SetDirection(direction);
    }

    void DeterminePinkGhostDirection()
    {

    }

    void DetermineBlueGhostDorection()
    {

    }

    void DetermineOrangeGhostDirection()
    {

    }

    //Grab the distance between each direction 
    string GetClosestDirection(Vector2 target)
    {
        float shortestDistance = 0;
        string lastMovingDirection = movementController.lastMovingDirection;
        string newDirection = "";

        //Check all the available directions that enemy can go right now and fogure out which one direction is shortest.
        NodeController nodeController = movementController.currentNode.GetComponent<NodeController>();

        //If we can move up and we aren't reversing 
        if(nodeController.canMoveUp && lastMovingDirection != "down")
        {
            //Get the node above us
            GameObject nodeUp = nodeController.nodeUp;

            //Get the distance from our top node to pacman
            float distance = Vector2.Distance(nodeUp.transform.position, target);

            //If this is a shortest distance so far, set the enemy's direction
            if(distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "up";
            }
        }

        if (nodeController.canMoveDown && lastMovingDirection != "up")
        {
            //Get the node above us
            GameObject nodeDown = nodeController.nodeDown;

            //Get the distance from our top node to pacman
            float distance = Vector2.Distance(nodeDown.transform.position, target);

            //If this is a shortest distance so far, set the enemy's direction
            if (distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "down";
            }
        }

        if (nodeController.canMoveLeft && lastMovingDirection != "right")
        {
            //Get the node above us
            GameObject nodeLeft = nodeController.nodeLeft;

            //Get the distance from our top node to pacman
            float distance = Vector2.Distance(nodeLeft.transform.position, target);

            //If this is a shortest distance so far, set the enemy's direction
            if (distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "left";
            }
        }

        if (nodeController.canMoveRight && lastMovingDirection != "left")
        {
            //Get the node above us
            GameObject nodeRight = nodeController.nodeRight;

            //Get the distance from our top node to pacman
            float distance = Vector2.Distance(nodeRight.transform.position, target);

            //If this is a shortest distance so far, set the enemy's direction
            if (distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "right";
            }
        }
        return newDirection;
    }
}
 