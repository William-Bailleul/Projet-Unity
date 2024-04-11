using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static Colors ActiveColor;
    public static Colors PreviousColor;
    public delegate void ChangeColor(Colors newColor);
    public static event ChangeColor OnChangeColor;
    private bool _canChange;
    private bool _firstColorUse;
    [SerializeField] private int _colorOwned;

    public PlayerFeet _feet;
    public Player _player;
    private Dissolve _dissolve;

    void Start()
    {
        _canChange = true;
        _firstColorUse = true;
        ActiveColor = Colors.None;
        _colorOwned = 1;
        _dissolve = FindObjectOfType<Dissolve>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && _feet._isGrounded)
        {
            if (!_canChange || _colorOwned == 0) return;
            Debug.Log($"{ActiveColor}");
            PreviousColor = ActiveColor;
            ActiveColor = (Colors)((int)ActiveColor % (_colorOwned)+1);
            Debug.Log($"{ActiveColor}");
            if(_firstColorUse && _colorOwned == 1)
            {
                    _firstColorUse = false;
                    SwitchState();
                    StartCoroutine(ColorChanging());
            }
            if (_colorOwned == 1) return;
            SwitchState();
            StartCoroutine(ColorChanging());


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

    void OnColorChange(Colors newColor)
    {
        if (_dissolve != null)
        {
            _dissolve.OnColorChange(newColor);
            _dissolve.SetDissolveAmount(1.1f, PreviousColor);
        }
    }
}
