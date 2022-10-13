using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// to tell the player what direction you have to move.
/// </summary>

public class PlayerController : MonoBehaviour
{

    MovementController movementController;

    // Start is called before the first frame update
    void Start()
    {
        movementController = GetComponent<MovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            movementController.SetDirection("left");
        } 
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            movementController.SetDirection("right");
        } 
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            movementController.SetDirection("up");
        } 
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            movementController.SetDirection("down");
        }
    }
}
