using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class Camera : MonoBehaviour
{

    public Transform player;
    private Vector3 velocity = Vector3.zero;
    private float smooth = 0.20f;

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.position;
        playerPos.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, playerPos, ref velocity, smooth);
    }
}
