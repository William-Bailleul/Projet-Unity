using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static Colors ActiveColor;
    private static Colors PreviousColor;
    public delegate void ChangeColor(Colors newColor);
    public static event ChangeColor OnChangeColor;
    private bool _canChange;
    [SerializeField] private int _colorOwned;

    public PlayerFeet _feet;
    public Player _player;

    private Dissolve _dissolve;

    void Start()
    {
        _canChange = true;
        ActiveColor = Colors.None;
        OnChangeColor += OnColorChange;
        _colorOwned = 1;
        _dissolve = FindObjectOfType<Dissolve>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && _feet._isGrounded)
        {
            if (!_canChange) return;
            //Debug.Log($"{ActiveColor}");
            PreviousColor = ActiveColor;
            ActiveColor = (Colors)((int)ActiveColor % (_colorOwned) + 1);
            //Debug.Log($"{ActiveColor}");
            StartCoroutine(ColorChanging());
            SwitchState();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _colorOwned++;
            if (_colorOwned > 3)
                _colorOwned = Enum.GetValues(typeof(Colors)).Length - 1;
            //Debug.Log(_colorOwned);
        }

    }

    public void SwitchState()
    {
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
        _player._rb2d.velocity.Set(0f, 0f);
        yield return new WaitForSeconds(0.5f);
        _player._isFrozen = false;
        _canChange = true;
    }

    void OnColorChange(Colors newColor)
    {
        if (_dissolve != null)
        {
            _dissolve.OnColorChange(newColor);
            _dissolve.SetDissolveAmount(1.1f, GetPreviousColor());
        }
    }

    public static Colors GetPreviousColor()
    {
        return PreviousColor;
    }

    public static Colors GetActiveColor()
    {
        return ActiveColor;
    }

}