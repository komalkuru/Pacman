using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    //To find adjacent nodes
    [Tooltip("Enables if there is a adjacent node to the left.")]
    public bool canMoveLeft = false;

    [Tooltip("Enables if there is a adjacent node to the right.")]
    public bool canMoveRight = false;

    [Tooltip("Enables if there is a adjacent node to the up.")]
    public bool canMoveUp = false;

    [Tooltip("Enables if there is a adjacent node to the down.")]
    public bool canMoveDown = false;

    [Tooltip("Store left adjacent node of current node.")]
    public GameObject nodeLeft;

    [Tooltip("Store right adjacent node of current node.")]
    public GameObject nodeRight;

    [Tooltip("Store upward adjacent node of current node.")]
    public GameObject nodeUp;

    [Tooltip("Store downward adjacent node of current node.")]
    public GameObject nodeDown;

    [Tooltip("Wrap the player right to left.")]
    public bool isWrapRightNode = false;

    [Tooltip("Wrap the player left to right.")]
    public bool isWrapLeftNode = false;

    private void Awake()
    {
        //This is goig to send out the line up, down, left and right and it's goind to grab every sing thing that is touches.
        //To hits multiple things
        RaycastHit2D[] hitsDown;
        hitsDown = Physics2D.RaycastAll(transform.position, -Vector2.up); // Hit the line downward
        
        // Loop through all of the gameobject that the raycast hits
        for(int i=0; i < hitsDown.Length; i++)
        {
            // get the distance between the node which get the hit - current node (node2 - node1) 
            float distance = Mathf.Abs(hitsDown[i].point.y - transform.position.y);

            if(distance < 0.3f)
            {
                canMoveDown = true;
                nodeDown = hitsDown[i].collider.gameObject;
            }
        }

        //check upward hit
        RaycastHit2D[] hitsUp;
        hitsUp = Physics2D.RaycastAll(transform.position, Vector2.up); // Hit the line upward

        // Loop through all of the gameobject that the raycast hits
        for (int i = 0; i < hitsUp.Length; i++)
        {
            // get the distance between the node which get the hit - current node (node2 - node1) 
            float distance = Mathf.Abs(hitsUp[i].point.y - transform.position.y);

            if (distance < 0.3f)
            {
                canMoveUp = true;
                nodeUp = hitsUp[i].collider.gameObject;
            }
        }

        //check rightward rayhit
        RaycastHit2D[] hitsRight;
        hitsRight = Physics2D.RaycastAll(transform.position, Vector2.right); // Hit the line rightward

        // Loop through all of the gameobject that the raycast hits
        for (int i = 0; i < hitsRight.Length; i++)
        {
            // get the distance between the node which get the hit - current node (node2 - node1) 
            float distance = Mathf.Abs(hitsRight[i].point.x - transform.position.x);

            if (distance < 0.3f)
            {
                canMoveRight = true;
                nodeRight = hitsRight[i].collider.gameObject;
            }
        }

        //check leftward rayhit
        RaycastHit2D[] hitsLeft;
        hitsLeft = Physics2D.RaycastAll(transform.position, -Vector2.right); // Hit the line leftward

        // Loop through all of the gameobject that the raycast hits
        for (int i = 0; i < hitsLeft.Length; i++)
        {
            // get the distance between the node which get the hit - current node (node2 - node1) 
            float distance = Mathf.Abs(hitsLeft[i].point.x - transform.position.x);

            if (distance < 0.3f)
            {
                canMoveLeft = true;
                nodeLeft = hitsLeft[i].collider.gameObject;
            }
        }
    }

    public GameObject GetNodeFromDirection(string direction)
    {
        if(direction == "left" && canMoveLeft)
        {
            return nodeLeft;
        } 
        else if(direction == "right" && canMoveRight)
        {
            return nodeRight;
        } 
        else if(direction == "down" && canMoveDown)
        {
            return nodeDown;
        } 
        else if(direction == "up" && canMoveUp)
        {
            return nodeUp;
        } else
        {
            return null;
        }
    }
}
