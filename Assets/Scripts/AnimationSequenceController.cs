using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using TMPro;

public class AnimationSequenceController : MonoBehaviour
{
    public Animator elevatorAnimator; 
    public Animator mapAnimator; 
    public GameObject player;
    public string elevatorCloseTrigger; 
    public string elevatorDownTrigger; 
    public string mapSpinTrigger; 
    public string nextSceneName;
    public int elevatorCloseFrames;
    public int elevatorDownFrames;
    public int mapSpinFrames;
    public float frameRate = 30f;
    public float sceneLoadDelay = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != player) return;
        StartCoroutine(PlaySequence());
        StartCoroutine(LoadSceneAfterDelay());
    }

    private IEnumerator PlaySequence()
    {
        elevatorAnimator.SetTrigger(elevatorCloseTrigger);
        yield return new WaitForSeconds(elevatorCloseFrames / frameRate);

        player.SetActive(false);

        elevatorAnimator.SetTrigger(elevatorDownTrigger);
        yield return new WaitForSeconds(elevatorDownFrames / frameRate);

        Debug.Log("tsudgfdszuf");
        mapAnimator.SetTrigger(mapSpinTrigger);
        yield return new WaitForSeconds(mapSpinFrames / frameRate);
        Debug.Log("Next Scene loading");
        SceneManager.LoadSceneAsync(nextSceneName);

    }

    private IEnumerator LoadSceneAfterDelay()
    {
        Debug.Log("Starting Delay");
        yield return new WaitForSeconds(sceneLoadDelay);
        
    }
}
