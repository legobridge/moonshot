using UnityEngine;

public class PlanetBehavior : MonoBehaviour
{
    public float PeriodOfRevolution;
    private float _radiusOfOrbit;
    private float _initialAngle;
    private float _orbitFrequency;

    // Start is called before the first frame update
    void Start()
    {
        _radiusOfOrbit = transform.localPosition.magnitude;
        _initialAngle = Mathf.Acos(transform.localPosition.x / _radiusOfOrbit);
        _orbitFrequency = 0.001f / PeriodOfRevolution;
    }

    // Update is called once per frame
    void Update()
    {
        float timeState = Code.UI.GetTimeState();
        float angleOfDisplacement = _orbitFrequency * timeState;
        float angle = _initialAngle + angleOfDisplacement;
        transform.localPosition = _radiusOfOrbit * new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }
}
