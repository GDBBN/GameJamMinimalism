using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollisionSound : MonoBehaviour
{
    public AudioSource hitSound;

    void OnCollisionEnter()
    {
        hitSound.Play();
    }
}
