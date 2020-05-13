/**
 * Author:          Sebastian Przyszlak
 * Collaborators:   
 */
 [UnityEngine.CreateAssetMenu (fileName = "MoveStats", menuName = "Stats/MoveStats")]
public class MoveStats : UnityEngine.ScriptableObject {
    [UnityEngine.SerializeField] private float speed = 5f;
    public float Speed => speed;

    [UnityEngine.SerializeField] private float jumpHeight = 3f;
    public float JumpHeight => jumpHeight;

    [UnityEngine.SerializeField] private UnityEngine.LayerMask allowJumpOn = 0;
    public UnityEngine.LayerMask AllowJumpOn => allowJumpOn;

    [UnityEngine.SerializeField] private float groundDistance = 0.2f;
    public float GroundDistance => groundDistance;
}
