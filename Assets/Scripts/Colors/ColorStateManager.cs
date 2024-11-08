using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private Hue _hue;

    void Start()
    {
        _canChange = true;
        _firstColorUse = true;
        ActiveColor = Colors.None;
        _dissolve = FindObjectOfType<Dissolve>();
        _hue = GetComponent<Hue>();

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
                _hue.ChangeHue(ActiveColor);
            }
            if (_colorOwned == 1) return;
            SwitchState();
            StartCoroutine(ColorChanging());
            _hue.ChangeHue(ActiveColor);

        }
        //if (Input.GetKeyDown(KeyCode.Q)) {
        //    _colorOwned++;
        //    if (_colorOwned > 3)
        //        _colorOwned = Enum.GetValues(typeof(Colors)).Length-1;
        //    Debug.Log(_colorOwned);
        //}
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
        _player.getInstance.IsFrozen = true;
        _player._rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        _player.getInstance.IsFrozen = false;
        _canChange = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Red Brush" || collision.gameObject.tag == "Blue Brush" || collision.gameObject.tag == "Yellow Brush")
        {
            Destroy(collision.gameObject);
            _colorOwned++;
            StartCoroutine(ColorChanging());
        }
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
