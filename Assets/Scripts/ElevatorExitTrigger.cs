using UnityEngine;

public class ElevatorExitTrigger : MonoBehaviour
{
    public LevelIntroSequence levelIntroSequence;

    private void Start()
    {
        if (levelIntroSequence == null)
        {
            levelIntroSequence = FindFirstObjectByType<LevelIntroSequence>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            levelIntroSequence.OnPlayerExitElevator();
        }
    }
}
