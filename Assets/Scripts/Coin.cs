using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip pickupSound; // Assign the pickup sound in the Inspector
    public ParticleSystem pickupEffect; // Assign the particle effect prefab in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding with the coin is the player
        if (other.CompareTag("Player"))
        {
            // Play the sound effect
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            // Instantiate the particle effect at the coin's position
            Instantiate(pickupEffect, transform.position, Quaternion.identity);

            // Notify the player to update the coin balance
            other.GetComponent<CubeController>().AddCoin();

            // Destroy the coin game object
            Destroy(gameObject);
        }
    }
}
