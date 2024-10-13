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
        // 1. Map dreht sich nach oben
        mapAnimator.SetTrigger(mapRotateTrigger);
        yield return new WaitForSeconds(delayBetweenSteps);

        // 2. Elevator steigt auf
        elevatorAnimator.SetTrigger(elevatorRiseTrigger);
        yield return new WaitForSeconds(delayBetweenSteps);

        // 3. Player wird aktiviert
        player.SetActive(true);
        yield return new WaitForSeconds(delayBetweenSteps);

        // 4. Elevator �ffnet sich
        elevatorAnimator.SetTrigger(elevatorOpenTrigger);

        // Spieler soll den Aufzug verlassen k�nnen, bevor die n�chsten Schritte ablaufen
    }

    void Update()
    {
        // �berpr�fen, ob der Spieler den Aufzug verlassen hat
        if (playerExitedElevator)
        {
            CloseAndDescendElevator();
        }
    }

    private void CloseAndDescendElevator()
    {
        // 5. Elevator schlie�t und f�hrt wieder nach unten
        elevatorAnimator.SetTrigger(elevatorCloseTrigger);
        StartCoroutine(DelayElevatorDescend());
    }

    private IEnumerator DelayElevatorDescend()
    {
        yield return new WaitForSeconds(delayBetweenSteps);
        elevatorAnimator.SetTrigger(elevatorDescendTrigger);
    }

    // Wird vom Spieler aufgerufen, wenn er den Aufzug verl�sst
    public void OnPlayerExitElevator()
    {
        playerExitedElevator = true;
    }
}
