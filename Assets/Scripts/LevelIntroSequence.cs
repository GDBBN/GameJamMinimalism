using UnityEngine;
using System;
using System.Collections;

public class LevelIntroSequence : MonoBehaviour
{
    public Animator mapAnimator;
    public Animator elevatorAnimator;
    public GameObject player;
    public string mapRotateTrigger = "RotateUp";
    public string elevatorRiseTrigger = "RiseUp";
    public string elevatorOpenTrigger = "Open";
    public string elevatorCloseTrigger = "Close";
    public string elevatorDescendTrigger = "Descend";
    public float delayBetweenSteps = 1f;

    private bool playerExitedElevator = false;

    void Start()
    {
        player.SetActive(false);
        StartCoroutine(PlayIntroSequence());
    }

    private IEnumerator PlayIntroSequence()
    {
        //Map Anim
        mapAnimator.SetTrigger(mapRotateTrigger);
        yield return new WaitForSeconds(delayBetweenSteps);

        //elevator rises
        elevatorAnimator.SetTrigger(elevatorRiseTrigger);
        yield return new WaitForSeconds(delayBetweenSteps);

        //activate Player
        player.SetActive(true);
        yield return new WaitForSeconds(delayBetweenSteps);

        //elevator opens
        elevatorAnimator.SetTrigger(elevatorOpenTrigger);
    }

    void Update()
    {
        if (playerExitedElevator)
        {
            CloseAndDescendElevator();
        }
    }

    private void CloseAndDescendElevator()
    {
        //Elevator closes and descends
        elevatorAnimator.SetTrigger(elevatorCloseTrigger);
        StartCoroutine(DelayElevatorDescend());
    }

    private IEnumerator DelayElevatorDescend()
    {
        yield return new WaitForSeconds(delayBetweenSteps);
        elevatorAnimator.SetTrigger(elevatorDescendTrigger);
    }
    
    public void OnPlayerExitElevator()
    {
        playerExitedElevator = true;
    }
}
