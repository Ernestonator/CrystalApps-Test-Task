using UnityEngine;

/// <summary>
/// Represents one obstacle.
/// All obstacles causes player to push, so it has it's power and time.
/// Also we can decide if player will be dead after he collides with obstacle.
/// </summary>
public class Obstacle : MonoBehaviour
{
    public float pushPower = 10f;
    public float pushTime = 0.5f;
    /// <summary>
    /// Defines if player will be dead after colliding with this obstacle.
    /// </summary>
    public bool isDeadly = true;
}
