using UnityEngine;

public class ElevatorExitTrigger : MonoBehaviour
{
    public LevelIntroSequence levelIntroSequence; // Referenz zum Hauptskript

    void Start()
    {
        if (levelIntroSequence == null)
        {
            levelIntroSequence = FindObjectOfType<LevelIntroSequence>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            levelIntroSequence.OnPlayerExitElevator();
        }
    }
}
