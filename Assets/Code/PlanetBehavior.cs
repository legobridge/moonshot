using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetBehavior : MonoBehaviour
{
    public float PeriodOfRevolution;
    private float _radiusOfOrbit;
    private Vector2 _vectorToParent;
    private float _velocity;

    // Start is called before the first frame update
    void Start()
    {
        if (PeriodOfRevolution < 0.1f)
        {
            PeriodOfRevolution = Random.Range(60, 240);
        }
        _vectorToParent = transform.position - transform.parent.position;
        _radiusOfOrbit = _vectorToParent.magnitude;
        var perimeter = 2 * Mathf.PI * _radiusOfOrbit;
        _velocity = perimeter / PeriodOfRevolution;
    }

    // Update is called once per frame
    void Update()
    {
        _vectorToParent = transform.position - transform.parent.position;
        Vector2 directionOfMotion = Vector2.Perpendicular(_vectorToParent).normalized;
        transform.position = transform.position + 0.01f * _velocity * (Vector3) directionOfMotion;
    }
}
