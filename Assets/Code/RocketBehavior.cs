using UnityEngine;

public class RocketBehavior : MonoBehaviour
{
    private float _distanceFromEarthCenter;
    private float _angle;
    // Start is called before the first frame update
    void Start()
    {
        _distanceFromEarthCenter = transform.localPosition.magnitude;
        _angle = Mathf.Acos(transform.localPosition.x / _distanceFromEarthCenter);
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        _angle += 0.01f * x;
        transform.localEulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * _angle + 90);
        transform.localPosition = _distanceFromEarthCenter * new Vector2(Mathf.Cos(_angle), Mathf.Sin(_angle));
    }
}
