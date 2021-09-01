using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "particle")
        {
            other.gameObject.GetComponent<ParticleBehaviour>().EndJourney(false, "Particle Lost");
        }

        if (other.collider.tag == "boosItem")
        {
            Destroy(other.gameObject);
        }
    }
}
