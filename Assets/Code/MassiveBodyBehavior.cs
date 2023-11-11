using UnityEngine;

public class MassiveBodyBehavior : MonoBehaviour
{
    public float Mass;

    public float GetAcceleration(float distance)
    {
        return Code.GameState.GravitationalConstant * Mass / (distance * distance);
    }
}
