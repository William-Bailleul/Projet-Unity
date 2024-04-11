using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
public class ColorEvent2 : MonoBehaviour
{
    public ColorManager2.Colors Color;
    private bool _active = false;
    private Collider2D _collider;
    private SpriteRenderer _sprite;
    private Dissolve _dissolve;
    private ColorManager2.Colors _previousColor;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _dissolve = GetComponent<Dissolve>();
        SetActive(false);
    }

    private void Start()
    {
        ColorManager2.OnChangeColor += onColorChange;
    }

    private void OnDestroy()
    {
        ColorManager2.OnChangeColor -= onColorChange;
    }

    public void onColorChange(ColorManager2.Colors newColor)
    {
        bool shouldBeActive = (Color == newColor);

        if (_dissolve != null && _dissolve.gameObject.activeSelf)
        {
            _dissolve.OnColorChange(newColor);
        }

        if (shouldBeActive)
        {
            SetActive(true);
        }
        else
        {
            if (_dissolve != null && Color == ColorManager2.GetPreviousColor())
            {
                StartCoroutine(FadeOutAndDisable());
            }
        }

        _previousColor = ColorManager2.GetPreviousColor();
    }



    private void SetActive(bool active)
    {
        _active = active;
        _collider.enabled = active;
        _sprite.enabled = active;
    }

    private IEnumerator FadeOutAndDisable()
    {
        if (_dissolve != null)
        {
            yield return StartCoroutine(_dissolve.Vanish());
        }

        SetActive(false);
    }

    private void onDestroy()
    {
        if(_dissolve != null)
        {
            Destroy(_dissolve.gameObject);
        }
    }
}