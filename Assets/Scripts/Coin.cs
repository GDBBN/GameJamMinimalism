using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip pickupSound; 
    public ParticleSystem pickupEffect; 

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            
        Instantiate(pickupEffect, transform.position, Quaternion.identity);
            
        other.GetComponent<CubeController>().AddCoin();
            
        Destroy(gameObject);
    }
}
