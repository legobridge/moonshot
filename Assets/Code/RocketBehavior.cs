using UnityEngine;

public class RocketBehavior : MonoBehaviour
{
    public float LaunchVelocity = 0.1f;
    public float BoosterVelocity = 0.0001f;
    public GameObject RocketPrefab;
    // Todo: booster fuel, asteroids

    private float _initialDistanceFromEarthCenter;
    private float _initialAngle;
    private float _angle;
    private Vector3 _velocityVector = new Vector3(0, 0, 0);

    void Start()
    {
        _initialDistanceFromEarthCenter = transform.localPosition.magnitude;
        _initialAngle = Mathf.Acos(transform.localPosition.x / _initialDistanceFromEarthCenter);
        _angle = _initialAngle;
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (Code.GameState.IsRocketFired())
        {
            // Compute amount of thruster boost
            float xBoost = -Input.GetAxis("Horizontal");
            float yBoost = -Input.GetAxis("Vertical");
            Vector3 boost = BoosterVelocity * (transform.right * xBoost + transform.up * yBoost);

            // Compute gravitational effects
            Vector3 gravitationalAcc = new(0, 0, 0);
            var massiveBodies = FindObjectsOfType<MassiveBodyBehavior>();
            foreach (var mb in massiveBodies)
            {
                Vector3 vectorToMb = mb.transform.position - transform.position;
                float acc = mb.GetAcceleration(vectorToMb.magnitude);
                gravitationalAcc += acc * vectorToMb.normalized;
            }
            _velocityVector += boost + gravitationalAcc;
            transform.localPosition += _velocityVector;
        }
        else
        {
            float x = Input.GetAxis("Horizontal");
            _angle -= 0.01f * x;

            transform.localEulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * _angle + 90);
            transform.localPosition = _initialDistanceFromEarthCenter * new Vector2(Mathf.Cos(_angle), Mathf.Sin(_angle));

            if (Input.GetButton("Jump"))
            {
                _velocityVector = LaunchVelocity * transform.localPosition;
                Code.GameState.FireRocket();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Code.GameState.IsRocketFired())
        {
            // Todo: check for win
            CrashRocket();
        }
    }

    private void OnBecameInvisible()
    {
        CrashRocket();
    }

    private void CrashRocket()
    {
        Vector2 rocketStartPosition = _initialDistanceFromEarthCenter * new Vector2(Mathf.Cos(_initialAngle), Mathf.Sin(_initialAngle));
        var newRocket = Instantiate(RocketPrefab, transform.parent);
        newRocket.transform.localPosition = rocketStartPosition;
        Code.GameState.ResetRocket();
        Destroy(gameObject);
    }
}
