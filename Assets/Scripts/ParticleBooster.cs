using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBooster : MonoBehaviour
{
    public float speedMultiplier;
    public Transform launchPoint;
    private Rigidbody2D particleRb;
    public AudioSource audioSource;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "particle")
        {
            //other.gameObject.SetActive(false);
            particleRb = other.GetComponent<Rigidbody2D>();
            particleRb.transform.position = launchPoint.position;
            particleRb.velocity = launchPoint.right * particleRb.velocity.magnitude * speedMultiplier;
            audioSource.Play();
            //particleRb.velocity = new Vector2(particleRb.velocity.x, 0f) * speedMultiplier;
            //other.gameObject.SetActive(true);
        }
    }
}
