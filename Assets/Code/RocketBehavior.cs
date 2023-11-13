using UnityEngine;

public class RocketBehavior : MonoBehaviour
{
    public float LaunchVelocity = 0.3f;
    public float ThrusterVelocity = 0.0005f;
    public GameObject RocketPrefab;
    public AudioClip LaunchSound;
    public AudioClip ThrusterSound;
    public AudioClip CrashSound;
    public AudioClip WinSound;

    private float _initialDistanceFromEarthCenter;
    private Vector3 _velocityVector = new Vector3(0, 0, 0);
    private AudioManager _audioManager;

    void Start()
    {
        _initialDistanceFromEarthCenter = transform.localPosition.magnitude;
        _audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (Code.GameState.IsRocketLaunched())
        {
            // Compute amount of thruster boost
            float xBoost = Input.GetAxis("Horizontal");
            float yBoost = Input.GetAxis("Vertical");
            Vector3 boost = ThrusterVelocity * (transform.right * xBoost + transform.up * yBoost);

            // Compute gravitational effects
            Vector3 gravitationalAcc = new(0, 0, 0);
            var massiveBodies = FindObjectsOfType<MassiveBodyBehavior>();
            foreach (var mb in massiveBodies)
            {
                Vector3 vectorToMb = mb.transform.position - transform.position;
                float acc = mb.GetAcceleration(vectorToMb.magnitude);
                gravitationalAcc += acc * vectorToMb.normalized;
            }

            if (boost.magnitude > 0.01f)
            {
                _audioManager.PlayOneShot(ThrusterSound);
            }

            // Change velocity
            _velocityVector += boost + gravitationalAcc;
            transform.localPosition += _velocityVector;
        }
        else
        {
            Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 parentPos = transform.parent.transform.position;
            Vector2 directionToMouse = (worldMousePos - parentPos).normalized;

            float zRotation = -Vector2.SignedAngle(directionToMouse, new Vector2(0, 1));
            transform.localEulerAngles = new Vector3(0, 0, zRotation); // todo
            transform.localPosition = _initialDistanceFromEarthCenter * directionToMouse;

            if (Input.GetButton("Jump"))
            {
                _velocityVector = LaunchVelocity * transform.localPosition;
                _audioManager.PlayOneShot(LaunchSound);
                Code.GameState.LaunchRocket();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Code.GameState.IsRocketLaunched())
        {
            if (collision.name != "Sun" && collision.transform.parent.name == "Jupiter")
            {
                _audioManager.PlayOneShot(WinSound);
                Code.GameState.GameOver(true);
                Destroy(gameObject);
            }
            else
            {
                CrashRocket();
            }
        }
    }

    private void OnBecameInvisible()
    {
        CrashRocket();
    }

    private void CrashRocket()
    {
        if (Code.GameState.IsRocketLaunched())
        {
            _audioManager.PlayOneShot(CrashSound);
            Code.GameState.ResetRocket();
            if (!Code.GameState.IsGameOver())
            {
                Vector2 rocketStartPosition = new(0, _initialDistanceFromEarthCenter);
                var newRocket = Instantiate(RocketPrefab, transform.parent);
                newRocket.transform.localPosition = rocketStartPosition;
            }
            Destroy(gameObject);
        }
    }
}
