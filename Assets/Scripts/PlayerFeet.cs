using UnityEngine;

public class PlayerFeet : MonoBehaviour
{
    public bool _isGrounded;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            _isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            _isGrounded = false;
    }
}
