using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        transform.position += -transform.right * Time.deltaTime * speed;
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
