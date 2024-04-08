using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Custom/Stats")]
public class ScriptableStats : ScriptableObject
{
    public float horizontal = 10f;

    public float maxTimeJump = 0.2f;
    public float currentJumpTime = 0;

    public float coyoteTime = .15f;


    public float jumpForce = 25f;
    public float gravityScale = 15f;

}