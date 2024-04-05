using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeet : MonoBehaviour
{
    public bool _isGrounded;

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        // Vérifier si le rendu appartient au Sorting Layer cible
        if (collision.gameObject.GetComponent<Renderer>() != null && collision.gameObject.GetComponent<Renderer>().sortingLayerName == "Ground")
        {
            _isGrounded = true;
        }

        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        // Vérifier si le rendu appartient au Sorting Layer cible
        if (collision.gameObject.GetComponent<Renderer>() != null && collision.gameObject.GetComponent<Renderer>().sortingLayerName == "Ground")
        {
            _isGrounded = false;
        }
    }
}
