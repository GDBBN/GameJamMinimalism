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
    public float delayBetweenSteps = 1f; // Zeitverzögerung zwischen den Schritten

    private bool playerExitedElevator = false;

    void Start()
    {
        player.SetActive(false); // Spieler ist am Anfang deaktiviert
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

        // 4. Elevator öffnet sich
        elevatorAnimator.SetTrigger(elevatorOpenTrigger);

        // Spieler soll den Aufzug verlassen können, bevor die nächsten Schritte ablaufen
    }

    void Update()
    {
        // Überprüfen, ob der Spieler den Aufzug verlassen hat
        if (playerExitedElevator)
        {
            CloseAndDescendElevator();
        }
    }

    private void CloseAndDescendElevator()
    {
        // 5. Elevator schließt und fährt wieder nach unten
        elevatorAnimator.SetTrigger(elevatorCloseTrigger);
        StartCoroutine(DelayElevatorDescend());
    }

    private IEnumerator DelayElevatorDescend()
    {
        yield return new WaitForSeconds(delayBetweenSteps);
        elevatorAnimator.SetTrigger(elevatorDescendTrigger);
    }

    // Wird vom Spieler aufgerufen, wenn er den Aufzug verlässt
    public void OnPlayerExitElevator()
    {
        playerExitedElevator = true;
    }
}
