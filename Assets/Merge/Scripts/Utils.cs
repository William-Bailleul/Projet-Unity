using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Rigidbody2D _rb2d;
    public static Utils instance;

    public static Rigidbody2D UtilsRigidBody2D { get => _rb2d; set => _rb2d = value; }

    public static void Flip2D_Object(GameObject gameObject, float xRot = 0f, float yRot = 0f)
    {
        gameObject.transform.localRotation = Quaternion.Euler(xRot, yRot, 0f);
    }

}
