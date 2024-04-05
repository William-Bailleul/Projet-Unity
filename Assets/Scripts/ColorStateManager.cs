using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static Colors ActiveColor;
    public delegate void ChangeColor(Colors newColor);
    public static event ChangeColor OnChangeColor;
    private int _firstLoop;

    void Start()
    {
        ActiveColor = Colors.None;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log($"{ActiveColor}");
            ActiveColor++;
            _firstLoop++;
            if (_firstLoop > Enum.GetValues(typeof(Colors)).Length)
            {
                ActiveColor = (Colors)((int)ActiveColor % Enum.GetValues(typeof(Colors)).Length);
            }
            else
            {

                ActiveColor = (Colors)((int)(ActiveColor) % Enum.GetValues(typeof(Colors)).Length-1)+1;
            }
            Debug.Log($"{ActiveColor}");
        }

    }

    public void SwitchState()
    {
        // Quand tu changes la couleur
        OnChangeColor?.Invoke(ActiveColor);
    }

    public enum Colors
    {
        None,
        Red,
        Blue,
        Yellow
    }
}
