using UnityEngine;

public class RocketBehavior : MonoBehaviour
{
    public float LaunchVelocity = 0.1f;
    public float BoosterVelocity = 0.0001f;
    // Todo: booster fuel, collision detection, gravity, asteroids

    private float _distanceFromEarthCenter;
    private float _angle;
    private float _xBoost = 0f;
    private float _yBoost = 0f;
    private Vector3 _velocityVector = new Vector3(0, 0, 0);

    void Start()
    {
        _distanceFromEarthCenter = transform.localPosition.magnitude;
        _angle = Mathf.Acos(transform.localPosition.x / _distanceFromEarthCenter);
    }

    void Update()
    {
        if (Code.UI.IsRocketFired())
        {
            _xBoost = -Input.GetAxis("Horizontal");
            _yBoost = -Input.GetAxis("Vertical");
        }
        else
        {
            float x = Input.GetAxis("Horizontal");
            _angle -= 0.01f * x;

            transform.localEulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * _angle + 90);
            transform.localPosition = _distanceFromEarthCenter * new Vector2(Mathf.Cos(_angle), Mathf.Sin(_angle));

            if (Input.GetButtonDown("Jump"))
            {
                _velocityVector = LaunchVelocity * transform.localPosition;
                Code.UI.FireRocket();
            }
        }
    }

    private void FixedUpdate()
    {
        if (Code.UI.IsRocketFired())
        {
            _velocityVector += BoosterVelocity * (transform.right * _xBoost + transform.up * _yBoost);
            transform.localPosition += _velocityVector;
        }
    }
}
