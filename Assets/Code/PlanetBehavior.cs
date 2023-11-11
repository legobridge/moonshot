using UnityEngine;

public class PlanetBehavior : MonoBehaviour
{
    public float OrbitFrequencyScale = 0.001f;
    public float PeriodOfRevolution;
    private float _radiusOfOrbit;
    private float _initialAngle;
    private float _angle;
    private float _orbitFrequency;

    void Start()
    {
        _radiusOfOrbit = transform.localPosition.magnitude;
        _initialAngle = Mathf.Acos(transform.localPosition.x / _radiusOfOrbit);
        _angle = _initialAngle;
        _orbitFrequency = OrbitFrequencyScale / PeriodOfRevolution;
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        float timeState = Code.GameState.GetTimeState();
        float angleOfDisplacement = _orbitFrequency * timeState;
        _angle = _initialAngle + angleOfDisplacement;
        transform.localPosition = _radiusOfOrbit * new Vector2(Mathf.Cos(_angle), Mathf.Sin(_angle));
    }
}
