using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to keep track of global things.
/// </summary>

public class GameManager : MonoBehaviour
{
    public GameObject pacman;
    [SerializeField] private ScoreController scoreController;
    public GameObject leftWrapNode;
    public GameObject rightWrapNode;

    public GameObject ghostNodeStart;
    public GameObject ghostNodeLeft;
    public GameObject ghostNodeRight;
    public GameObject ghostNodeCenter;

    public AudioSource siren;
    public AudioSource munch1;
    public AudioSource munch2;
    public int currentMunch;

    public enum GhostMode
    {
        chase,
        scatter
    }

    public GhostMode currentGhostMode;

    private void Awake()
    {
        ghostNodeStart.GetComponent<NodeController>().isGhostAtStartingNode = true;
        Debug.Log("isGhostAtStartingNode = true");

        currentGhostMode = GhostMode.chase;
        pacman = GameObject.Find("Player");
        scoreController = GameObject.Find("ScoreController").GetComponent<ScoreController>();
        currentMunch = 0;
        siren.Play(); 
    }

    //Check pellet is present or not in the node
    public void CollectedPellet(NodeController nodeController)
    {
        if(currentMunch == 0)
        {
            munch1.Play();
            currentMunch = 1;
        }
        else if(currentMunch == 1)
        {
            munch2.Play();
            currentMunch = 0;
        }
        scoreController.AddToScore(10);
    }
}
