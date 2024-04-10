using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{

    public GameObject player;
    public float speed;
    public enum state
    {
        CAMERA,
        LIGHT
    }

    public state currentState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (currentState == state.CAMERA )
            transform.position = Vector3.Lerp(transform.position, player.transform.position - Vector3.forward, Time.fixedDeltaTime * speed);

        if (currentState == state.LIGHT)
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x, 8, 0), Time.fixedDeltaTime * speed);
    }
}
