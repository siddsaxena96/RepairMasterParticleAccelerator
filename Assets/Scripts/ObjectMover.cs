using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public float speed;
    public Vector2 dir;
    public int inverter = 1;

    private void Awake()
    {
        transform.position = point1.position;
        dir = (point2.position - point1.position).normalized;
    }

    private void Update()
    {
        transform.Translate(inverter * dir * speed);

        if (inverter > 0 && Vector2.Distance(transform.position, point2.position) <= 1f)
            inverter = -1;
        if (inverter < 0 && Vector2.Distance(transform.position, point1.position) <= 1f)
            inverter = 1;
    }
}
