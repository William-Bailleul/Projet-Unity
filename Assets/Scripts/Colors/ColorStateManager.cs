using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static Colors ActiveColor;
    public delegate void ChangeColor(Colors newColor);
    public static event ChangeColor OnChangeColor;
    private bool _canChange;
    [SerializeField] private int _colorOwned;

    public PlayerFeet _feet;
    public Player _player;


    void Start()
    {
        _canChange = true;
        ActiveColor = Colors.None;
        _colorOwned = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && _feet._isGrounded)
        {
            if (!_canChange) return;
            Debug.Log($"{ActiveColor}");
            ActiveColor = (Colors)((int)ActiveColor % (_colorOwned)+1);
            Debug.Log($"{ActiveColor}");
            StartCoroutine(ColorChanging());
            SwitchState();
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            _colorOwned++;
            if (_colorOwned > 3)
                _colorOwned = Enum.GetValues(typeof(Colors)).Length-1;
            Debug.Log(_colorOwned);
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

    public IEnumerator ColorChanging()
    {
        _canChange = false;
        _player._isFrozen = true;
        _player._rb.velocity.Set(0f, 0f);
        yield return new WaitForSeconds(0.5f);
        _player._isFrozen = false;
        _canChange = true;
    }

}