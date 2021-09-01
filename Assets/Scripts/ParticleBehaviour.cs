using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehaviour : MonoBehaviour
{
    [SerializeField] private Rigidbody2D particleRb = null;
    public float particleSpeed = 100f;
    private bool endHit = false;
    private bool particleInJourney;
    public Action<bool, string> OnJourneyEnd;

    public void LaunchParticle()
    {
        particleRb.bodyType = RigidbodyType2D.Dynamic;
        particleRb.velocity = transform.right * particleSpeed;
        particleInJourney = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "endpoint" && !endHit)
        {
            endHit = true;
            Debug.Log("End Hit");
            EndJourney(true, "Particle Transfer Complete");
            particleInJourney = false;
        }
    }

    private void Update()
    {
        if (particleInJourney && particleRb.velocity.magnitude <= 4f)
        {
            particleInJourney = false;
            EndJourney(false, "Particle too slow");
        }
    }

    public void EndJourney(bool success, string message)
    {
        OnJourneyEnd?.Invoke(success, message);
    }

}
